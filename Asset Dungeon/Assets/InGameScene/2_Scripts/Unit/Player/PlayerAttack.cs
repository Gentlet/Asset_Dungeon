    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : UnitAttack {

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (CursorManager.Instance.GameisRunning && !Unit.Stun)
        {
            WeaponDegreeSet();

            if (!Unit.Animator.RollingTrigger)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(1)) 
                {
                    Attack();
                }
            }

            if(Input.GetKeyDown(KeyCode.R) && Unit.Magazine != Unit.Weapon.Magazine)
            {
                StartCoroutine(Reloading());
            }
        }
    }

    protected override void WeaponDegreeSet()
    {
        Vector3 rotation = Vector3.zero;
        Vector3 weapon_rotation = transform.rotation.eulerAngles;

        rotation.z = transform.position.Angle(CursorManager.Instance.position);

        if (1 < Mathf.Abs(rotation.z) / 90f)
        {
            rotation.y = 180;
            rotation.z = (rotation.z * -1) + 180;
        }


        transform.rotation = Quaternion.Euler(rotation);
    }
}
