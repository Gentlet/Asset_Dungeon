using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotifAttackCard : PatternCard
{
    protected override void Init()
    {
        name = "MotifAttackCard";
        property = Properties.Pattern;
        restriction = Restrictions.Enemy;
    }

    public override IEnumerator Pattern(EnemyUnit unit)
    {
        return MotifAttack(unit);
    }

    public IEnumerator MotifAttack(EnemyUnit unit)
    {
        EnemyAttack attack = unit.GetUnitAttack<EnemyAttack>();
        EnemyMovement movement = unit.GetUnitMovement<EnemyMovement>();

        movement.StopMovemnet = true;

        List<Bullet[]> bul = new List<Bullet[]>();

        RevolutionObject obj = GameObject.Instantiate<RevolutionObject>(BulletManager.Instance.RevolutionObj, BulletManager.Instance.bulletsObject.transform);
        obj.Init(attack.Degree2PlayerUnit.Angle2Direcrion(), unit.Weapon.Speed, null);
        obj.transform.position = unit.transform.position;
        obj.RotateRate = 2;

        for (int i = 0; i < 15; i++)     //총알갯수
        {
            Bullet[] bullets = attack.publicAttack();
            bul.Add(bullets);

            for (int j = 0; j < bullets.Length; j++)
            {
                if (bullets[j].ConnectedUnitType != UnitStatus.Type.Player)
                {
                    bullets[j].Speed = 0;
                    obj.StoreBullet(bullets[j]);
                }
            }


            if (i < 3)
            {
                for (int j = 0; j < bullets.Length; j++)
                {
                    bullets[j].transform.position = obj.transform.position + new Vector3(0, 3);
                }

                obj.transform.rotation = Quaternion.Euler(0, 0, obj.transform.rotation.eulerAngles.z + 120);
            }
            else
            {
                //(i-2) / 4
                Vector3 point1 = obj.Bullets[((i - 2) / 4) % 3].transform.position;
                Vector3 point2 = obj.Bullets[(((i - 2) / 4) + 1) % 3].transform.position;

                Vector3 interval = (point2 - point1) / 5;

                for (int j = 0; j < bullets.Length; j++)
                {
                    bullets[j].transform.position = point1 + (interval * (((i - 2) % 4) + 1));
                }
            }
        }

        obj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        obj.Active = true;

        yield return new WaitForSeconds(1f);

        obj.BulletRefresh();

        obj.Active = false;

        for (int i = 0; i < bul.Count; i++)
        {
            for (int z = 0; z < bul[i].Length; z++)
            {
                Vector2 direction = obj.transform.position.Unit2UnitDirection(bul[i][z].transform.position);

                bul[i][z].Direction = direction;
                bul[i][z].Speed = unit.Weapon.Speed;
            }
        }

        movement.StopMovemnet = false;
    }
}
