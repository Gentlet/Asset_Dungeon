using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Manager : SingletonGameObject<Inventory_Manager>
{

    public Transform[] Slot_Transform = new Transform[0];
    bool[] Slot_full;

    int overCount;

    // Use this for initialization
    void Awake()
    {
        Slot_full = new bool[Slot_Transform.Length];

        for(int i=0;i<Slot_Transform.Length;i++)
        {
            Slot_full[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Slot_Transform.Length; i++)
        {
            if (Slot_Transform[i].Find("Item_YEZE") == null)
            {
                SlotF[i] = false;
            }
            else
            {
                SlotF[i] = true;
            }
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
