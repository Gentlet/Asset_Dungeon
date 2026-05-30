using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : UnitAttack
{      //2017/11/09 ㅇㅣㅇㅜㅓㄴㅎㅕㄱ ㄱㅐㅅㅐㄱㄱㅣ

    private Unit playerunit;
    
    public float journeyTime = 1.0F;

    public float player_distance;



    private float startTime;

    private float degree2playerunit;

    void Start()
    {
        playerunit = null;
        startTime = Time.time;

        StartCoroutine(CardAttack());
    }

    private void Update()
    {
        if (playerunit != null)
            player_distance = Vector2.Distance(Unit.transform.position, PlayerUnit.transform.position);

        if (15f < player_distance)
            return;


        if (CursorManager.Instance.GameisRunning && !Unit.Stun)
        {
            WeaponDegreeSet();
        }
        //if(Input.GetKeyDown(KeyCode.M))
        //{
        //    StartCoroutine(Unit.Cards.FindCard(Card.Properties.Pattern).GetCard<PatternCard>().Pattern(Unit.GetUnit<EnemyUnit>()));
        //}

        
    }

    private IEnumerator CardAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (CursorManager.Instance.GameisRunning && DetectPlayer2 && player_distance <= 10f)
            {
                //StartCoroutine(Unit.Cards.FindCard(Card.Properties.Pattern).GetCard<PatternCard>().Pattern(Unit.GetUnit<EnemyUnit>()));

                for (int i = 0; i < 10; i++)
                {
                    int tmp = Random.Range(0, Unit.Cards.Length);

                    if (Unit.Cards[tmp] == null)
                        continue;

                    StartCoroutine(Unit.Cards[tmp].GetCard<PatternCard>().Pattern(Unit.GetUnit<EnemyUnit>()));

                    break;
                }
            }
        }
    }

#region NotUseCodes
    //private void Patern3_2()            //패턴3-2
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        for (int j = 0; j < 5; j++)
    //        {
    //            Bullet bullet = Attack()[0];
    //            bullet.transform.position = Unit.transform.position;

    //            bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle((Vector2)(playerunit.transform.position - transform.position)) + (-20 + (j * 10)));       //크게 퍼지는 샷건
    //            bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle(bullet.Direction) + (90 * i));

    //        }
    //    }

    //    //for (int i = 0; i < 20; i++)
    //    //{
    //    //    Bullet bullet = Attack()[0];
    //    //    bullet.transform.position = Unit.transform.position;    //몸통에서쏘기
    //    //    //bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle((Vector2)(playerunit.transform.position - transform.position)) + (-20 + (i * 2)));       //크게 퍼지는 샷건

    //    //    if (i < 5)
    //    //        bullet.Direction = playerunit.transform.position - (bullet.transform.position + Vector3.up * (0.5f - (i * -0.25f)));
    //    //    else if (i < 10 && i > 5)
    //    //    {
    //    //        bullet.Direction = playerunit.transform.position - (bullet.transform.position + Vector3.up * (0.5f + ((i - 5) * -0.25f)));
    //    //        bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle(bullet.Direction) + 90);
    //    //    }

    //    //    else if (i < 15 && i > 10)
    //    //    {
    //    //        bullet.Direction = playerunit.transform.position - (bullet.transform.position + Vector3.up * (0.5f + ((i - 10) * -0.25f)));
    //    //        bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle(bullet.Direction) + 180);
    //    //    }

    //    //    else
    //    //    {
    //    //        bullet.Direction = playerunit.transform.position - (bullet.transform.position + Vector3.up * (0.5f + ((i - 15) * -0.25f)));
    //    //        bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle(bullet.Direction) + 270);
    //    //    }

    //    //}
    //}   

    //private void Patern6_2()
    //{
    //    Vector3[] tmp = new Vector3[]
    //    {
    //        new Vector3(0.5f, 0),
    //        new Vector3(-0.5f, 0),
    //        new Vector3(0, 0.5f),
    //        new Vector3(0, -0.5f),
    //    };
    //    for (int j = 0; j < 4; j++)
    //    {
    //        for (int i = 0; i < 4; i++)
    //        {
    //            Bullet bullet = Attack()[0];

    //            bullet.transform.position = Unit.transform.position + tmp[i];
    //            switch (j)      //플레이어를 바라보고 4방향
    //            {
    //                case 0:
    //                    {
    //                        bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle((Vector2)(playerunit.transform.position - transform.position)) + 90);
    //                        break;
    //                    }
    //                case 1:
    //                    {
    //                        bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle((Vector2)(playerunit.transform.position - transform.position)) + 180);
    //                        break;
    //                    }
    //                case 2:
    //                    {
    //                        bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle((Vector2)(playerunit.transform.position - transform.position)) + 270);
    //                        break;
    //                    }
    //                case 3:
    //                    {
    //                        bullet.Direction = bullet.Angle2Direcrion(bullet.Direction2Angle((Vector2)(playerunit.transform.position - transform.position)) + 0);
    //                        break;
    //                    }
    //            }
    //            //switch (j)    //무조건 동서남북 4방향
    //            //{ 
    //            //    case 0: {
    //            //            bullet.Direction = bullet.Angle2Direcrion(90);
    //            //            break;
    //            //        }
    //            //    case 1: {
    //            //            bullet.Direction = bullet.Angle2Direcrion(180);
    //            //            break;
    //            //        }
    //            //    case 2: {
    //            //            bullet.Direction = bullet.Angle2Direcrion(270);
    //            //            break;
    //            //        }
    //            //    case 3: {
    //            //            bullet.Direction = bullet.Angle2Direcrion(0);
    //            //            break;
    //            //        }
    //            //}
    //        }
    //    }
    //}

    Vector2 BezierCurve(float t, Vector2 p0, Vector2 p1)
    {
        return ((1 - t) * p0) + ((t) * p1);
    }
    
    Vector2 BezierCurve2(float t, Vector2 p0, Vector2 p1, Vector2 p2)    // t = 0->1까지의 수 p0 = 시작좌표 p1 = 중간걸치기좌표 p2 = 도착좌표
    {
        Vector2 pa = BezierCurve(t, p0, p1);

        Vector2 pb = BezierCurve(t, p1, p2);

        return BezierCurve(t, pa, pb);
    }
    
    private void Patern7()              //패턴7
    {
        Vector3[] tmp = new Vector3[]
        {
            new Vector3(0.5f, 0),
            new Vector3(-0.5f, 0),
            new Vector3(0, 0.5f),
            new Vector3(0, -0.5f),
        };

        for (int i = 0; i < 4; i++)
        {
            Bullet bullet = Attack()[0];

            bullet.transform.position = Unit.transform.position + tmp[i];
            bullet.Direction = BezierCurve2(1, transform.position, transform.position + new Vector3(0, 2, 0), Unit.transform.position);
        }
    }
#endregion

    protected override void WeaponDegreeSet()
    {
        Vector3 rotation = Vector3.zero;
        Vector3 weapon_rotation = transform.rotation.eulerAngles;

        degree2playerunit = transform.position.Angle(PlayerUnit.transform.position);

        rotation.z = degree2playerunit;

        if (1 < Mathf.Abs(rotation.z) / 90f)
        {
            rotation.y = 180;
            rotation.z = (rotation.z * -1) + 180;
        }

        transform.rotation = Quaternion.Euler(rotation);
    }

    #region Property
    public float Degree2PlayerUnit
    {
        get
        {
            return degree2playerunit;
        }
    }

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
                if (hits[i].transform.CompareTag("Bullet") || hits[i].transform.CompareTag("Enemy")
                    /*|| hits[i].transform.CompareTag("Cliff") || hits[i].transform.CompareTag("CliffLine")*/)
                    continue;

                return hits[i].transform.name == "Player" ? true : false;
            }

            return false;
        }
    }

    public bool DetectPlayer2
    {
        get
        {
            Vector3 playerpos = PlayerUnit.transform.position;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + (transform.rotation * Vector3.right), (playerpos - transform.position).normalized);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform.CompareTag("Bullet") || hits[i].transform.CompareTag("Enemy")
                    || hits[i].transform.CompareTag("Cliff") || hits[i].transform.CompareTag("CliffLine"))
                    continue;

                return hits[i].transform.name == "Player" ? true : false;
            }

            return false;
        }
    }
    #endregion
}
