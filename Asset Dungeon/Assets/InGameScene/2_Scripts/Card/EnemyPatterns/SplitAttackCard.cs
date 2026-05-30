using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitAttackCard : PatternCard
{
    protected override void Init()
    {
        name = "SplitAttackCard";
        property = Properties.Pattern;
        restriction = Restrictions.Enemy;
    }

    public override IEnumerator Pattern(EnemyUnit unit)
    {
        return SplitAttack(unit);
    }

    public IEnumerator SplitAttack(EnemyUnit unit)
    {
        EnemyAttack attack = unit.GetUnitAttack<EnemyAttack>();

        for (int i = 0; i < 5; i++)     //총알갯수
        {
            Bullet[] bullet = attack.publicAttack();

            for (int j = 0; j < bullet.Length; j++)
            {
                if (i % 2 == 0)     //짝수
                {
                    bullet[j].Degree = attack.Degree2PlayerUnit + 5f;
                }
                else
                {
                    bullet[j].Degree = attack.Degree2PlayerUnit - 5f;
                }
            }

            yield return new WaitForSeconds(0.2f);
        }

        unit.GetUnitMovement<EnemyMovement>().StopMovemnet = false;
    }
}
