using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossUnit : EnemyUnit {

    public Image habar;
    public GameObject hpbarall;

    public GameObject orginalparticle;

    public Vector2 range;

    Vector3 pos;

    bool dead;

	// Use this for initialization
	void OnEnable () {
        base.OnEnable();

        pos = hpbarall.transform.position;
	}

    // Update is called once per frame
    void Update () {
        float hppoint = 1f / StatusManager.Instance.GetUnitStatus("Boss").Hp;

        if (habar != null)
            habar.fillAmount = hppoint * Status.Hp;
    }

    public void BossDie()
    {
        GameObject.Find("Canvas").transform.Find("NotificationUI").gameObject.SetActive(true);

        SoundMng.Instance.BGM_Change(4);
    }

    protected override void Dead()
    {
        Destroy(this.GetUnit<BossUnit>().hpbarall.gameObject);

        StartCoroutine(BoomBoomBoom());

        if (!animator.stopanimation)
        {
            UnitManager.Instance.RemoveUnit(this);

            animator.stopanimation = true;

            animator.BodyAnimator.Play("Dead");
        }
    }

    protected override IEnumerator hithit(Bullet bullet)
    {
        StopCoroutine(shake());
        StartCoroutine(shake());

        return base.hithit(bullet);
    }

    IEnumerator BoomBoomBoom()
    {
        if (!dead)
        {
            dead = true;

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(orginalparticle);

                obj.transform.position = transform.position + new Vector3(Random.Range(range.x * -1, range.x), Random.Range(range.y * -1, range.y), 0);


                StartCoroutine(des(obj));

                yield return new WaitForSeconds(Random.Range(0.3f, 0.8f));
            }
        }
    }

    IEnumerator des(GameObject obj)
    {
        yield return new WaitForSeconds(2.1f);

        Destroy(obj);
    }

    IEnumerator shake()
    {
        if (hpbarall != null)
        {
            for (int i = 0; i < 5; i++)
            {
                if (hpbarall != null) hpbarall.transform.position += new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));

                yield return new WaitForSeconds(0.05f);

                if (hpbarall != null) hpbarall.transform.position = pos;
            }

            if (hpbarall != null) hpbarall.transform.position = pos;

        }
    }
}
