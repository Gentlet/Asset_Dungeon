using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundMng : MonoBehaviour
{
    public static SoundMng Instance { get; set; }

    public float BGM_volume;
    public float SFX_volume;

    public AudioClip Main_BGM;
    public AudioClip Stage_1_BGM;
    public AudioClip Stage_2_BGM;
    public AudioClip Boss_BGM;
    public AudioClip Stage_Win_BGM;

    public AudioClip Popup_On_SFX;
    public AudioClip Popup_Off_SFX;
    public AudioClip Btn_SFX;
    public AudioClip Exit_SFX;
    public AudioClip Buy_SFX;
    public AudioClip GetItem_SFX;

    public GameObject BGM;
    public GameObject SFX;

    public int StageNum = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Start()
    {
        BGM_volume = BGM.GetComponent<AudioSource>().volume;
        SFX_volume = SFX.GetComponent<AudioSource>().volume;
    }

    void Update()
    {
        SFX.GetComponent<AudioSource>().volume = SFX_volume;
        BGM.GetComponent<AudioSource>().volume = BGM_volume;
    }

    public void SFX_Play(int i)
    {
        switch (i)
        {
            case 1: // 버튼 클릭 사운드
                SFX.GetComponent<AudioSource>().clip = Btn_SFX;
                SFX.GetComponent<AudioSource>().Play();
                break;
            case 2: // 팝업 열 때 사운드
                SFX.GetComponent<AudioSource>().clip = Popup_On_SFX;
                SFX.GetComponent<AudioSource>().Play();
                break;
            case 3: // 팝업 닫을 때 사운드 
                SFX.GetComponent<AudioSource>().clip = Popup_Off_SFX;
                SFX.GetComponent<AudioSource>().Play();
                break;
            case 4:
                SFX.GetComponent<AudioSource>().clip = Buy_SFX;
                SFX.GetComponent<AudioSource>().Play();
                break;
            case 5:
                SFX.GetComponent<AudioSource>().clip = GetItem_SFX;
                SFX.GetComponent<AudioSource>().Play();
                break;

        }
    }

    public void BGM_Change(int n)
    {
        switch (n)
        {
            case 0:
                BGM.GetComponent<AudioSource>().clip = Main_BGM;
                BGM.GetComponent<AudioSource>().Play();
                break;
            case 1:
                BGM.GetComponent<AudioSource>().clip = Stage_1_BGM;
                BGM.GetComponent<AudioSource>().Play();
                break;
            case 2:
                BGM.GetComponent<AudioSource>().clip = Stage_2_BGM;
                BGM.GetComponent<AudioSource>().Play();
                break;
            case 3:
                BGM.GetComponent<AudioSource>().clip = Boss_BGM;
                BGM.GetComponent<AudioSource>().Play();
                break;
            case 4:
                BGM.GetComponent<AudioSource>().clip = Stage_Win_BGM;
                BGM.GetComponent<AudioSource>().Play();
                break;
        }
    }
}