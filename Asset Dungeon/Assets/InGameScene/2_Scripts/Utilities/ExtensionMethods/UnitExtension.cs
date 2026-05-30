using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitExtension {
    public static T GetUnit<T>(this Unit unit) where T : Unit
    {
        return unit as T;
    }

    public static T GetUnitAttack<T>(this Unit unit) where T : UnitAttack
    {
        return unit.Attack as T;
    }

    public static T GetUnitMovement<T>(this Unit unit) where T : UnitMovement
    {
        return unit.Movement as T;
    }
}
