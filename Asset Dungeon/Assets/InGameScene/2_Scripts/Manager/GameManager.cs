using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : SingletonGameObject<GameManager> {
    
    public Vector4[] roomrange;
    public GameObject[] Doorpaths;

    public Image hp_bar;
    public Image magazine_bar;

    public GameObject[] hitparticles;

    public GameObject WallCrushParticle;

    private PlayerUnit player;
    private Unit[] enemys;

    private int playerroom;

    private bool playermove;
	// Use this for initialization
	void Start () {
        player = UnitManager.Instance.GetUnit(UnitStatus.Type.Player).GetUnit<PlayerUnit>();
        enemys = UnitManager.Instance.GetUnits(UnitStatus.Type.Enemy);

        Refresh();
	}
	
	// Update is called once per frame
	void Update () {
        if (playermove)
            Refresh();


        if (Input.GetKeyDown(KeyCode.Y))
        {
            OpenTheDoor(Doorpaths[0]);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            CloseTheDoor(Doorpaths[0]);
        }

    }

    public bool IsInTheRoom(Vector3 pos)
    {
        for (int i = 0; i < roomrange.Length; i++)
        {
            if ((roomrange[i].x <= pos.x && pos.x <= roomrange[i].z) &&
                (roomrange[i].y <= pos.y && pos.y <= roomrange[i].w)
                )
            {
                return true;
            }
        }

        return false;
    }

    private void Refresh()
    {
        for (int i = 0; i < roomrange.Length; i++)
        {
            if((roomrange[i].x <= player.transform.position.x && player.transform.position.x <= roomrange[i].z) &&
                (roomrange[i].y <= player.transform.position.y && player.transform.position.y <= roomrange[i].w)
                )
            {
                playerroom = i;

                break;
            }
        }

        GameObject[] doors = PlayerRoomDoorpaths;

        if(RoomEmpty)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                OpenTheDoor(doors[i]);
            }
        }
        else
        {
            for (int i = 0; i < doors.Length; i++)
            {
                CloseTheDoor(doors[i]);
            }
        }
    }

    private void OpenTheDoor(GameObject doorpath)
    {
        StartCoroutine(OpenTheDoorAnimation(doorpath));
        playermove = true;
    }

    private void CloseTheDoor(GameObject doorpath)
    {

        StartCoroutine(CloseTheDoorAnimation(doorpath));
        playermove = false;
    }

    private IEnumerator OpenTheDoorAnimation(GameObject doorpath)
    {
        if (doorpath.transform.position.z != 1)
        {
            Vector3 vec = doorpath.transform.position;

            vec.z = 1;

            doorpath.transform.position = vec;
        }
        else
            yield break;

        SpriteRenderer[] sprites = new SpriteRenderer[doorpath.transform.childCount];
        BoxCollider2D[] boxes = new BoxCollider2D[doorpath.transform.childCount];

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = doorpath.transform.GetChild(i).GetComponent<SpriteRenderer>();
            boxes[i] = doorpath.transform.GetChild(i).GetComponent<BoxCollider2D>();
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].enabled = false;
        }

        for (int z = 0; z < 50; z++)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].transform.position += Vector3.up * 0.05f;

                sprites[i].color += new Color(0f, 0f, 0f, -0.02f);
            }

            yield return new WaitForEndOfFrame();
        }


        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].transform.position += Vector3.down * 2.5f;

            sprites[i].color += new Color(0f, 0f, 0f, 1f);
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].enabled = true;
        }

        doorpath.SetActive(false);
    }

    private IEnumerator CloseTheDoorAnimation(GameObject doorpath)
    {
        if (doorpath.transform.position.z == 1)
        {
            Vector3 vec = doorpath.transform.position;

            vec.z = 0;

            doorpath.transform.position = vec;
        }
        else
            yield break;

        SpriteRenderer[] sprites = new SpriteRenderer[doorpath.transform.childCount];
        BoxCollider2D[] boxes = new BoxCollider2D[doorpath.transform.childCount];

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = doorpath.transform.GetChild(i).GetComponent<SpriteRenderer>();
            boxes[i] = doorpath.transform.GetChild(i).GetComponent<BoxCollider2D>();
        }

        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].enabled = false;
        }

        doorpath.SetActive(true);

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].transform.position += Vector3.up * 2.5f;

            sprites[i].color += new Color(0f, 0f, 0f, 0);
        }


        for (int z = 0; z < 50; z++)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].transform.position += Vector3.down * 0.05f;

                sprites[i].color += new Color(0f, 0f, 0f, 0.02f);
            }

            yield return new WaitForEndOfFrame();
        }



        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i].enabled = true;
        }
    }

    public void EnemyDead()
    {
        enemys = UnitManager.Instance.GetUnits(UnitStatus.Type.Enemy);

        Refresh();
    }

    public void RefreshPlayerState()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is not define in GameManager Script");
            return;
        }

        float hppoint = 1f / StatusManager.Instance.GetUnitStatus("Player").Hp;
        float bulletpoint = 1f / player.Weapon.Magazine;

        hp_bar.fillAmount = hppoint * player.Status.Hp;
        magazine_bar.fillAmount = bulletpoint * player.Magazine;
    }

    public void RefreshPlayerStateOnlyHpBar()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is not define in GameManager Script");
            return;
        }

        float hppoint = 1f / StatusManager.Instance.GetUnitStatus("Player").Hp;

        hp_bar.fillAmount = hppoint * player.Status.Hp;
    }

    public void PlayWallCrushParticle(Vector3 position)
    {
        GameObject obj = Instantiate(GameManager.Instance.WallCrushParticle);

        obj.transform.position = position;

        StartCoroutine(des(obj));
    }

    IEnumerator des(GameObject obj)
    {
        yield return new WaitForSeconds(1f);

        Destroy(obj);
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < roomrange.Length; i++)
        {
            Gizmos.color = Color.green;
            Vector2 middle = new Vector2((roomrange[i].x + roomrange[i].z) / 2f, (roomrange[i].y + roomrange[i].w) / 2f);
            Vector2 size = new Vector2((roomrange[i].z - middle.x) * 2, (roomrange[i].w - middle.y) * 2);

            Gizmos.DrawWireCube(middle, size);
        }
    }

    #region Properties
    private bool RoomEmpty
    {
        get
        {
            if (enemys != null)
            {
                for (int i = 0; i < enemys.Length; i++)
                {
                    if ((roomrange[playerroom].x <= enemys[i].transform.position.x && enemys[i].transform.position.x <= roomrange[playerroom].z) &&
                        (roomrange[playerroom].y <= enemys[i].transform.position.y && enemys[i].transform.position.y <= roomrange[playerroom].w)
                        )
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    private GameObject[] PlayerRoomDoorpaths
    {
        get
        {
            List<GameObject> tmp = new List<GameObject>();

            for (int i = 0; i < Doorpaths.Length; i++)
            {
                for (int j = 0; j < Doorpaths[i].transform.childCount; j++)
                {
                    if ((roomrange[playerroom].x <= Doorpaths[i].transform.GetChild(j).transform.position.x && Doorpaths[i].transform.GetChild(j).transform.position.x <= roomrange[playerroom].z) &&
                        (roomrange[playerroom].y <= Doorpaths[i].transform.GetChild(j).transform.position.y && Doorpaths[i].transform.GetChild(j).transform.position.y <= roomrange[playerroom].w)
                        )
                    {
                        tmp.Add(Doorpaths[i]);
                    }
                }
            }

            GameObject[] result = new GameObject[tmp.Count];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = tmp[i];
            }

            return result;
        }
    }
#endregion
}
