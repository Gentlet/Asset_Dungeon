using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBook : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("Idle");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Idle()
    {
        while (true)
        {
            for (int i = 0; i < 50; i++)
            {
                transform.position += Vector3.up * 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < 50; i++)
            {
                transform.position += Vector3.down * 0.01f;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
