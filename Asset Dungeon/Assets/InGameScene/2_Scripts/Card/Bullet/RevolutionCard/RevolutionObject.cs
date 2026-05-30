using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionObject : MonoBehaviour {
   
    private List<Bullet> bullets;

    private Rigidbody2D rigid;
    private Transform following;

    private Vector2 direction;

    private float speed;
    private float rotaterate;

    private bool active;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        bullets = new List<Bullet>();
    }

    private void Update()
    {
        //if (!IsBulletsActive)
        //{
        //    for (int i = 0; i < bullets.Count; i++)
        //    {
        //        bullets[i].transform.parent = BulletManager.Instance.bulletsObject.transform;
        //    }

        //    Destroy(gameObject);
        //}

        BulletRefresh();

        if (bullets.Count == 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if(Active)
        {
            rigid.velocity = direction * speed;
            transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + RotateRate);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }

        if (Following != null)
        {
            transform.position = Following.transform.position;
        }
    }


    public void Init(Vector2 direction, float speed, Transform following)
    {
        Following = following;
        Direction = direction;
        Speed = speed;
    }

    public void StoreBullet(Bullet bullet, float distance)
    {
        bullet.transform.parent = transform;

        bullet.Speed += Random.Range(-5f, 0f);

        StartCoroutine(BulletDestroy(bullet));
        StartCoroutine(BulletDirectionSet(bullet));
        StartCoroutine(BulletSpeedDown(bullet, distance));

        bullets.Add(bullet);
    }

    public void StoreBullet(Bullet bullet)
    {
        bullet.transform.parent = transform;
        bullet.revolutionbullet = true;

        bullets.Add(bullet);
    }

    public void BulletRefresh()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].Active == false)
            {
                bullets[i].transform.parent = BulletManager.Instance.bulletsObject.transform;
                bullets[i].revolutionbullet = false;
                bullets.Remove(bullets[i]);
            }
        }
    }

    private IEnumerator BulletSpeedDown(Bullet bullet, float distance)
    {
        while(true)
        {
            if (distance < Vector3.Distance(transform.position, bullet.transform.position)) 
            {
                if (RotateRate != -1)
                    RotateRate = bullet.Speed * 0.2f * (Random.Range(0, 2) == 0 ? -1 : 1);
                bullet.Speed *= 0.03f;

                Active = true;
                break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator BulletDirectionSet(Bullet bullet)
    {
        while(true)
        {
            bullet.Direction = transform.position.Unit2UnitDirection(bullet.transform.position);

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator BulletDestroy(Bullet bullet)
    {
        yield return new WaitForSeconds(8f);

        bullet.Active = false;
    }

    #region Properties
    private bool IsBulletsActive
    {
        get
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].Active)
                    return true;
            }

            return false;
        }
    }

    public List<Bullet> Bullets
    {
        get
        {
            return bullets;
        }
    }

    public Transform Following
    {
        get
        {
            return following;
        }
        set
        {
            following = value;
        }
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value.normalized;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    public float RotateRate
    {
        get
        {
            return rotaterate;
        }
        set
        {
            rotaterate = value;
        }
    }

    public bool Active
    {
        get
        {
            return active;
        }
        set
        {
            active = value;
        }
    }

    #endregion
}
