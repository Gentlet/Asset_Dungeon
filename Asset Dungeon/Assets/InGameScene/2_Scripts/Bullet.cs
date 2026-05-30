using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour {

    public bool revolutionbullet;

    private bool active;
    private bool notcrush;
    private bool moveactive;

    private Unit connected_unit;
    private UnitStatus.Type connedted_unit_type;
    private Card[] connected_cards;

    private Vector3 direction;
    private Rigidbody2D rigid;
    private CircleCollider2D collider;
    private SpriteRenderer spriteRenderer;

    private float angle;

    private float speed;
    private float damage;
    private float degree;


#region Card변수들
    public int reflectcount;

    #endregion



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (notcrush)
            return;

        if (collision.CompareTag("Wall"))
        {

            if (reflectcount != -1 && ConnectedUnit != null)
            {
                #region ReflectCard_Check
                Card card = ConnectedUnit.Cards.FindCard("ReflectCard");

                if (card != null && card.CompareUnitType(connected_unit.Status.UnitType))
                    card.GetCard<ReflectCard>().Reflect(this);
                #endregion
            }
            else
            {
                GameManager.Instance.PlayWallCrushParticle(transform.position);

                Active = false;
            }
        }
    }

    private void Awake()
    {
        connected_cards = new Card[3];

        moveactive = true;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsInTheRoom(transform.position))
            Active = false;

    }

    private void FixedUpdate()
    {
        if (CursorManager.Instance.GameisRunning && moveactive)
        {
            //Debug.Log("direction : " + direction);
            //Debug.Log("speed : " + speed);

            if (connected_unit == null)
                return;

            Rigid.velocity = direction * speed;
        }
        else
            Rigid.velocity = Vector3.zero;
    }

    public void Init(Unit unit)
    {
        connected_unit = unit;
        connedted_unit_type = unit.Status.UnitType;
        connected_cards = connected_unit.Cards;
        SpriteRenderer.sprite = unit.Weapon.BulletSprite;

        transform.position = connected_unit.Attack.transform.position + (connected_unit.Attack.transform.rotation * new Vector3(1, 0, 0));
        transform.localScale = Vector3.one * 1.5f;
        transform.rotation = connected_unit.Attack.transform.rotation;

        Direction = unit.Attack.transform.right;

        speed = unit.Weapon.Speed;
        damage = unit.Weapon.Damage;
        Degree = transform.position.Angle(transform.position + (Vector3)direction) + Random.Range(connected_unit.Weapon.Accuracy * -1f, connected_unit.Weapon.Accuracy);

        if (ConnectedUnit.Cards.FindCard("ReflectCard") != null)
            reflectcount = ConnectedUnit.Weapon.ReflectCount;
        else
            reflectcount = -1;
    }

    #region Properties

    public bool NotCrush
    {
        get
        {
            return notcrush;
        }
        set
        {
            notcrush = value;
        }
    }

    public Unit ConnectedUnit
    {
        get
        {
            return connected_unit;
        }
    }

    public UnitStatus.Type ConnectedUnitType
    {
        get
        {
            return connedted_unit_type;
        }
    }

    public Card[] ConnectedCards
    {
        get
        {
            return connected_cards;
        }
    }

    public bool Active
    {
        get
        {
            return active;
        }
        set
        {
            if (!value)
                connected_unit = null;

            active = value;

            gameObject.SetActive(value);
        }
    }

    public bool MoveActive
    {
        get
        {
            return moveactive;
        }
        set
        {
            moveactive = value;
        }
    }

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value.normalized;

            angle = Vector3.zero.Angle(Direction);
        }
    }

    public float Angle
    {
        get
        {
            return angle;
        
        }
    }

    public float Degree
    {
        get
        {
            return degree;
        }
        set
        {
            degree = value % 360;

            Direction = this.Angle2Direcrion(degree);
        }
    }

    public CircleCollider2D BulletCollider
    {
        get
        {
            return Collider;
        }
    }

    public Rigidbody2D Rigid
    {
        get
        {
            if (rigid == null)
                rigid = GetComponent<Rigidbody2D>();

            return rigid;
        }
    }

    public CircleCollider2D Collider
    {
        get
        {
            if (collider == null)
                collider = GetComponent<CircleCollider2D>();

            return collider;
        }
    }

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            return spriteRenderer;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    public float Damage
    {
        get
        {
            return damage;
        }
    }
    #endregion
}
