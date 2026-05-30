using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Attack
 * Unit의 하위 컴포넌트
 * Unit의 공격에 대한 부분을 구현한다
 * 각각의 Unit에 맞게 상속하여 구현 할 수 있을듯 싶다(??)
 */
public abstract class UnitAttack : ChildUnitInterface
{
    private SpriteRenderer spriteRenderer;
    private Renderer renderer;

    private bool delay;

    private bool reload;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer = GetComponent<Renderer>();
    }

    protected virtual Bullet[] Attack()
    {
        if (delay && Unit.Magazine != -1)
            return null;

        if(Unit.Magazine == -1)
        {

        }
        else if (0 < this.Unit.Magazine && !reload)
        {
            this.Unit.Magazine -= 1;
        }
        else if (!reload)
        {
            StartCoroutine(Reloading());

            return null;
        }
        else
            return null;

        Bullet[] bullets = new Bullet[1];

        Card card = Unit.Cards.FindCard(Card.Properties.Weapon);

        if (card != null)
        {
            bullets = card.GetCard<WeaponCard>().Attack(Unit);
        }
        else
        {
            bullets = new Bullet[Unit.Weapon.BulletNum];

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = BulletManager.Instance.Create();
                bullets[i].Init(Unit);
            }
        }

        for (int i = 0; i < bullets.Length; i++)
        {
            if (Unit.unitName == "Player")
            {
                bullets[i].transform.position = transform.GetChild(0).transform.position;
                //bullets[i].transform.position += transform.rotation * (Vector3)Unit.Weapon.Bullet_Pos;
                Unit.Animator.PlayGunShot();
            }
        }

        #region RevolutionCard_Check

        card = Unit.Cards.FindCard("RevolutionCard");

        if (card != null && card.CompareUnitType(Unit.Status.UnitType))
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                RevolutionObject obj = card.GetCard<RevolutionCard>().Revolution(bullets[i], Vector2.zero, bullets[i].Speed, Unit.transform, 3f + Random.Range(-1f, 0f));
            }
        }

        #endregion

        StartCoroutine(Delaing());
        Unit.Animator.playAudio(Unit.Weapon.Sound);
        Unit.Animator.WeaponShot();

        if (Unit.unitName == "Player")
            GameManager.Instance.RefreshPlayerState();

        return bullets;
    }

    protected IEnumerator Reloading()
    {
        float time = Time.time + Unit.Weapon.ReloadTime;

        float reloadvalue = (1f - GameManager.Instance.magazine_bar.fillAmount) / (Unit.Weapon.ReloadTime * 53.5f);

        reload = true;

        Unit.Animator.WeaponReload();


        while (true)
        {
            if (time < Time.time)
                break;

            GameManager.Instance.magazine_bar.fillAmount += reloadvalue;

            yield return new WaitForSeconds(0.01f);
        }


        this.Unit.Magazine = this.Unit.Weapon.Magazine;

        if (Unit.unitName == "Player")
            GameManager.Instance.RefreshPlayerState();

        Unit.Animator.WeaponChange();
    
        reload = false;
    }

    IEnumerator Delaing()
    {
        delay = true;
        yield return new WaitForSeconds(Unit.Weapon.Delay);

        delay = false;
    }

    public Bullet[] publicAttack()
    {
        return Attack();
    }

    protected virtual void WeaponDegreeSet()
    {

    }

    #region Property
    public Sprite UnitAttackSprite
    {
        get
        {
            return spriteRenderer.sprite;
        }
        set
        {
            spriteRenderer.sprite = value;
        }
    }

    public SpriteRenderer UnitAttackSpriteRenderer
    {
        get
        {
            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
    }

    public int SortingLayer
    {
        get
        {
            return renderer.sortingOrder;
        }
        set
        {
            renderer.sortingOrder = value;
        }
    }

    public bool Reload
    {
        get
        {
            return reload;
        }
    }
#endregion
}
