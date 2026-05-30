using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {

    public static InGameUI ingameui;

    public PlayerUnit player;

    GameObject CraftTable;
    GameObject Shop; 
    GameObject Inventory;
    GameObject Storage;
    GameObject IngameUI;
    GameObject MoneyText;
    GameObject PauseUI;
    GameObject OptionUI;
    GameObject GameOver;
    public GameObject BI;

    public GameObject[] Craft_page;
    public GameObject[] Craft_Btns;

    public GameObject[] UIs;

    Transform[] SaveInventory;

    public GameObject[] Item;

	// Use this for initialization
	void Start () {
        ingameui = this;

        IngameUI = transform.Find("InGameUI").gameObject;
        Storage = transform.Find("StorageUI").gameObject;
        Inventory = transform.Find("InventoryUI").gameObject;
        Shop = transform.Find("ShopUI").gameObject;
        CraftTable = transform.Find("CraftTable").gameObject;
        MoneyText = IngameUI.transform.Find("MoneyText").gameObject;
        PauseUI = transform.Find("PauseUI").gameObject;
        OptionUI = transform.Find("Option").gameObject;
        GameOver = transform.Find("GameOverUI").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
            Inventory_On();

        if (Input.GetKeyDown(KeyCode.T))
            CraftTable_On();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ShortCutDown(1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ShortCutDown(2);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            ShortCutDown(3);

        if(Input.GetMouseButtonDown(0) && UIs[8].transform.GetChild(0).GetComponent<NotificationUI>().FadeIn == true)
        {
            Go_Main();
        }

        MoneyTextUpdate();
        BlackImage();
    }

    void MoneyTextUpdate()
    {
        //MoneyText.GetComponent<Text>().text = " " + UnitManager.Instance.GetUnitofName("Player").Money;
    }

    public void CraftTable_On()
    {
        CraftTable.SetActive(true);
    }

    public void CraftTable_Off()
    {
        CraftTable.SetActive(false);
    }

    public void Shop_On()
    {
        Shop.SetActive(true);
        Inventory_On();
    }

    public void Shop_Off()
    {
        Shop.SetActive(false);
    }

    public void GameOver_On()
    {
        GameOver.SetActive(true);
    }

    void Storage_On()
    {
        Inventory.SetActive(true);
        Storage.SetActive(true);

        for (int i = 0; i < Inventory_Manager.Instance.Slot_Transform.Length; i++)
        {
            if(Inventory_Manager.Instance.SlotF[i] == true)
            {
                Inventory_Manager.Instance.SlotF[i] = false;
            }
        }
    }

    public void Pause_On()
    {
        PauseUI.SetActive(true);
    }

    public void Pause_Down()
    {
        PauseUI.SetActive(false);
    }

    public void Go_Window()
    {
        Application.Quit();
    }

    public void Go_Main()
    {
        SceneManager.LoadScene(0);
        SoundMng.Instance.BGM_Change(0);
    }

    public void Option()
    {
        OptionUI.SetActive(true);
    }

    public void Inventory_On()
    {
        Inventory.SetActive(true);

        //Inventory_Manager.Instance.Slot_Transform = SaveInventory;

        //Transform temp;
        //int minimum = 0;
        //int ay = 0;

        //for (int i = 0; i < Inventory_Manager.Instance.Slot_Transform.Length; i++)
        //{
        //    if (Inventory_Manager.Instance.SlotF[i])
        //    {
        //        if (Inventory_Manager.Instance.SlotF[minimum] == false)
        //        {
        //            temp = Inventory_Manager.Instance.Slot_Transform[i];
        //            Inventory_Manager.Instance.Slot_Transform[minimum] = temp;
        //            Inventory_Manager.Instance.Slot_Transform[i] = Inventory_Manager.Instance.Slot_Transform[minimum];
        //            ay++;
        //        }
        //        else
        //        {
        //            minimum += 1;
        //            continue;
        //        }
        //    }
        //}
    }

    public void Inventory_Off()
    {
        Inventory.SetActive(false);

        SaveInventory = new Transform[18];

        for (int i = 0; i < Inventory_Manager.Instance.Slot_Transform.Length; i++)
        {
            if (Inventory_Manager.Instance.SlotF[i])
                for (int j = 0; j < SaveInventory.Length; j++)
                {
                    if (SaveInventory[j] == null)
                        SaveInventory[j] = Inventory_Manager.Instance.Slot_Transform[i];
                }
        }
    } 

    public void Storage_Off()
    {
        Storage.SetActive(false);
    }

    public void Buy_1(int Item_Num)
    {
        SoundMng.Instance.SFX_Play(4);

        GameObject new_Item = Instantiate(Item[Item_Num]);

        Invoke("GetItem", 0.5f);

        int fuck;
        int nullNum;

        fuck = 1;
        nullNum = 0;

        for (int i = 0; i < Inventory_Manager.Instance.Slot_Transform.Length; i++)
        {
            if (i < Inventory_Manager.Instance.Slot_Transform.Length - 1)
            {
                if (Inventory_Manager.Instance.SlotF[i] == true && Inventory_Manager.Instance.Slot_Transform[i].CompareTag("Inventory_Slot"))
                {
                    if (new_Item.GetComponent<Drag>().Item_num == Inventory_Manager.Instance.Slot_Transform[i].
                    GetChild(0).GetComponent<Drag>().Item_num)
                    {
                        fuck = 2;
                        nullNum = i;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
            else
            {
                switch (fuck)
                {
                    case 1:
                        for (int j = 0; j < Inventory_Manager.Instance.Slot_Transform.Length; j++)
                        {
                            if (Inventory_Manager.Instance.SlotF[j] == false && 
                                Inventory_Manager.Instance.Slot_Transform[j].CompareTag("Inventory_Slot"))
                            {
                                new_Item.transform.SetParent(Inventory_Manager.Instance.Slot_Transform[j]);
                                new_Item.transform.localPosition = new Vector2(0, 0);
                                new_Item.name = "Item_YEZE";
                                break;
                            }
                        }
                        break;
                    case 2:
                        if (Inventory_Manager.Instance.Slot_Transform[nullNum].CompareTag("Inventory_Slot"))
                        {
                            Destroy(new_Item);
                            Inventory_Manager.Instance.Slot_Transform[nullNum].GetChild(0).GetComponent<Drag>().NumOfIt += 1;
                        }
                        break;
                }

            }
        }
    }

    public void Buy_2(int price)
    {
        //if (UnitManager.Instance.GetUnitofName("Player").Money > price)
            //UnitManager.Instance.GetUnitofName("Player").Money -= price;
    }

    public void GetItem()
    {
        SoundMng.Instance.SFX_Play(5);
    }

    public void ShortCutDown(int n)
    {
        switch (n)
        {
            case 1:
                for (int i = 0; i < Inventory_Manager.Instance.Slot_Transform.Length; i++)
                {
                    if (Inventory_Manager.Instance.Slot_Transform[i].name == "ItemSlot_1")
                        if (Inventory_Manager.Instance.SlotF[i] == true)
                        {
                            Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>()
                                .RealMe.GetComponent<Drag>().NumOfIt -= 1;
                            UseItem(Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>().Item_num);

                            if (Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>()
                                .RealMe.GetComponent<Drag>().NumOfIt < 1)
                            {
                                Destroy(Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>()
                                .RealMe);
                            }
                        }
                }
                break;
            case 2:
                for (int i = 0; i < Inventory_Manager.Instance.Slot_Transform.Length; i++)
                {
                    if (Inventory_Manager.Instance.Slot_Transform[i].name == "ItemSlot_2")
                        if (Inventory_Manager.Instance.SlotF[i] == true)
                        {
                            Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>()
                                .RealMe.GetComponent<Drag>().NumOfIt -= 1;

                            if (Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>()
                                .RealMe.GetComponent<Drag>().NumOfIt < 1)
                            {
                                Destroy(Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>()
                                .RealMe);
                            }
                        }
                }
                break;
            case 3:
                for (int i = 0; i < Inventory_Manager.Instance.Slot_Transform.Length; i++)
                {
                    if (Inventory_Manager.Instance.Slot_Transform[i].name == "ItemSlot_3")
                        if (Inventory_Manager.Instance.SlotF[i] == true)
                        {
                            Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>()
                                .RealMe.GetComponent<Drag>().NumOfIt -= 1;

                            if (Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>()
                                .RealMe.GetComponent<Drag>().NumOfIt < 1)
                            {
                                Destroy(Inventory_Manager.Instance.Slot_Transform[i].GetChild(0).GetComponent<Drag>()
                                .RealMe);
                            }
                        }
                }
                break;
        }

    }

    public void UseItem(int n)
    {
        switch (n)
        {
            case 0:

                float hp = player.Status.Hp;

                hp += 2;

                if (5f <= hp)
                    hp = 5;
                
                player.Status.Hp = hp;

                GameManager.Instance.RefreshPlayerState();
                break;
            case 1:

                break;
            case 2:

                break;
        }


    }

    public void Craft_Page_Btn(int i)
    {
        switch (i)
        {
            case 1:
                Craft_page[0].SetActive(true);
                Craft_page[1].SetActive(false);
                Craft_page[2].SetActive(false);

                Craft_Btns[0].SetActive(false);
                Craft_Btns[1].SetActive(true);
                Craft_Btns[2].SetActive(true);
                break;
            case 2:
                Craft_page[0].SetActive(false);
                Craft_page[1].SetActive(true);
                Craft_page[2].SetActive(false);

                Craft_Btns[0].SetActive(true);
                Craft_Btns[1].SetActive(false);
                Craft_Btns[2].SetActive(true);
                break;
            case 3:
                Craft_page[0].SetActive(false);
                Craft_page[1].SetActive(false);
                Craft_page[2].SetActive(true);

                Craft_Btns[0].SetActive(true);
                Craft_Btns[1].SetActive(true);
                Craft_Btns[2].SetActive(false);
                break;
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

    public void BlackImage()
    {
        if (UIs[0].activeSelf || UIs[1].activeSelf || UIs[2].activeSelf ||
            UIs[3].activeSelf || UIs[4].activeSelf || UIs[5].activeSelf || UIs[6].activeSelf || UIs[7].activeSelf || UIs[8].activeSelf ||
            GameObject.Find("Canvas").transform.Find("TextBoxUI(Clone)") != null)
        {
            BI.SetActive(true);
        }

        else if ((!UIs[0].activeSelf && !UIs[1].activeSelf && !UIs[2].activeSelf &&
            !UIs[3].activeSelf && !UIs[4].activeSelf && !UIs[5].activeSelf && !UIs[6].activeSelf && 
            !UIs[7].activeSelf && !UIs[8].activeSelf) && GameObject.Find("Canvas").transform.Find("TextBoxUI(Clone)") == null)
        {
            BI.SetActive(false);
        }

        if (BI.activeSelf)
        {
            CursorManager.Instance.PlayMode(false);
        }
        else
        {
            CursorManager.Instance.PlayMode(true);
        }
    }
}
