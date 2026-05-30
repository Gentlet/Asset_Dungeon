using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCard : Card {

    public void Mount(Unit unit)
    {
        unit.Weapon = WeaponManager.Instance.GetWeapon(Name);
        unit.Magazine = unit.Weapon.Magazine;
        unit.Animator.WeaponChange();

        Card card = unit.Cards.FindCard(Card.Properties.Attack);

        if (card != null && card.Name == "FlameCard")
        {
            unit.SpriteRenderer.color = Color.red;
        }
        if (card != null && card.Name == "FreezeCard")
        {
            unit.SpriteRenderer.color = Color.blue;
        }
        if (card != null && card.Name == "ThunderCard")
        {
            unit.SpriteRenderer.color = Color.yellow;
        }
    }

    public virtual Bullet[] Attack(Unit unit)
    {
        Bullet[] bullets = new Bullet[unit.Weapon.BulletNum];

        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = BulletManager.Instance.Create();
            bullets[i].Init(unit);

            if (1 < bullets.Length) 
                bullets[i].Direction = bullets[i].Angle2Direcrion(bullets[i].Angle + Random.Range(-15f, 15f));

        }

        return bullets;
    }
}
