using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationUI : MonoBehaviour {

    public bool FadeIn = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() { 
	}

    public void Faded()
    {
        FadeIn = true;
    }
}
