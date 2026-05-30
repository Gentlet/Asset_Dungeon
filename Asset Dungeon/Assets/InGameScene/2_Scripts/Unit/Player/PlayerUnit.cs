using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit {

    public InGameUI yeah;

	void OnEnable() {
        CameraManager.Instance.SetPlayer(this);

        base.OnEnable();


        if (cards.FindCard(Card.Properties.Weapon) != null)
        {
            WeaponSetting();
            GameManager.Instance.RefreshPlayerState();
        }
    }

    void Update()
    {
        if (CursorManager.Instance.GameisRunning)
        {
            if (Input.GetKeyDown(KeyCode.N))
                dontdie = !dontdie;


            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                cards[0] = CardManager.Instance.GetCard("HandGun");
                WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                cards[0] = CardManager.Instance.GetCard("AssaultRifle");
                WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                cards[0] = CardManager.Instance.GetCard("ShotGun");
                WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                cards[1] = CardManager.Instance.GetCard("FlameCard");
                WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                cards[1] = CardManager.Instance.GetCard("FreezeCard");
                WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                cards[1] = CardManager.Instance.GetCard("ThunderCard");
                WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                cards[2] = CardManager.Instance.GetCard("ReflectCard");
                WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                cards[2] = CardManager.Instance.GetCard("RevolutionCard");
                WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                cards[2] = null;
                WeaponSetting();
                GameManager.Instance.RefreshPlayerState();
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                CameraManager.Instance.StartCoroutine(CameraManager.Instance.CameraShake());
            }

        }
    }

    protected override void Dead()
    {
        yeah.GameOver_On();
        base.Dead();
    }

    public override void PlayerStatusRefresh()
    {
        if (!this.GetUnitAttack<PlayerAttack>().Reload)
            GameManager.Instance.RefreshPlayerState();
        else
            GameManager.Instance.RefreshPlayerStateOnlyHpBar();
    }

    #region Property
    #endregion
}
