using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Text : MonoBehaviour {

    public int NofItem;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "x" + NofItem;

        if (NofItem > 1)
        {
            gameObject.SetActive(true);
        }
        else if(NofItem < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
