using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingletonGameObject<CameraManager> {
    public enum TargetTypes
    {
        None,
        Player,
        Boss,
        Another,
    }

    public Vector2 offset;
    public Vector2 smoothtime;

    private PlayerUnit player;
    private GameObject targetUnit;

    private Camera camera;

    private TargetTypes targetType;

    [SerializeField]
    private bool shaking;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        targetType = TargetTypes.None;
    }

    private void Update()
    {
        Follow();

        if (Input.GetKey(KeyCode.F1))
            camera.orthographicSize -= 0.1f;
        if (Input.GetKey(KeyCode.F2))
            camera.orthographicSize += 0.1f;

        if (Input.GetKeyDown(KeyCode.V))
            StartCoroutine(CameraShake());
    }

    private void Follow()
    {
        if (!shaking)
            Player();
            //Invoke(targetType.ToString(), 0.0f);
    }

    #region FollowType
    //움직이지 않음
    private void None()
    {

    }

    //플래이어와 마우스의 3분 1지점을 향하여 이동
    private void Player()
    {

        Vector3 cursorPos = CursorManager.Instance.position;
        Vector2 vec = (cursorPos - player.transform.position) * 0.15f;

        offset = vec;


        Vector2 velocity = new Vector2();

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + offset.x, ref velocity.x, smoothtime.x);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + offset.y, ref velocity.y, smoothtime.y);
        transform.position = new Vector3(posX, posY, transform.position.z);




        //Vector3 cursorPos = CursorManager.Instance.position;
        //test.transform.position = (cursorPos - player.transform.position) * 0.3f + player.transform.position;
        //Vector3 vec = (cursorPos - player.transform.position) * 0.3f + player.transform.position;
        //vec.z = -10;
        //camera.transform.position = vec;

    }

    //보스 애니메이션이 끝날동안 보스에 고정함
    private void Boss()
    {

    }

    //기타등등
    private void Another()
    {
        camera.transform.position = new Vector3(targetUnit.transform.position.x, targetUnit.transform.position.y, -10);
    }
#endregion

    public void SetTargetUnit(GameObject target,TargetTypes type)
    {
        targetUnit = target;
        //targetType = type;
    }

    public void SetPlayer(PlayerUnit unit)
    {
        player = unit;
        targetType = TargetTypes.Player;

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    public IEnumerator CameraShake()
    {
        if(!shaking)
        {
            shaking = true;

            for (int i = 0; i < 3; i++)
            {
                transform.position = transform.position + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
                yield return new WaitForSeconds(0.1f / 2f);
                transform.position = player.transform.position + new Vector3(0, 0, -10);
                yield return new WaitForSeconds(0.1f / 2f);
            }

            shaking = false;
        }
    }


    #region Properties
    public Camera MainCamera
    {
        get
        {
            return camera;
        }
    }

    public GameObject TargetUnit
    {
        get
        {
            return targetUnit;
        }
    }

    public TargetTypes TargetType
    {
        get
        {
            return targetType;
        }
    }
#endregion
}
