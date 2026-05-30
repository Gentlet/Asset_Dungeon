using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeCard : AttackCard
{
    protected override void Init()
    {
        name = "FreezeCard";
        property = Properties.Attack;
        restriction = Restrictions.None;
    }

    public override IEnumerator AttackActive(Unit unit)
    {
        return Freeze(unit);
    }

    private IEnumerator Freeze(Unit unit)
    {
        float speed = unit.Status.Speed;

        unit.Status.Speed = speed * 0.6f;

        yield return new WaitForSeconds(3f);

        unit.Status.Speed = speed;
    }
}
