using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : SingletonGameObject<UnitManager> {

    private List<Unit> units;

    public void AddUnit(Unit unit)
    {
        Units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        Units.Remove(unit);
    }

    public Unit GetUnit(int num)
    {
        if (Units.Count <= num)
            return null;
        return Units[num];
    }

    public Unit GetUnit(string name)
    {
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Status.Name == name)
                return Units[i];
        }

        return null;
    }

    public Unit[] GetUnits(string name)
    {
        List<Unit> detectedunits = new List<Unit>();

        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Status.Name == name)
                detectedunits.Add(Units[i]);
        }

        if (detectedunits.Count == 0)
            return null;

        Unit[] detecedunits_array = new Unit[detectedunits.Count];

        for (int i = 0; i < detectedunits.Count; i++)
        {
            detecedunits_array[i] = detectedunits[i];
        }

        return detecedunits_array;
    }

    public Unit GetUnit(UnitStatus.Type type)
    {
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Status.UnitType == type)
                return Units[i];
        }

        return null;
    }

    public Unit[] GetUnits(UnitStatus.Type type)
    {
        List<Unit> detectedunits = new List<Unit>();

        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Status.UnitType == type)
                detectedunits.Add(Units[i]);
        }

        if(detectedunits.Count == 0)
            return null;

        Unit[] detecedunits_array = new Unit[detectedunits.Count];

        for (int i = 0; i < detectedunits.Count; i++)
        {
            detecedunits_array[i] = detectedunits[i];
        }

        return detecedunits_array;
    }


    #region Property
    private List<Unit> Units
    {
        get
        {
            if (units == null)
                units = new List<Unit>();

            return units;
        }
        set
        {
            units = value;
        }
    }

#endregion
}
