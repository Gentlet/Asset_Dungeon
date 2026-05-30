using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card
{
    public enum Restrictions
    {
        None,
        Player,
        Enemy,
    }

    public enum Properties
    {
        Bullet,
        Weapon,
        Attack,

        Pattern,
    }

    private int cardnumber = -1;

    protected string name;
    protected Properties property;
    protected Restrictions restriction;

    protected virtual void Init()
    {

    }

    public bool CompareUnitType(UnitStatus.Type type)
    {
        if (restriction == Restrictions.None || type.ToString() == restriction.ToString())
            return true;

        return false;
    }

    public void SetCardNumber()
    {
        Init();

        if (cardnumber == -1)
        {
            for (int i = 0; i < CardManager.Instance.Cards.Length; i++)
            {
                if (CardManager.Instance.GetCard(i) == null)
                {
                    cardnumber = i;
                    break;
                }
            }
        }
    }

    #region Property

    public int Number
    {
        get
        {
            return cardnumber;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }
    public Properties Property
    {
        get
        {
            return property;
        }
    }

    public Restrictions Restriction
    {
        get
        {
            return restriction;
        }
    }
    #endregion
}
