using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon {
    [SerializeField]
    private string name;
    [SerializeField]
    private Sprite weapon_sprite;
    [SerializeField]
    private Sprite bullet_sprite;
    [SerializeField]
    private Vector2 bullet_pos;
    [SerializeField]
    private AudioClip sound;
    [SerializeField]
    private float speed;
    //[SerializeField]
    //private float covariance;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float weight;
    [SerializeField]
    private float delay;
    [SerializeField]
    private int magazine;
    [SerializeField]
    private float reloadtime;
    [SerializeField]
    private int reflectcont;
    [SerializeField]
    private float accuracy;
    [SerializeField]
    private int bulletnum;

    #region Properties         
    public string Name
    {
        get
        {
            return name;
        }
    }

    public Sprite WeaponSprite
    {
        get
        {
            return weapon_sprite;
        }
    }

    public Sprite BulletSprite
    {
        get
        {
            return bullet_sprite;
        }
    }

    public Vector2 Bullet_Pos
    {
        get
        {
            return bullet_pos;
        }
    }

    public AudioClip Sound
    {
        get
        {
            return sound;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    //public float Covariance
    //{
    //    get
    //    {
    //        return covariance;
    //    }
    //}

    public float Damage
    {
        get
        {
            return damage;
        }
    }

    public float Weight
    {
        get
        {
            return weight;
        }
    }

    public float Delay
    {
        get
        {
            return delay;
        }
    }

    public int Magazine
    {
        get
        {
            return magazine;
        }
    }

    public float ReloadTime
    {
        get
        {
            return reloadtime;
        }
    }

    public int ReflectCount
    {
        get
        {
            return reflectcont;
        }
    }

    public float Accuracy
    {
        get
        {
            return accuracy;
        }
    }

    public int BulletNum
    {
        get
        {
            return bulletnum;
        }
    }
    #endregion
}


public class WeaponManager : SingletonGameObject<WeaponManager>
{
    public Weapon[] weapons;

    public Weapon GetWeapon(string name)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (name == weapons[i].Name)
            {
                return weapons[i];
            }
        }

        throw new System.Exception("\'" + name + "\'" + "에 맞는 무기를 불러올 수 없음!");
    }
}

