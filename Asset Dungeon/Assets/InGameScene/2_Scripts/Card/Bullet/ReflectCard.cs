using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectCard : Card {

    protected override void Init()
    {
        name = "ReflectCard";
        property = Properties.Bullet;
        restriction = Restrictions.None;
    }

    public void Reflect(Bullet bullet)
    {
        if (0 < bullet.reflectcount)
        {
            CircleCollider2D collider = bullet.BulletCollider;
            Vector3 direction = bullet.Direction;


            List <RaycastHit2D[]> hits = new List<RaycastHit2D[]>();        //총알 충돌체크를 위한 레이를쏨

            hits.Add(Physics2D.RaycastAll((bullet.transform.position + new Vector3(collider.radius / 2f, 0, 0)) - direction, direction, 4f));
            hits.Add(Physics2D.RaycastAll((bullet.transform.position + new Vector3(-collider.radius / 2f, 0, 0)) - direction, direction, 4f));
            hits.Add(Physics2D.RaycastAll((bullet.transform.position + new Vector3(0, collider.radius / 2f, 0)) - direction, direction, 4f));
            hits.Add(Physics2D.RaycastAll((bullet.transform.position + new Vector3(0, -collider.radius / 2f, 0)) - direction, direction, 4f));


            RaycastHit2D closestRay = new RaycastHit2D();
            for (int i = 0; i < 4; i++)         //벽과 충돌한 가장 가까운 레이를 고름
            {
                for (int j = 0; j < hits[i].Length; j++)
                {
                    if (
                        (hits[i][j].transform.CompareTag("Wall") && closestRay.transform == null) || 
                        (hits[i][j].transform.CompareTag("Wall") && Vector3.Distance(bullet.transform.position, hits[i][j].point) < Vector3.Distance(bullet.transform.position, closestRay.point))
                        )
                    {
                        closestRay = hits[i][j];
                    }
                }
            }

            if (closestRay.transform == null)
            {
                Debug.Log("Chrushed Block is not found");
                return;
            }

            #region 
            //Vector3 vec = closestRay.point + (Vector2)direction;    //부딛친 블럭찾기

            //vec.x = ((vec.x - bullet.ConnectedUnit.transform.position.x) < 0 ? Mathf.FloorToInt(vec.x) : (int)vec.x);
            //vec.y = ((vec.y - bullet.ConnectedUnit.transform.position.y) < 0 ? Mathf.FloorToInt(vec.y) : (int)vec.y);

            //TileProperty tile = TileManager.Instance.GetTilemap("Wall").GetTile(vec);

            //if (tile == null)
            //{
            //    tile = TileManager.Instance.GetTilemap("Wall").GetClosestTile(vec);
            //}

            //블럭 비교
            //if ((tile.CompareType("CrossRightTop") && 0 < direction.x && 0 < direction.y) || (tile.CompareType("CrossRightBottom") && 0 < direction.x && direction.y < 0) || (tile.CompareType("CrossLeftTop") && direction.x < 0 && 0 < direction.y) || (tile.CompareType("CrossLeftBottom") && direction.x < 0 && direction.y < 0))
            //    direction *= -1f;
            //else

#endregion
            direction = Vector3.Reflect(direction, closestRay.normal);

            if (closestRay.normal.x != 0 && closestRay.normal.y == 0)
                bullet.transform.rotation = Quaternion.Euler(bullet.transform.rotation.eulerAngles.x, bullet.transform.rotation.eulerAngles.y + 180, bullet.transform.rotation.eulerAngles.z);
            if (closestRay.normal.y != 0 && closestRay.normal.x == 0)
                bullet.transform.rotation = Quaternion.Euler(bullet.transform.rotation.eulerAngles.x + 180, bullet.transform.rotation.eulerAngles.y, bullet.transform.rotation.eulerAngles.z);

            bullet.Direction = direction;

            bullet.reflectcount -= 1;

            if (bullet.reflectcount == 0)
            {
                GameManager.Instance.PlayWallCrushParticle(bullet.transform.position);

                bullet.Active = false;
            }
        }
        else
        {
            bullet.Active = false;
        }
    }
}
