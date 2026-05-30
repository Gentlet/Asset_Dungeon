using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolutionCard : Card
{

    protected override void Init()
    {
        name = "RevolutionCard";
        property = Properties.Bullet;
        restriction = Restrictions.None;
    }

    public RevolutionObject Revolution(Bullet bullet, Vector2 direction, float speed, Transform following, float distance)
    {
        RevolutionObject obj = GameObject.Instantiate<RevolutionObject>(BulletManager.Instance.RevolutionObj, BulletManager.Instance.bulletsObject.transform);
        obj.Init(direction, speed, following);
        obj.transform.position = bullet.ConnectedUnit.transform.position;

        obj.StoreBullet(bullet, distance);


        return obj;
    }

    public RevolutionObject Revolution(Bullet[] bullets, Vector2 direction, float speed, Transform following, float distance)
    {
        RevolutionObject obj = GameObject.Instantiate<RevolutionObject>(BulletManager.Instance.RevolutionObj, BulletManager.Instance.bulletsObject.transform);
        obj.Init(direction, speed, following);



        for (int i = 0; i < bullets.Length; i++)
        {
            obj.StoreBullet(bullets[i], distance);
        }
        

        return obj;
    }
}
