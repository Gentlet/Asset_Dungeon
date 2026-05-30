using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * UnityStatus : public
 * 설명 :
 *      Unit의 기본 스테이터스를 저장한다.
 * 멤버 변수 :
 *      name       : string            private                  해당 unit의 이름
 *      hp         : float             private                  해당 unit의 체력
 *      speed      : float             private                  해당 unit의 이동속도
 *      unitType   : UnitStatus.Type   private                  Unit의 타입
 *  
 *  ENUM 
 *  Type     
 *      Player                    플레이어
 *      Enemy                     적
 *      Neutral                   중립
 */

[System.Serializable]
public class UnitStatus
{
    public enum Type
    {
        Player,
        Enemy,
        Neutral,
    }

    [SerializeField]
    private string name = null;
    [SerializeField]
    private string weapon_name;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;

    [SerializeField]
    private CardManager.CardNames[] basiccards;

    [SerializeField]
    private Type unitType;

    public UnitStatus(UnitStatus status)
    {
        name = status.Name;
        weapon_name = status.Weapon_Name;
        hp = status.Hp;
        speed = status.Speed;
        damage = status.Damage;
        unitType = status.UnitType;
    }
    

    #region Properties         
    public string Name
    {
        get
        {
            return name;
        }
    }

    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
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

    public Type UnitType
    {
        get
        {
            return unitType;
        }
    }

    public string Weapon_Name
    {
        get
        {
            return weapon_name;
        }
    }

    public CardManager.CardNames[] BasicCards
    {
        get
        {
            return basiccards;
        }
    }
    #endregion
}

/*
 * StatusManager : public
 * 설명 :
 *      인스펙터에서 UnitStatus의 정보들을 배열에 넣어 이름으로 해당 유닛의 스테이터스를 불러오는 매니저
 * 멤버 변수 :
 *      unitsStatus       : UnitStatus[]        public          Unit들의 정보
 * 멤버 함수 :
 *      getUnitStatus              인자 값으로 넘어온 유닛의 이름으로 배열 내에 있는 유닛 스테이터스를 반환한다.
 *          매개 변수 
 *              name : string
 *          반환 값
 *              UnitStatus
 *      
 *  
 */
public class StatusManager : SingletonGameObject<StatusManager>
{
    public UnitStatus[] unitStatus;

    public UnitStatus GetUnitStatus(string name)
    {
        for (int i = 0; i < unitStatus.Length; i++)
        {
            if (name == unitStatus[i].Name)
            {
                return unitStatus[i];
            }
        }

        throw new System.Exception("\'" + name + "\'" + "에 맞는 유닛의 스테이터스를 불러올 수 없음!");
    }
}
