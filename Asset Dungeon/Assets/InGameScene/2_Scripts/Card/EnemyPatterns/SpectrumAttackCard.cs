using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumAttackCard : PatternCard
{
    protected override void Init()
    {
        name = "SpectrumAttackCard";
        property = Properties.Pattern;
        restriction = Restrictions.Enemy;
    }

    public override IEnumerator Pattern(EnemyUnit unit)
    {
        return SpectrumAttack(unit);
    }

    public IEnumerator SpectrumAttack(EnemyUnit unit)
    {
        EnemyAttack attack = unit.GetUnitAttack<EnemyAttack>();
        EnemyMovement movement = unit.GetUnitMovement<EnemyMovement>();

        movement.StopMovemnet = true;
        
        for (int i = 0; i < 2; i++)     //16발
        {
            for (int j = -4; j < 4; j++)
            {
                Bullet[] bullets = attack.publicAttack();

                for (int z = 0; z < bullets.Length; z++)
                {
                    bullets[z].Degree = attack.Degree2PlayerUnit + (j * 3);
                }

                yield return new WaitForSeconds(0.05f);
            }

            for (int j = 4; -4 < j; j--)
            {
                Bullet[] bullets = attack.publicAttack();

                for (int z = 0; z < bullets.Length; z++)
                {
                    bullets[z].Degree = attack.Degree2PlayerUnit + (j * 3);
                }

                yield return new WaitForSeconds(0.05f);
            }
        }

        movement.StopMovemnet = false;
    }
}
