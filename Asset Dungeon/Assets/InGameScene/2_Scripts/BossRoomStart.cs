using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossRoomStart : MonoBehaviour {

    public Image image;

    public GameObject obj;

    public AudioSource StartA;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            StartCoroutine(nextstage());
    }
    
    IEnumerator nextstage()
    {
        float tmp = 0.05f;
        Color color = image.color;

        image.gameObject.SetActive(true);
        StartA.Play();

        while (true)
        {
            if (1f <= image.color.a)
            {
                tmp *= -1f;
                break;
            }

            color.a += tmp;
            image.color = color;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1f);

        if (SoundMng.Instance != null)
            SoundMng.Instance.BGM_Change(3);

        obj.SetActive(true);

        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (image.color.a <= 0f)
            {
                tmp *= -1f;
                break;
            }

            color.a += tmp;
            image.color = color;

            yield return new WaitForEndOfFrame();
        }

        image.gameObject.SetActive(false);

        Destroy(gameObject);
    }

}
