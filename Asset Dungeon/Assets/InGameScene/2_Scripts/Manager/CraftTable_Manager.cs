using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable_Manager : SingletonGameObject<CraftTable_Manager>
{
    public Transform Temp;
    public Transform[] Slot_Transform = new Transform[0];
    public GameObject C_S_Line;
    bool[] Slot_full;

    // Use this for initialization
    void Awake()
    {
        Slot_full = new bool[Slot_Transform.Length];

        for (int i = 0; i < Slot_Transform.Length; i++)
        {
            Slot_full[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Slot_Transform.Length; i++)
        {
            if (Slot_Transform[i].childCount < 2)
            {
                SlotF[i] = false;
            }
            else
            {
                SlotF[i] = true;
            }
        }

        for (int j = 0; j < Slot_Transform.Length; j++)
        {
            if (SlotF[j] == true)
            {
                Slot_Transform[j].Find("Slot_On").gameObject.SetActive(true);
            }
            else
            {
                Slot_Transform[j].Find("Slot_On").gameObject.SetActive(false);
            }
        }

        if(SlotF[0] && SlotF[1] && SlotF[2])
        {
            C_S_Line.SetActive(true);
        }
        else
        {
            C_S_Line.SetActive(false);
        }
    }

    public bool[] SlotF
    {
        get
        {
            return Slot_full;
        }
    }
}
