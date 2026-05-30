using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터 (ex> Player Enemy NPC등) 을 만들때는 이거 상속받아서 새로 만드시길 추천합니다.
public abstract class Unit : MonoBehaviour
{
    public string unitName;

    private int money;
    private int magazine;

    protected new Rigidbody2D rigidbody;

    protected SpriteRenderer spriterenderer;

    protected UnitStatus status;
    protected UnitAnimator animator;
    protected UnitAttack attack;
    protected UnitMovement movement;

    protected Weapon weapon;
    protected Card[] cards;

    protected bool stun;

    protected bool dontdie;

    [SerializeField]
    private CardManager.CardNames[] newcards;

    private void Awake()
    {
        cards = new Card[3];

        spriterenderer = transform.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();

            if (!Animator.RollingTrigger && bullet.ConnectedUnitType != status.UnitType)
            {
                bullet.Active = false;

                if (!dontdie)
                    Status.Hp -= bullet.Damage;

                #region AttackCard

                Card card = bullet.ConnectedCards.FindCard(Card.Properties.Attack);

                if (card != null)
                {
                    StartCoroutine(card.GetCard<AttackCard>().AttackActive(this));
                }

                #endregion

                if (unitName == "Player")
                {
                    PlayerStatusRefresh();
                }

                StopCoroutine("hithit");
                StartCoroutine(hithit(bullet));


                if (Status.Hp < 0)
                {
                    Dead();
                }
            }
        }
    }

    protected virtual void OnEnable()
    {
        UnitManager.Instance.AddUnit(this);

        status = new UnitStatus(StatusManager.Instance.GetUnitStatus(unitName));
        weapon = WeaponManager.Instance.GetWeapon(status.Weapon_Name);

        magazine = weapon.Magazine;

        //하위 컴포넌트 초기화
        {
            rigidbody = GetComponent<Rigidbody2D>();

            animator = transform.GetComponent<UnitAnimator>();
            attack = transform.GetChild(0).GetComponent<UnitAttack>();
            movement = GetComponent<UnitMovement>();

            if (attack == null)
                attack = transform.GetComponent<UnitAttack>();

            if(animator != null) animator.ConnectUnit(this);
            attack.ConnectUnit(this);
            movement.ConnectUnit(this);

            //카드셋팅
            if (status.BasicCards != null)
            {
                for (int i = 0; i < status.BasicCards.Length; i++)
                {
                    cards[i] = CardManager.Instance.GetCard(status.BasicCards[i]);
                }
            }

            if (newcards != null)
            {
                for (int i = 0; i < newcards.Length; i++)
                {
                    Card tmp = CardManager.Instance.GetCard(newcards[i]);

                    if (tmp != null)
                        cards[i] = tmp;
                }
            }
        }
    }

    private void Start()
    {
        WeaponSetting();
    }

    private void Update()
    {
        uDead();
    }

    public virtual void uDead()
    {
        if (status.Hp < 0f)
            Dead();
    }

    public void pDead()
    {
        Dead();
    }

    protected virtual void Dead()
    {
        UnitManager.Instance.RemoveUnit(this);

        animator.DeadAnimation();
    }

    public void WeaponSetting()
    {
        if (attack.gameObject == gameObject)
            return;

        for (int i = 0; i < cards.Length; i++)
        {
            if(cards[i] != null && cards[i].Property == global::Card.Properties.Weapon)
            {
                cards[i].GetCard<WeaponCard>().Mount(this);
                return;
            }
        }
        
        Weapon = WeaponManager.Instance.GetWeapon(status.Weapon_Name);
    }

    protected virtual IEnumerator hithit(Bullet bullet)
    {
        for (int i = 0; i < 3; i++)
        {
            Color color = spriterenderer.color;

            color.a = 0.5f;

            spriterenderer.color = color;

            yield return new WaitForSeconds(0.05f);

            color.a = 1;

            spriterenderer.color = color;

            yield return new WaitForSeconds(0.1f);
        }
    }

    public virtual void PlayerStatusRefresh()
    {

    }


    #region Properties
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }

    public int Magazine
    {
        get
        {
            return magazine;
        }
        set
        {
            magazine = value;
        }
    }

    public bool Stun
    {
        get
        {
            return stun;
        }
        set
        {
            stun = value;
        }
    }

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return spriterenderer;
        }
    }

    public UnitAnimator Animator
    {
        get
        {
            return animator;
        }
    }

    public UnitAttack Attack
    {
        get
        {
            return attack;
        }
    }

    public UnitMovement Movement
    {
        get
        {
            return movement;
        }
    }

    public UnitStatus Status
    {
        get
        {
            return status;
        }
    }

    public Weapon Weapon
    {
        get
        {
            return weapon;
        }
        set
        {
            attack.UnitAttackSprite = value.WeaponSprite;

            weapon = value;
        }
    }

    public Card[] Cards
    {
        get
        {
            return cards;
        }
    }

    public Rigidbody2D Rigid
    {
        get
        {
            return rigidbody;
        }
    }
    #endregion
}
