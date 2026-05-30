using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit {

	// Use this for initialization
	void OnEnable() {
        base.OnEnable();
	}

    // Update is called once per frame
    void Update()
    {
        if (CursorManager.Instance.GameisRunning)
        {

            if (Input.GetKeyDown(KeyCode.M))
                dontdie = !dontdie;

        }
    }

    protected override void Dead()
    {

        UnitManager.Instance.RemoveUnit(this);

        GameManager.Instance.EnemyDead();

        Animator.DeadAnimation();
        Destroy(gameObject);

    }

    protected override IEnumerator hithit(Bullet bullet)
    {
        Card tcard = bullet.ConnectedCards.FindCard(Card.Properties.Attack);

        if (tcard == null)
        {
            GameObject obj = Instantiate(GameManager.Instance.hitparticles[0], transform);

            obj.transform.position = transform.position;

            StartCoroutine(des(obj));
        }
        else if (tcard.Name == "FlameCard")
        {
            GameObject obj = Instantiate(GameManager.Instance.hitparticles[0], transform);

            obj.transform.position = transform.position;

            StartCoroutine(des(obj));
        }
        else if (tcard.Name == "FreezeCard")
        {
            GameObject obj = Instantiate(GameManager.Instance.hitparticles[1], transform);

            obj.transform.position = transform.position;

            StartCoroutine(des(obj));
        }
        else if (tcard.Name == "ThunderCard")
        {
            GameObject obj = Instantiate(GameManager.Instance.hitparticles[2], transform);

            obj.transform.position = transform.position;

            StartCoroutine(des(obj));
        }
        else
            Debug.Log("set");


        return base.hithit(bullet);
    }

    IEnumerator des(GameObject obj)
    {
        yield return new WaitForSeconds(1f);

        Destroy(obj);
    }

    #region Property
    #endregion
}
