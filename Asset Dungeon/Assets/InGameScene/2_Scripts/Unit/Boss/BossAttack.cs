using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : UnitAttack {      //카메라7.3 보스 5x5

    public AudioClip[] Audio_Count;
    private AudioSource AudioS;

    public GameObject Blight;      //빛
    public GameObject Target;      //표적

    public GameObject Pos_RU;
    public GameObject Pos_RD;
    public GameObject Pos_LU;
    public GameObject Pos_LD;
    public GameObject Pos_UM;
    public GameObject Pos_DM;

    private bool Idle = true;

    private bool RU_Moving = false;
    private bool RD_Moving = false;
    private bool LU_Moving = false;
    private bool LD_Moving = false;
    private bool UM_Moving = false;
    private bool DM_Moving = false;

    private GameObject[] Warning2 = new GameObject[100];
    private GameObject[] Rock2 = new GameObject[100];
    private GameObject[] Rock2_Parent = new GameObject[100];
    private GameObject em_Pos;
    private GameObject Rocket;
    private GameObject Hole1;
    private GameObject Hole2;
    private GameObject Hole3;

    private GameObject Face;
    private GameObject[] Target2 = new GameObject[10];
    private GameObject[] B2light = new GameObject[100];
    private Unit playerunit;
    private Bullet[] bullet2;

    private int Patern6_Angle;
    private int Patern6_Angle_Random = 0;
    private int b = 0;      //0 = <- , 1 = ->
    private int k = 0;
    private int AP1_moving1 = 0;
    private int AP1_moving2 = 0;
    private int AP1_moving3 = 0;
    int x, y, z, v, n = 0;

    private bool HolePlay1 = false;
    private bool HolePlay2 = false;
    private bool HolePlay3 = false;

    public Animator HoleAni1;
    public Animator HoleAni2;
    public Animator HoleAni3;
    public GameObject RocketBack;

    int rocketback = 0;
    

    void Start () {
        AudioS = GetComponent<AudioSource>();
        Rocket = transform.GetChild(0).gameObject;
        Hole1 = transform.GetChild(1).gameObject;
        Hole2 = transform.GetChild(2).gameObject;
        Hole3 = transform.GetChild(3).gameObject;
    }

    void Update ()
    {
        if (HolePlay1 == false)
            Hole1.SetActive(false);
        else
            Hole1.SetActive(true);

        if (HolePlay2 == false)
            Hole2.SetActive(false);
        else
            Hole2.SetActive(true);

        if (HolePlay3 == false)
            Hole3.SetActive(false);
        else
            Hole3.SetActive(true);

        if (Idle == true && !Unit.Animator.stopanimation)
        Unit.Animator.BodyAnimator.Play("D_IdleAnimation");
        
        if (RU_Moving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos_RU.transform.position, Unit.Status.Speed * 0.1f);
        }
        else if (RD_Moving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos_RD.transform.position, Unit.Status.Speed * 0.1f);
        }
        else if (LU_Moving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos_LU.transform.position, Unit.Status.Speed * 0.1f);
        }
        else if (LD_Moving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos_LD.transform.position, Unit.Status.Speed * 0.1f);
        }
        else if (UM_Moving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos_UM.transform.position, Unit.Status.Speed * 0.1f);
        }
        else if (DM_Moving == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Pos_DM.transform.position, Unit.Status.Speed * 0.1f);
        }
        else
            Unit.Rigid.velocity = Vector2.zero;
        
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StartCoroutine("Patern1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            StartCoroutine("Patern2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            StartCoroutine("Patern5");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine("Patern6_1");
            StartCoroutine("Patern6_2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine("Patern4");
        }
    }

    protected Bullet Attack1()
    {
        Bullet bullet = BulletManager.Instance.Create();

        bullet.Init(Unit);

        return bullet;
    }

    protected Bullet[] Attack(int num)
    {
        Bullet[] bullet = new Bullet[num];
        
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i] = Attack1();
        }

        return bullet;
        
    }

    IEnumerator Patern1()              // 확률20%
    {
        Idle = false;
        Unit.Animator.BodyAnimator.Play("D_RocketAttackAnimation");
        AudioS.PlayOneShot(Audio_Count[3]);
        Target2[0] = Instantiate(Target, PlayerUnit.transform.position + Vector3.down * 0.5f, Quaternion.Euler(0, 0, 0)) as GameObject;
        Target2[0].transform.SetParent(PlayerUnit.transform);
        for (int i = 0; i < 50; i++)     //쏘는 횟수
        {
            Bullet bullet1 = Attack1();
            Unit.Animator.playAudio(Unit.Weapon.Sound);
            bullet1.Degree = Rocket.transform.position.Angle(PlayerUnit.transform.position);
            bullet1.transform.position = Rocket.transform.position;

            yield return new WaitForSeconds(0.1f);      //쏘는 간격
        }
        Destroy(Target2[0].gameObject);
        Idle = true;
    }

    IEnumerator Patern2()              // 확률 30%
    {
        Idle = false;
        Unit.Animator.BodyAnimator.Play("D_AttackAnimation");
        int a = 0;
        Vector3 algleOffset = Vector3.zero;

        AudioS.PlayOneShot(Audio_Count[3]);
        Target2[1] = Instantiate(Target, PlayerUnit.transform.position + Vector3.down * 0.5f, Quaternion.Euler(0, 0, 0)) as GameObject;
        Target2[1].transform.SetParent(PlayerUnit.transform);
        yield return new WaitForSeconds(1);
        Idle = true;
        for (int i = 0; i < 5; i++)
        {
           
            algleOffset.x = 2 * Mathf.Cos((((360 / 5) * i) + 90) * Mathf.Deg2Rad);
            algleOffset.y = 2 * Mathf.Sin((((360 / 5) * i) + 90) * Mathf.Deg2Rad);

            B2light[i] = Instantiate(Blight, PlayerUnit.transform.position + algleOffset, Quaternion.Euler(0, 0, 0)) as GameObject;
            B2light[i].transform.SetParent(PlayerUnit.transform);
            yield return new WaitForSeconds(0.1f);
        }

        bullet2 = Attack((int)5);

        for (int i = 0; i < 5; i++)
        {
            bullet2[i].Speed = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            Destroy(B2light[i].gameObject);
            algleOffset.x = 2 * Mathf.Cos((((360 / 5) * i) + 90) * Mathf.Deg2Rad);
            algleOffset.y = 2 * Mathf.Sin((((360 / 5) * i) + 90) * Mathf.Deg2Rad);

            bullet2[i].Speed = 0;
            bullet2[i].transform.position = PlayerUnit.transform.position + algleOffset;
            bullet2[i].transform.SetParent(PlayerUnit.transform);
            a++;
            yield return new WaitForSeconds(0.1f);

            if (a == 5)
            {
                for (int j = 0; j < 5; j++)
                {
                    yield return new WaitForSeconds(1f);
                    AudioS.PlayOneShot(Audio_Count[0]);
                    yield return new WaitForSeconds(1f);
                    AudioS.PlayOneShot(Audio_Count[1]);
                    yield return new WaitForSeconds(1f);
                    AudioS.PlayOneShot(Audio_Count[2]);
                    bullet2[j].transform.parent = null;
                    bullet2[j].Direction = PlayerUnit.transform.position - bullet2[j].transform.position;
                    bullet2[j].Speed = bullet2[i].ConnectedUnit.Weapon.Speed;
                }
                Destroy(Target2[1].gameObject);
            }
        }

    }

    IEnumerator Patern3()              // 확률 10%
    {

        yield return new WaitForSeconds(1);
    }

    IEnumerator Patern4()              // 확률 20%
    {
        Idle = false;
        Unit.Animator.BodyAnimator.Play("D_RocketAttackAnimation");
        AudioS.PlayOneShot(Audio_Count[3]);
        Target2[2] = Instantiate(Target, PlayerUnit.transform.position + Vector3.down * 0.5f, Quaternion.Euler(0, 0, 0)) as GameObject;
        Target2[2].transform.SetParent(PlayerUnit.transform);
        int random4 = Random.Range(10, 20);

        for (int i = 0; i < random4; i++)
        {
            float degree = Rocket.transform.position.Angle(PlayerUnit.transform.position);

            for (int j = 1; j < 6; j++)
            {
                Unit.Animator.playAudio(Unit.Weapon.Sound);
                Bullet bullet4 = Attack1();
                bullet4.Speed -= 3;
                bullet4.transform.position = Rocket.transform.position;
                if (j == 1 || j == 2)
                    bullet4.Degree = degree + ((j) * 7.5f);
                else
                    bullet4.Degree = degree - ((j-3) * 7.5f);
            }
            yield return new WaitForSeconds(0.25f);
        }
        Destroy(Target2[2].gameObject);
        Idle = true;
    }

    IEnumerator Patern5()              // 확률 20%
    {
        Idle = false;
        HolePlay1 = true;
        HolePlay3 = true;
        Unit.Animator.BodyAnimator.Play("D_AttackAnimation");
        AudioS.PlayOneShot(Audio_Count[3]);
        Target2[3] = Instantiate(Target, PlayerUnit.transform.position + Vector3.down * 0.5f, Quaternion.Euler(0, 0, 0)) as GameObject;
        Target2[3].transform.SetParent(PlayerUnit.transform);
        
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Unit.Animator.playAudio(Unit.Weapon.Sound);
                int angle = Random.Range(-15, 16);
                Bullet bullet5 = Attack1();
                bullet5.transform.position = Hole1.transform.position;
                bullet5.Degree = Rocket.transform.position.Angle(PlayerUnit.transform.position) + angle;
            }
            for (int j = 0; j < 2; j++)
            {
                Unit.Animator.playAudio(Unit.Weapon.Sound);
                int angle = Random.Range(-15, 16);
                Bullet bullet5 = Attack1();
                bullet5.transform.position = Hole3.transform.position;
                bullet5.Degree = Rocket.transform.position.Angle(PlayerUnit.transform.position) + angle;
            }
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(Target2[3].gameObject);
        HolePlay1 = false;
        HolePlay3 = false;
        Idle = true;
    }

    IEnumerator Patern6_1()              // 확률 20%
    {
        Idle = false;
        int RL_Move = Random.Range(1, 3);

        if(RL_Move == 0)        //Right
        {
            AudioS.PlayOneShot(Audio_Count[4]);
            rocketback = 0;
            Unit.Animator.BodyAnimator.Play("D_RocketAttackAnimation");
            RU_Moving = true;
            yield return new WaitForSeconds(1.5f);
            RU_Moving = false;
            rocketback = 1;
            transform.position = Pos_RD.transform.position;
            Unit.Animator.BodyAnimator.Play("U_RocketAttackAnimation");
            LD_Moving = true;
            yield return new WaitForSeconds(1.5f);
            AudioS.PlayOneShot(Audio_Count[4]);
            yield return new WaitForSeconds(1.5f);
            LD_Moving = false;
            rocketback = 0;
            transform.position = Pos_LU.transform.position;
            Unit.Animator.BodyAnimator.Play("D_RocketAttackAnimation");
            UM_Moving = true;
            yield return new WaitForSeconds(1.5f);
            UM_Moving = false;
            Unit.Animator.BodyAnimator.Play("D_IdleAnimation");
        }
        else                    //Left
        {
            AudioS.PlayOneShot(Audio_Count[4]);
            rocketback = 0;
            Unit.Animator.BodyAnimator.Play("D_RocketAttackAnimation");
            LU_Moving = true;
            yield return new WaitForSeconds(1.5f);
            LU_Moving = false;
            rocketback = 1;
            transform.position = Pos_LD.transform.position;
            Unit.Animator.BodyAnimator.Play("U_RocketAttackAnimation");
            RD_Moving = true;
            yield return new WaitForSeconds(1.5f);
            AudioS.PlayOneShot(Audio_Count[4]);
            yield return new WaitForSeconds(1.5f);
            RD_Moving = false;
            rocketback = 0;
            transform.position = Pos_RU.transform.position;
            Unit.Animator.BodyAnimator.Play("D_RocketAttackAnimation");
            UM_Moving = true;
            yield return new WaitForSeconds(1.5f);
            UM_Moving = false;
            Unit.Animator.BodyAnimator.Play("D_IdleAnimation");
        }
        Idle = true;
    }

    IEnumerator Patern6_2()
    {
        AudioS.PlayOneShot(Audio_Count[3]);
        Target2[4] = Instantiate(Target, PlayerUnit.transform.position + Vector3.down * 0.5f, Quaternion.Euler(0, 0, 0)) as GameObject;
        Target2[4].transform.SetParent(PlayerUnit.transform);
        for (int i = 0; i < 55; i++)     //쏘는 횟수
        {
            Unit.Animator.playAudio(Unit.Weapon.Sound);
            Bullet bullet1 = Attack1();
            bullet1.Degree = Rocket.transform.position.Angle(PlayerUnit.transform.position);
            if (rocketback == 0)
                bullet1.transform.position = Rocket.transform.position;
            else
                bullet1.transform.position = RocketBack.transform.position;
            Patern6_Angle++;
            yield return new WaitForSeconds(0.1f);      //쏘는 간격
        }
        Destroy(Target2[4].gameObject);
    }
    
    #region Property

    public Unit PlayerUnit
    {
        get
        {
            if (playerunit == null)
                playerunit = UnitManager.Instance.GetUnit("Player");

            return playerunit;
        }
        set
        {
            playerunit = value;
        }
    }

    public bool DetectPlayer
    {
        get
        {
            Vector3 playerpos = PlayerUnit.transform.position;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + (transform.rotation * Vector3.right), (playerpos - transform.position).normalized);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.CompareTag("Bullet") || hits[i].transform.CompareTag("Enemy")) continue;

                return hits[i].transform.name == "Player" ? true : false;
            }

            return false;
        }
    }

    #endregion
}
