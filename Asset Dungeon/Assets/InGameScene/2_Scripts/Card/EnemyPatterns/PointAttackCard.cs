using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAttackCard : PatternCard {

    protected override void Init()
    {
        name = "PointAttackCard";
        property = Properties.Pattern;
        restriction = Restrictions.Enemy;
    }

    public override IEnumerator Pattern(EnemyUnit unit)
    {
        return PointAttack(unit);
    }

    public IEnumerator PointAttack(EnemyUnit unit)
    {
        EnemyAttack attack = unit.GetUnitAttack<EnemyAttack>();
        EnemyMovement movement = unit.GetUnitMovement<EnemyMovement>();

        movement.StopMovemnet = true;

        int tmp = Random.Range(0, 2);
        for (int i = 0; i < 5; i++)     //총알갯수
        {
            Bullet[] bullets = attack.publicAttack();

            for (int j = 0; j < bullets.Length; j++)
            {
                bullets[j].Degree = attack.Degree2PlayerUnit + (tmp == 0 ? 5f : -5f);
            }
            yield return new WaitForSeconds(0.2f);
        }

        movement.StopMovemnet = false;
    }
}
