using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingUI : MonoBehaviour {

    float ftimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ftimer += Time.deltaTime;

        if(ftimer >= 3)
        {
            SoundMng.Instance.BGM_Change(1);
            SceneManager.LoadScene(1);
        }
	}
}
