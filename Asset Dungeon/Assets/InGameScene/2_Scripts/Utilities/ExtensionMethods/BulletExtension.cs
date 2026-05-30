using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletExtension {

    public static Vector2 Angle2Direcrion(this Bullet bullet, float angle)
    {
        angle = angle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static float Direction2Angle(this Bullet bullet, Vector2 direction)
    {
        return bullet.transform.position.Angle(bullet.transform.position + (Vector3)direction);
    }
}
