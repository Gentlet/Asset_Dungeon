using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : WeaponCard {

    protected override void Init()
    {
        name = "AssaultRifle";
        property = Properties.Weapon;
        restriction = Restrictions.None;
    }
}
