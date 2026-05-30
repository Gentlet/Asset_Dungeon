using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : WeaponCard {

    protected override void Init()
    {
        name = "ShotGun";
        property = Properties.Weapon;
        restriction = Restrictions.None;
    }
}
