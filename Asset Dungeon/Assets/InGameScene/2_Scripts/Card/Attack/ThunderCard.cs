using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderCard : AttackCard {
    protected override void Init()
    {
        name = "ThunderCard";
        property = Properties.Attack;
        restriction = Restrictions.None;
    }

    public override IEnumerator AttackActive(Unit unit)
    {
        return Thunder(unit);
    }

    private IEnumerator Thunder(Unit unit)
    {
        unit.Stun = true;

        yield return new WaitForSeconds(1f);

        unit.Stun = false;
    }
}
