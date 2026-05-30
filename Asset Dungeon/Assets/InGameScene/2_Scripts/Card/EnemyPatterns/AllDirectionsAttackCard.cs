using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDirectionsAttackCard : PatternCard
{
    protected override void Init()
    {
        name = "AllDirectionsAttackCard";
        property = Properties.Pattern;
        restriction = Restrictions.Enemy;
    }

    public override IEnumerator Pattern(EnemyUnit unit)
    {
        return AllDirectionsAttack(unit);
    }

    public IEnumerator AllDirectionsAttack(EnemyUnit unit)
    {
        EnemyAttack attack = unit.GetUnitAttack<EnemyAttack>();
        EnemyMovement movement = unit.GetUnitMovement<EnemyMovement>();

        movement.StopMovemnet = true;
        
        for (int i = 0; i < 12; i++)
        {
            Bullet[] bullets = attack.publicAttack();

            for (int j = 0; j < bullets.Length; j++)
            {
                bullets[j].transform.position = unit.transform.position;        //몸통에서쏘기

                float angle = 360f / 12f * i * Mathf.Deg2Rad;
                bullets[j].Direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            }
        }

        movement.StopMovemnet = false;

        yield return new WaitForSeconds(0.2f);
    }
}
