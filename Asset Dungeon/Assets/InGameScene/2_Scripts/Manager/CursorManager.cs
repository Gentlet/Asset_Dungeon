using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CursorManager : SingletonGameObject<CursorManager> {
    public Sprite test;
    
    private float sensitivity = 30;
    private bool ispause;

    //Cursor.SetCursor(Texture2D, CursorMode)               ********

    // Use this for initialization
    private void Awake () {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        Cursor.SetCursor(test.texture, Vector2.one * 25, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ispause = !ispause;
        }
	}
    
    private void SensitivityControl()
    {
        if (Input.GetKey(KeyCode.N))
            sensitivity -= 0.1f;
        if (Input.GetKey(KeyCode.M))
            sensitivity += 0.1f;
    }

    public void PlayMode(bool bol)
    {
        ispause = !bol;
    }

    #region Properties
    public List<GameObject> CursorEnterObjects
    {
        get
        {
            List<GameObject> list = new List<GameObject>();

            RaycastHit2D[] hits = Physics2D.RaycastAll(position, Vector2.zero, 1);

            Debug.DrawRay(position, Vector2.zero * 1, Color.black, 1);

            for (int i = 0; i < hits.Length; i++)
                list.Add(hits[i].transform.gameObject);

            return list;
        }
    }

    public Vector3 position
    {
        get
        {
            return CameraManager.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            //return CameraManager.Instance.MainCamera.ScreenToWorldPoint(cursor.transform.position);
        }
    }

    public bool GameisRunning
    {
        get
        {
            return !ispause;
        }
    }
#endregion
}
