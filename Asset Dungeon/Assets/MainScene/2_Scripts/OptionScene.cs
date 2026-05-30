using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionScene : MonoBehaviour {

    public GameObject MainMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Exit_Option();
        }
	}

    public void Exit_Option()
    {
        gameObject.SetActive(false);

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenu.SetActive(true);
        }

        SoundMng.Instance.SFX_Play(3);
    }

    public void BGM_Update(float value)
    {
        SoundMng.Instance.BGM_volume = value;
    }

    public void SFX_Update(float value)
    {
        SoundMng.Instance.SFX_volume = value;
    }
}
