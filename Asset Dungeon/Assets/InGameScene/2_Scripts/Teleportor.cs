using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Teleportor : MonoBehaviour {
    public Image image;

    public ParticleSystem Particle;

    public Vector2 pos;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            StartCoroutine(nextstage(collision.gameObject));
        }
    }

    IEnumerator nextstage(GameObject obj)
    {
        float tmp = 0.02f;
        Color color = image.color;

        image.gameObject.SetActive(true);


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

        obj.transform.position = pos;

        yield return new WaitForSeconds(0.1f);

        if (SoundMng.Instance != null)
            SoundMng.Instance.BGM_Change(2);

        ParticleSystem particle = Instantiate(Particle);

        particle.transform.position = pos;

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


        yield return new WaitForSeconds(0.8f);

        //while (true)
        //{
        //    if (360f - particle.transform.rotation.eulerAngles.x <= 3f)
        //    {
        //        break;
        //    }

        //    particle.transform.rotation = Quaternion.Euler(particle.transform.rotation.eulerAngles + new Vector3(2f, 0, 0));


        //    yield return new WaitForEndOfFrame();
        //}

        Destroy(particle.gameObject);

    }
}
