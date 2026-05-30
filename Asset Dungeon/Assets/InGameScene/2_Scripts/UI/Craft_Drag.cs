using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Craft_Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject Card_Clones;

    Transform Temp;
    Transform FirstParent;

    Vector3 FirstPos;

    public string OriginalName;

    public static GameObject beingDragged;

    float radius;
    bool isEquip = false;

    // Use this for initialization
    void Start()
    {
        Temp = CraftTable_Manager.Instance.Temp;
        FirstParent = transform.parent;
        FirstPos = transform.localPosition;
        radius = CraftTable_Manager.Instance.Slot_Transform[0].GetComponent<RectTransform>().rect.width / 2;

        OriginalName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Card_Clones != null && transform.position == Card_Clones.transform.position)
        {
            Destroy(Card_Clones);
        }

        Equip_Off();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beingDragged = gameObject;

        if (gameObject.name != "Card_Clones" && gameObject.name != "C_Thing")
        {
            CreateCardClone();

            FirstPos = transform.localPosition;
            FirstParent = transform.parent;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameObject.name != "Card_Clones" && gameObject.name != "C_Thing")
        {
            transform.position = Input.mousePosition;
            transform.SetParent(Temp);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        beingDragged = null;

        if (gameObject.name != "Card_Clones" && gameObject.name != "C_Thing")
        {
            for (int i = 0; i < CraftTable_Manager.Instance.Slot_Transform.Length; i++)
            {
                if (Vector2.Distance(Input.mousePosition, CraftTable_Manager.Instance.Slot_Transform[i].position) < radius)
                {
                    if (CraftTable_Manager.Instance.SlotF[i] == false)
                    {
                        if (transform.tag == CraftTable_Manager.Instance.Slot_Transform[i].tag)
                        {
                            transform.SetParent(CraftTable_Manager.Instance.Slot_Transform[i]);
                            transform.localPosition = Vector2.zero;
                            transform.SetAsFirstSibling();
                            transform.name = "C_Thing";

                            GunTypeUI.Instance.GunSet();
                            break;
                        }
                        else
                        {
                            Out(gameObject);
                        }
                    }

                    else if (CraftTable_Manager.Instance.SlotF[i] == true)
                    {
                        if (transform.tag == CraftTable_Manager.Instance.Slot_Transform[i].tag)
                        {
                            Out(CraftTable_Manager.Instance.Slot_Transform[i].GetChild(0).gameObject);

                            transform.SetParent(CraftTable_Manager.Instance.Slot_Transform[i]);
                            transform.localPosition = Vector2.zero;
                            transform.SetAsFirstSibling();
                            transform.name = "C_Thing";

                            GunTypeUI.Instance.GunSet();
                            break;
                        }
                        else
                        {
                            Out(gameObject);
                        }
                    } 
                }
                else
                {
                    Out(gameObject);
                }
            }
        }
    }

    public void Out(GameObject g)
    {
        g.transform.SetParent(g.GetComponent<Craft_Drag>().FirstParent);
        g.transform.localPosition = g.GetComponent<Craft_Drag>().FirstPos;
        g.transform.name = g.GetComponent<Craft_Drag>().OriginalName;
    }

    public void CreateCardClone()
    {
        Card_Clones = Instantiate(gameObject, FirstParent);
        Card_Clones.transform.localPosition = FirstPos;
        Card_Clones.name = "Card_Clones";
    }


    public void Equip_Off()
    {
        if (Vector2.Distance(transform.position, Input.mousePosition) < radius)
            if (Input.GetMouseButtonDown(1))
            {
                if (transform.name == "C_Thing")
                {
                    Out(gameObject);
                    GunTypeUI.Instance.GunSet();
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (gameObject.name != "Card_Clones")
                        {
                            if (transform.tag == CraftTable_Manager.Instance.Slot_Transform[i].tag)
                            {
                                if (CraftTable_Manager.Instance.SlotF[i])
                                    Out(CraftTable_Manager.Instance.Slot_Transform[i].GetChild(0).gameObject);

                                CreateCardClone();

                                transform.SetParent(CraftTable_Manager.Instance.Slot_Transform[i]);
                                transform.localPosition = Vector2.zero;
                                transform.SetAsFirstSibling();
                                transform.name = "C_Thing";

                                GunTypeUI.Instance.GunSet();
                                break;
                            }
                        }
                    }
                }
            }
    }
}