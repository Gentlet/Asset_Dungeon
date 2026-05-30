using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhombusAttackCard : PatternCard
{
    protected override void Init()
    {
        name = "RhombusAttackCard";
        property = Properties.Pattern;
        restriction = Restrictions.Enemy;
    }

    public override IEnumerator Pattern(EnemyUnit unit)
    {
        return RhombusAttack(unit);
    }

    public IEnumerator RhombusAttack(EnemyUnit unit)
    {
        EnemyAttack attack = unit.GetUnitAttack<EnemyAttack>();
        EnemyMovement movement = unit.GetUnitMovement<EnemyMovement>();

        movement.StopMovemnet = true;

        Vector3[] tmp = new Vector3[]
        {
            new Vector3(0.5f, 0),
            new Vector3(-0.5f, 0),
            new Vector3(0, 0.5f),
            new Vector3(0, -0.5f),
        };

        for (int i = 0; i < 4; i++)
        {
            Bullet[] bullets = attack.publicAttack();

            for (int j = 0; j < bullets.Length; j++)
            {
                bullets[j].transform.position = unit.transform.position + tmp[i];
            }
        }

        movement.StopMovemnet = false;

        yield return new WaitForSeconds(0.1f);
    }
}
