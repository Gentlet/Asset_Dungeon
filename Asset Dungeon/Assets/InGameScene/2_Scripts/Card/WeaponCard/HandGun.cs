using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : WeaponCard {

    protected override void Init()
    {
        name = "HandGun";
        property = Properties.Weapon;
        restriction = Restrictions.None;
    }
}
