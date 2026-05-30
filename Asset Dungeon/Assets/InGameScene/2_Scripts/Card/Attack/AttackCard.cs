using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card {

    public virtual IEnumerator AttackActive(Unit unit)
    {
        return null;
    }
}
