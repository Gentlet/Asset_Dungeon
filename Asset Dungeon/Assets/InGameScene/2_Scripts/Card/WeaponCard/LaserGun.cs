using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : WeaponCard {

    protected override void Init()
    {
        name = "LaserGun";
        property = Properties.Weapon;
        restriction = Restrictions.None;
    }
}
