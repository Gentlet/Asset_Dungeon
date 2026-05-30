using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject beingDragged;
    GameObject Item_Clone;
    GameObject Item_Clone2;

    public GameObject RealMe;

    Vector3 firstPos;
    Transform FirstParent;
    Transform CloneTf;

    float radius;
    int Slot_amount;
    int Slot_NUM;

    bool isEquip = false;

    Transform Temp_S;

    public int Item_num;
    public int NumOfIt = 1;

    // Use this for initialization
    void Start()
    {
        NumOfIt = 1;
        radius = Inventory_Manager.Instance.Slot_Transform[1].GetComponent<RectTransform>().rect.width / 2;
        Slot_amount = Inventory_Manager.Instance.Slot_Transform.Length;
        firstPos = transform.position;
        FirstParent = transform.parent;

        CloneTf = GameObject.Find("Canvas").transform.Find("InventoryUI").Find("Inventory_Item_Slot").Find("Item_Clones_Slot");

        Temp_S = GameObject.Find("Canvas").transform.Find("Temp_Slot");

        //for (int i = 0; i < Slot_amount; i++)
        //{
        //    if (Vector2.Distance(Inventory_Manager.Instance.Slot_Transform[i].position, transform.position) < radius)
        //    {
        //        Inventory_Manager.Instance.SlotF[i] = true;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Slot_amount; i++)
        {
            if (Vector2.Distance(Inventory_Manager.Instance.Slot_Transform[i].position, firstPos) < radius)
            {
                if (Inventory_Manager.Instance.Slot_Transform[i] != null)
                    Slot_NUM = i;
            }
        }
        Out_ShortCut();
        Item_Clone2_Update();

        transform.GetChild(0).GetComponent<Text>().text = "x" + NumOfIt;

        if (NumOfIt > 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        if(isEquip)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }

        if(NumOfIt < 1)
        {
            Destroy(gameObject);
        }

        if(gameObject.CompareTag("Item_Clone2") && RealMe == null)
        {
            Destroy(gameObject);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = gameObject;
        firstPos = transform.position;
        FirstParent = transform.parent;

        if (!gameObject.CompareTag("Item_Clone") && !isEquip)
        {
            Item_Clone = Instantiate(gameObject, FirstParent);
            Item_Clone.tag = "Item_Clone";
            Item_Clone.transform.localPosition = Vector2.zero;
            Image img = Item_Clone.GetComponent<Image>();
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a / 2f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!gameObject.CompareTag("Item_Clone2") && !isEquip)
        {
            transform.position = Input.mousePosition;
            transform.SetParent(Temp_S);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isEquip)
        {
            for (int i = 0; i < Slot_amount; i++)
            {
                if (Vector2.Distance(Inventory_Manager.Instance.Slot_Transform[i].position, Input.mousePosition) < radius)
                {
                    if (Inventory_Manager.Instance.SlotF[i] == false)
                    {
                        transform.SetParent(Inventory_Manager.Instance.Slot_Transform[i]);
                        transform.localPosition = Vector2.zero;

                        if (Item_Clone != null)
                        {
                            if (Inventory_Manager.Instance.Slot_Transform[i].CompareTag("Item_ShortCut"))
                            {
                                if (Inventory_Manager.Instance.Slot_Transform[Slot_NUM].CompareTag("Storage_Slot"))
                                {
                                    transform.SetParent(FirstParent);
                                    transform.localPosition = Vector2.zero;

                                    if (Item_Clone != null)
                                    {
                                        Destroy(Item_Clone);
                                    }

                                    break;
                                }

                                if (!gameObject.CompareTag("Item_Clone2"))
                                {
                                    if (Item_Clone2 == null)
                                    {
                                        Item_Clone2 = Instantiate(gameObject, Inventory_Manager.Instance.Slot_Transform[i]);
                                        Item_Clone2.tag = "Item_Clone2";
                                        Item_Clone2.name = "Item_YEZE";
                                        Item_Clone2.transform.localPosition = Vector2.zero;
                                        Item_Clone2.GetComponent<Drag>().RealMe = gameObject;
                                    }
                                    transform.SetParent(FirstParent);
                                    transform.localPosition = Vector2.zero;
                                }
                            }
                            else
                            {
                                if (Item_Clone != null)
                                {
                                    Destroy(Item_Clone);
                                }
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    else
                    {
                        break;
                    }
                }
                else
                {
                    transform.SetParent(FirstParent);
                    transform.localPosition = Vector2.zero;
                    if (Item_Clone != null)
                    {
                        Destroy(Item_Clone);
                    }

                    beingDragged = null;
                }
            }
        }
    }

    public void Item_Clone2_Update()
    {
        if (!gameObject.CompareTag("Item_Clone2"))
        {
            if (Item_Clone2 == null)
            {
                isEquip = false;
            }
            else
            {
                isEquip = true;
            }
        }
    }

    public void Out_ShortCut()
    {
        if (Vector2.Distance(transform.position, Input.mousePosition) < radius)
            if (Input.GetMouseButtonDown(1) && transform.parent.CompareTag("Item_ShortCut"))
            {
                Destroy(gameObject);
            }
    }

    #region Properties
    #endregion
}