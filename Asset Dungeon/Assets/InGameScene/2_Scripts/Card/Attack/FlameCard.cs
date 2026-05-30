using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCard : AttackCard
{
    protected override void Init()
    {
        name = "FlameCard";
        property = Properties.Attack;
        restriction = Restrictions.None;
    }

    public override IEnumerator AttackActive(Unit unit)
    {
        return Flame(unit);
    }

    private IEnumerator Flame(Unit unit)
    {
        for (int i = 0; i < 5; i++)
        {
            unit.Status.Hp -= 2f;

            yield return new WaitForSeconds(1f);
        }
    }
}
