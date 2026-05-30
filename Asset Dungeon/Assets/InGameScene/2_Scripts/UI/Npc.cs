using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    public int npcNum;
    public float dist;
    public GameObject TextBox;
    GameObject Storage;
    GameObject Shop;
    GameObject Canvas;
    GameObject Inven;
    GameObject B_Image;

	// Use this for initialization
	void Start () {
        Canvas = GameObject.Find("Canvas");
        Storage = Canvas.transform.Find("StorageUI").gameObject;
        Shop = Canvas.transform.Find("ShopUI").gameObject;
        Inven = Canvas.transform.Find("InventoryUI").gameObject;
        B_Image = Canvas.transform.Find("BlackImage").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        dist = Vector2.Distance(UnitManager.Instance.GetUnit("Player").transform.position, transform.position);

        if (Input.GetKeyDown(KeyCode.Z) && dist < 4)
        {
            NpcInteractiond(npcNum);    
        }
	}

    void NpcInteractiond(int num)
    {
        B_Image.SetActive(true);

        switch (num)
        {
            case 1: // 일반 npc 1
                Instantiate(TextBox, Canvas.transform);
                TextBox.GetComponent<TextBoxUI>().Illust_Image.sprite = TextBox.GetComponent<TextBoxUI>().Illusts[1];
                //TextBox.GetComponent<TextBoxUI>().textFile = TextBox.GetComponent<TextBoxUI>().Texts[0];
                break;
            case 2: // 일반 npc 2
                Instantiate(TextBox, Canvas.transform);
                TextBox.GetComponent<TextBoxUI>().Illust_Image.sprite = TextBox.GetComponent<TextBoxUI>().Illusts[2];
                //TextBox.GetComponent<TextBoxUI>().textFile = TextBox.GetComponent<TextBoxUI>().Texts[0];
                break;
            case 3: // 창고 npc
                Storage.SetActive(true);
                Inven.SetActive(true);
                break;
            case 4: // 상점 npc
                Shop.SetActive(true);
                Inven.SetActive(true);
                break;
        }
    }
}
