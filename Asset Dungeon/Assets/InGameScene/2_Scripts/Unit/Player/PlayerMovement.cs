using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : UnitMovement {
    public ParticleSystem particle;

    private Vector3 direction;
    private bool roll;

    private Vector3 safePos;
    private Vector3 safevel;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CliffLine"))
        {
            safePos = transform.position;
            safevel = direction;
        }
    }

    protected override void Cliff(Collider2D collision)
    {
        if (collision.CompareTag("Cliff") && !roll)
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

    private void Update()
    {
        if (CursorManager.Instance.GameisRunning && !stop && !Unit.Stun)
        {
            if (!roll) Movement();

            Unit.Rigid.velocity = (roll == true ? (direction.normalized * 8f) : direction);
        }
        else
            Unit.Rigid.velocity = Vector3.zero;
    }

    protected override IEnumerator CliffDead()
    {
        if (!stop)
        {
            stop = true;


            while (true)
            {
                Unit.transform.localScale -= Vector3.one * 0.008f;
                Unit.transform.rotation = Quaternion.Euler(Unit.transform.rotation.eulerAngles + new Vector3(0, 0, 11f));

                if (Unit.transform.localScale.x <= Vector3.zero.x)
                {
                    break;
                }

                yield return new WaitForEndOfFrame();
            }

            Unit.transform.localScale = Vector3.zero;

            stop = false;

            Unit.transform.localScale = Vector3.one;
            Unit.transform.rotation = Quaternion.Euler(Vector3.zero);

            transform.position = safePos - (safevel * 0.5f);

            Unit.Status.Hp -= 1f;

            //StopAllCoroutines();
            ////StopCoroutine(CliffDead());

            GameManager.Instance.RefreshPlayerState();
        }
    }

    protected override void Movement()
    {
        Vector2 vel = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            vel.y += Unit.Status.Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vel.y -= Unit.Status.Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vel.x -= Unit.Status.Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vel.x += Unit.Status.Speed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !roll && vel != Vector2.zero) 
        {
            roll = true;

            Unit.Animator.WeaponAnimator.enabled = false;

            Color color = Unit.Attack.UnitAttackSpriteRenderer.color;
            color.a = 0;
            Unit.Attack.UnitAttackSpriteRenderer.color = color;


            Invoke("EndRoll", 0.5f);
        }

        direction = vel - (vel * ((Unit.Weapon.Weight - 1) * 0.15f));

        Unit.Animator.AnimationTrigger = direction.Direction2AnimationType();
        Unit.Animator.RollingTrigger = roll;

        if (vel == Vector2.zero || roll)
        {
            particle.gameObject.SetActive(false);
        }
        else
        {
            particle.gameObject.SetActive(true);
        }
    }

    private void EndRoll()
    {
        Color color = Unit.Attack.UnitAttackSpriteRenderer.color;
        color.a = 1;
        Unit.Attack.UnitAttackSpriteRenderer.color = color;

        Unit.Animator.WeaponAnimator.enabled = true;

        roll = false;
    }

    #region Property
    public bool Roll
    {
        get
        {
            return roll;
        }
    }
#endregion
}
