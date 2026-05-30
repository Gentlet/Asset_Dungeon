using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorFormAttackCard : PatternCard
{
    protected override void Init()
    {
        name = "SectorFormAttackCard";
        property = Properties.Pattern;
        restriction = Restrictions.Enemy;
    }

    public override IEnumerator Pattern(EnemyUnit unit)
    {
        return SectorFormAttack(unit);
    }

    public IEnumerator SectorFormAttack(EnemyUnit unit)
    {
        EnemyAttack attack = unit.GetUnitAttack<EnemyAttack>();
        EnemyMovement movement = unit.GetUnitMovement<EnemyMovement>();

        movement.StopMovemnet = true;

        for (int i = 0; i < 5; i++)
        {
            Bullet[] bullets = attack.publicAttack();

            for (int j = 0; j < bullets.Length; j++)
            {
                bullets[j].Degree = attack.Degree2PlayerUnit + (-20 + (i * 10));
            }
        }

        movement.StopMovemnet = false;

        yield return new WaitForSeconds(0.2f);
    }
}
