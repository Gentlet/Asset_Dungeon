using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunTypeUI : SingletonGameObject<GunTypeUI>
{

    private Unit player;
    private Card weapon;
    private Card attack;

    public Sprite[] GunSprites;
    public GameObject[] Holo_Prefab;

    public GameObject HOLO;

    Sprite GunTypeImage;

    Craft_Drag[] CD = new Craft_Drag[3];

    GameObject holo;
    int holonum;


    // Use this for initialization
    void Start()
    {
        player = UnitManager.Instance.GetUnit("Player");
        //GunStart();
        GunSet();

        
    }

    // Update is called once per frame
    void Update()
    {
        GunChange();
    }

    void GunChange()
    {
        weapon = player.Cards.FindCard(Card.Properties.Weapon);
        attack = player.Cards.FindCard(Card.Properties.Attack);

        if(weapon != null)
        {
            if (weapon.Name == "AssaultRifle")
            {
                if (holonum != 1)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);

                    GetComponent<Image>().sprite = GunSprites[9];
                    holo = Instantiate(Holo_Prefab[9], HOLO.transform);

                    holonum = 1;
                }
            }

            if (weapon.Name == "HandGun")
            {
                if (holonum != 2)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);

                    GetComponent<Image>().sprite = GunSprites[10];
                    holo = Instantiate(Holo_Prefab[10], HOLO.transform);

                    holonum = 2;
                }
            }

            if (weapon.Name == "ShotGun")
            {
                if (holonum != 3)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[11];
                    holo = Instantiate(Holo_Prefab[11], HOLO.transform);

                    holonum = 3;
                }

            }
        }

        if (weapon != null && attack != null)
        {
            if (weapon.Name == "AssaultRifle" && attack.Name == "ThunderCard")
            {
                if (holonum != 4)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[0];
                    holo = Instantiate(Holo_Prefab[0], HOLO.transform);

                    holonum = 4;
                }
                
            }

            if (weapon.Name == "AssaultRifle" && attack.Name == "FlameCard")
            {
                if (holonum != 5)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[1];
                    holo = Instantiate(Holo_Prefab[1], HOLO.transform);

                    holonum = 5;
                }
                
            }

            if (weapon.Name == "AssaultRifle" && attack.Name == "FreezeCard")
            {
                if (holonum != 6)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[2];
                    holo = Instantiate(Holo_Prefab[2], HOLO.transform);

                    holonum = 6;
                }
            }

            if (weapon.Name == "HandGun" && attack.Name == "ThunderCard")
            {
                if (holonum != 7)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[3];
                    holo = Instantiate(Holo_Prefab[3], HOLO.transform);

                    holonum = 7;
                }
            }

            if (weapon.Name == "HandGun" && attack.Name == "FlameCard")
            {
                if (holonum != 8)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[4];
                    holo = Instantiate(Holo_Prefab[4], HOLO.transform);

                    holonum = 8;
                }
            }

            if (weapon.Name == "HandGun" && attack.Name == "FreezeCard")
            {
                if (holonum != 9)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[5];
                    holo = Instantiate(Holo_Prefab[5], HOLO.transform);

                    holonum = 9;
                }
            }

            if (weapon.Name == "ShotGun" && attack.Name == "ThunderCard")
            {
                if (holonum != 10)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[6];
                    holo = Instantiate(Holo_Prefab[6], HOLO.transform);

                    holonum = 10;
                }
            }

            if (weapon.Name == "ShotGun" && attack.Name == "FlameCard")
            {
                if (holonum != 11)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[7];
                    holo = Instantiate(Holo_Prefab[7], HOLO.transform);

                    holonum = 11;
                }
            }

            if (weapon.Name == "ShotGun" && attack.Name == "FreezeCard")
            {
                if (holonum != 12)
                {
                    if (holo != null)
                        Destroy(holo.gameObject);


                    GetComponent<Image>().sprite = GunSprites[8];
                    holo = Instantiate(Holo_Prefab[8], HOLO.transform);

                    holonum = 12;
                }
            }
        }
        GetComponent<Image>().SetNativeSize();
    }

    public void GunSet()
    {
        CD[0] = CraftTable_Manager.Instance.Slot_Transform[0].GetChild(0).GetComponent<Craft_Drag>();
        CD[1] = CraftTable_Manager.Instance.Slot_Transform[1].GetChild(0).GetComponent<Craft_Drag>();
        CD[2] = CraftTable_Manager.Instance.Slot_Transform[2].GetChild(0).GetComponent<Craft_Drag>();

        if (CD[0] != null)
        {
            if (CD[0].OriginalName == "Riple")
            {
                player.Cards[0] = CardManager.Instance.GetCard("AssaultRifle");
                player.WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (CD[0].OriginalName == "Shotgun")
            {
                player.Cards[0] = CardManager.Instance.GetCard("ShotGun");
                player.WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }

            if (CD[0].OriginalName == "HandGun")
            {
                player.Cards[0] = CardManager.Instance.GetCard("HandGun");
                player.WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
        }

        else if (CD[0] == null)
        {
            player.Cards[0] = CardManager.Instance.GetCard("HandGun");
            player.WeaponSetting();
            GameManager.Instance.RefreshPlayerState();
        }

        if (CD[1] != null)
        {
            if (CD[1].OriginalName == "Blaze")
            {
                player.Cards[1] = CardManager.Instance.GetCard("FlameCard");
                player.WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }

            if (CD[1].OriginalName == "Freeze")
            {
                player.Cards[1] = CardManager.Instance.GetCard("FreezeCard");
                player.WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }

            if (CD[1].OriginalName == "Thunder")
            {
                player.Cards[1] = CardManager.Instance.GetCard("ThunderCard");
                player.WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
        }

        else if (CD[1] == null)
        {
            player.Cards[1] = null;
            player.WeaponSetting();
            GameManager.Instance.RefreshPlayerState();
        }

        if (CD[2] != null)
        {
            if (CD[2].OriginalName == "Reflect")
            {
                player.Cards[2] = CardManager.Instance.GetCard("ReflectCard");
                player.WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (CD[2].OriginalName == "Screw")
            {
                player.Cards[2] = CardManager.Instance.GetCard("RevolutionCard");
                player.WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
        }
        else if (CD[2] == null)
        {
            player.Cards[2] = null;
            player.WeaponSetting();
            GameManager.Instance.RefreshPlayerState();
        }

        #region GunChange

        

        #endregion
    }

    void GunStart()
    {
        player.Cards[0] = CardManager.Instance.GetCard("HandGun");
        player.WeaponSetting();
        GameManager.Instance.RefreshPlayerState();

        player.Cards[1] = null;
        player.WeaponSetting();
        GameManager.Instance.RefreshPlayerState();

        player.Cards[2] = null;
        player.WeaponSetting();
        GameManager.Instance.RefreshPlayerState();
    }
}
