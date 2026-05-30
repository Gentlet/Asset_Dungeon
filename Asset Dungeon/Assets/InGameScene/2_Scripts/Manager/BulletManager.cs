using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : SingletonGameObject<BulletManager> {
    //Test Code Init
    public GameObject bulletsObject;        //총알들을 모아둘 GameObject
    public GameObject OriginalBullet;       //가장 기본적인 총알  //Prefab

    public RevolutionObject RevolutionObj;       

    private List<Bullet> bullets;                   //총알들을 저장한 리스트

    private void Start()
    {
        bullets = new List<Bullet>();

        for (int i = 0; i < 200; i++)
        {
            Bullet bullet = Create();
        }

        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Active = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Active = false;
            }
        }
    }

    public Bullet Create()        //오브젝트 풀을 이용하여 총알을 생성
    {
        Bullet bullet = null;
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].Active && !bullets[i].revolutionbullet)
            {
                bullet = bullets[i];
                bullet.Active = true;
                break;
            }
        }

        if(bullet == null)
        {
            bullet = Instantiate(OriginalBullet, bulletsObject.transform).GetComponent<Bullet>();
            bullet.Active = true;
            bullets.Add(bullet);
        }

        //DebugManager.Instance.BulletNumUpdata(bullets.Count);

        return bullet;
    }
}
