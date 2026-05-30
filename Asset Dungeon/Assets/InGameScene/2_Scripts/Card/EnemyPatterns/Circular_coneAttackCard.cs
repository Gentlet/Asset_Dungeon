using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circular_coneAttackCard : PatternCard {
    int[] a = new int[30];
    protected override void Init()
    {
        name = "Circular_coneAttackCard";
        property = Properties.Pattern;
        restriction = Restrictions.Enemy;
    }

    public override IEnumerator Pattern(EnemyUnit unit)
    {
        return Circular_coneAttack(unit);
    }

    public IEnumerator Circular_coneAttack(EnemyUnit unit)
    {
        EnemyAttack attack = unit.GetUnitAttack<EnemyAttack>();
        EnemyMovement movement = unit.GetUnitMovement<EnemyMovement>();

        movement.StopMovemnet = true;
        for (int i = 0; i < 30; i++)
        {
            for (int z = 1; z <= 2; z++)
            {
                yield return new WaitForSeconds(0.01f);
                if (i == 0 && z == 1)
                    continue;

                Bullet[] bulletss = attack.publicAttack();

                for (int j = 0; j < bulletss.Length; j++)
                {
                    bulletss[j].Degree = attack.Degree2PlayerUnit;
                    if (z == 0)
                        bulletss[j].transform.position = unit.transform.position;
                    else if (z == 1)
                    {
                        bulletss[j].transform.position =  unit.transform.position +  (unit.GetUnitAttack<EnemyAttack>().transform.rotation * new Vector3(0, -3 + (z * 2), 0));
                    }
                    else if (z == 2)
                    {
                        bulletss[j].transform.position =  unit.transform.position + (unit.GetUnitAttack<EnemyAttack>().transform.rotation * new Vector3(0, -6 + (z * 4), 0));
                    }
                }
            }
            yield return new WaitForSeconds(0.01f);
        }


        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < 30; i++)     //총알갯수
        {
            Bullet[] bullets = attack.publicAttack();

            for (int j = 0; j < bullets.Length; j++)
            {
                bullets[j].Degree = attack.Degree2PlayerUnit + ((i*3) - 45);
            }
        }

        movement.StopMovemnet = false;
    }
}
