using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Movement
 * Unit의 하위 컴포넌트
 * Unit의 움직임에 대한 부분을 구현한다
 * 각각의 Unit에 맞게 상속하여 구현 할 수 있을듯 싶다(??)
 */
public abstract class UnitMovement : ChildUnitInterface
{
    protected bool stop;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Cliff(collision);
    }

    protected virtual void Cliff(Collider2D collision)
    {
        if (collision.CompareTag("Cliff"))
        {
            Vector2 pos = transform.position;
            pos.x %= 1;
            pos.y %= 1;

            List<RaycastHit2D> hits = new List<RaycastHit2D>();

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if (x == 0 || y == 0)
                        continue;

                    hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(0.45f * x, 0.3f * y), new Vector2(x, 0), 1f));
                    hits.Add(Physics2D.Raycast((Vector2)transform.position + new Vector2(0.3f * x, 0.45f * y), new Vector2(0, y), 1f));

                    Debug.DrawRay((Vector2)transform.position + new Vector2(0.45f * x, 0.3f * y), new Vector2(x, 0), (hits[hits.Count - 2].collider != null ? (hits[hits.Count - 2].collider.CompareTag("Cliff") ? Color.green : Color.red) : Color.red));
                    Debug.DrawRay((Vector2)transform.position + new Vector2(0.3f * x, 0.45f * y), new Vector2(0, y), (hits[hits.Count - 1].collider != null ? (hits[hits.Count - 1].collider.CompareTag("Cliff") ? Color.green : Color.red) : Color.red));
                }
            }

            int xc = 0;
            int yc = 0;

            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0 && hits[i].collider != null && hits[i].collider.CompareTag("Cliff"))
                    xc++;
                else if (i % 2 == 1 && hits[i].collider != null && hits[i].collider.CompareTag("Cliff"))
                    yc++;
            }

            if (xc == 4 || yc == 4)
            {
                StartCoroutine(CliffDead());
            }
        }
    }

    protected virtual IEnumerator CliffDead()
    {
        stop = true;


        while (true)
        {
            Unit.transform.localScale -= Vector3.one * 0.001f;
            Unit.transform.rotation = Quaternion.Euler(Unit.transform.rotation.eulerAngles + new Vector3(0, 0, 0.5f));

            if(Unit.transform.localScale.x <= Vector3.zero.x)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        Unit.transform.localScale = Vector3.zero;
        
        Unit.pDead();
    }

    protected virtual void Movement()
    {

    }
}
