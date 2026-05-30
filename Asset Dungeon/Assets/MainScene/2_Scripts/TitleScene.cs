using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

    public GameObject Really;
    public GameObject Option;
    public GameObject Continue;
    public GameObject MainMenu;

    public GameObject Loading;

    bool B_Con;
    bool B_Real;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(B_Con)
        {
            Continue.transform.localPosition = Vector2.MoveTowards(Continue.transform.localPosition,
                new Vector2(Continue.transform.localPosition.x, -30), 100);
        }

        if(B_Real)
        {
            Really.transform.localPosition = Vector2.MoveTowards(Really.transform.localPosition,
                new Vector2(Really.transform.localPosition.x, -25), 100);
        }
        else
        {
            Really.transform.localPosition = Vector2.MoveTowards(Really.transform.localPosition,
                new Vector2(Really.transform.localPosition.x, -850), 100);
        }
	}

    public void PlayBtn()
    {
        B_Con = true;
        SoundMng.Instance.SFX_Play(2);
    }

    public void Continue_()
    {

        Instantiate(Loading, GameObject.Find("MainScene").transform);
        SoundMng.Instance.SFX_Play(2);
    }

    public void ExitBtn()
    {
        B_Real = true;
        SoundMng.Instance.SFX_Play(2);
    }

    public void ExitBtn_N()
    {
        B_Real = false;
        SoundMng.Instance.SFX_Play(2);
    }

    public void ExitBtn_Y()
    {
        Application.Quit();
        SoundMng.Instance.SFX_Play(2);
    }

    public void OptionBtn()
    {
        Option.SetActive(true);
        MainMenu.SetActive(false);
        SoundMng.Instance.SFX_Play(2);
    }
}
