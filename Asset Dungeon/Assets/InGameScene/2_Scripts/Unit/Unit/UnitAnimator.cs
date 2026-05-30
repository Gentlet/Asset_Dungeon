using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : ChildUnitInterface
{
    public enum AnimationTypes
    {
        Idle,

        U_MoveAnimation,
        D_MoveAnimation,
        R_MoveAnimation,
        L_MoveAnimation,
        UR_MoveAnimation,
        UL_MoveAnimation,
        DR_MoveAnimation,
        DL_MoveAnimation,

        BodyAnimationEnd,


        U_RollingAnimation,
        D_RollingAnimation,
        R_RollingAnimation,
        L_RollingAnimation,
        UR_RollingAnimation,
        UL_RollingAnimation,
        DR_RollingAnimation,
        DL_RollingAnimation,
    }

    public bool stopanimation;

    [SerializeField]
    private Animator rollinganimator;

    private Animator bodyanimator;
    private Animator gunanimator;

    private Animator gunshotanimator;

    private AnimationTypes animationTrigger;

    private bool rollingtrigger;

    private AudioSource audioplayer;
    [SerializeField]
    private AudioClip[] clips;


    private float tmove;

    private bool rollingeffect;


    private void PlayAnimation()
    {

        if (stopanimation)
            return;

        if (animationTrigger < AnimationTypes.BodyAnimationEnd)
        {
            bodyanimator.SetInteger("Trigger", (int)AnimationTrigger);

            if (AnimationTrigger != AnimationTypes.Idle)
            {
                if (AnimationTrigger == AnimationTypes.R_MoveAnimation || AnimationTrigger == AnimationTypes.L_MoveAnimation)
                    bodyanimator.Play("D" + (rollingtrigger == true ? (AnimationTrigger + 9).ToString() : AnimationTrigger.ToString()));
                else
                    bodyanimator.Play((rollingtrigger == true ? (AnimationTrigger + 9).ToString() : AnimationTrigger.ToString()));


                //if (Unit.unitName != "Player")
                //    Debug.Log((rollingtrigger == true ? (AnimationTrigger + 9).ToString() : AnimationTrigger.ToString()));


                if (AnimationTrigger == AnimationTypes.U_MoveAnimation || AnimationTrigger == AnimationTypes.UR_MoveAnimation ||
                    AnimationTrigger == AnimationTypes.UL_MoveAnimation)
                {
                    Unit.Attack.SortingLayer = 4;
                }
                else
                {
                    Unit.Attack.SortingLayer = 6;
                }

                if (tmove < Time.time)
                {
                    playAudio(0);

                    tmove = Time.time + 0.3f;
                }

            }
            //1.2 , 0.3 // 0,1.2
            if(Unit.unitName=="Player" && RollingTrigger && rollingeffect)
            {
                switch (((AnimationTypes)(AnimationTrigger + 9)))
                {
                    case AnimationTypes.U_RollingAnimation:
                        rollinganimator.Play("RollingEffect");
                        rollinganimator.transform.position = Unit.transform.position + new Vector3(0f, -1.2f, 0f);

                        rollinganimator.transform.rotation = Quaternion.Euler(0, 0, 180);
                        break;
                    case AnimationTypes.D_RollingAnimation:
                        rollinganimator.Play("RollingEffect");
                        rollinganimator.transform.position = Unit.transform.position + new Vector3(0f, 1.2f, 0f);

                        rollinganimator.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                    case AnimationTypes.R_RollingAnimation:
                        rollinganimator.Play("RollingEffect");
                        rollinganimator.transform.position = Unit.transform.position + new Vector3(-1.2f, -0.3f, 0f);

                        rollinganimator.transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    case AnimationTypes.L_RollingAnimation:
                        rollinganimator.Play("RollingEffect");
                        rollinganimator.transform.position = Unit.transform.position + new Vector3(1.2f, -0.3f, 0f);

                        rollinganimator.transform.rotation = Quaternion.Euler(0, 0, 270);
                        break;
                    case AnimationTypes.UR_RollingAnimation:
                        rollinganimator.Play("RollingEffect");
                        rollinganimator.transform.position = Unit.transform.position + new Vector3(-1.2f, -0.8f, 0f);

                        rollinganimator.transform.rotation = Quaternion.Euler(0, 0, 120);
                        break;
                    case AnimationTypes.UL_RollingAnimation:
                        rollinganimator.Play("RollingEffect");
                        rollinganimator.transform.position = Unit.transform.position + new Vector3(1.2f, -0.8f, 0f);

                        rollinganimator.transform.rotation = Quaternion.Euler(0, 0, 240);
                        break;
                    case AnimationTypes.DR_RollingAnimation:
                        rollinganimator.Play("RollingEffect");
                        rollinganimator.transform.position = Unit.transform.position + new Vector3(-1.2f, 0.2f, 0f);

                        rollinganimator.transform.rotation = Quaternion.Euler(0, 0, 60);
                        break;
                    case AnimationTypes.DL_RollingAnimation:
                        rollinganimator.Play("RollingEffect");
                        rollinganimator.transform.position = Unit.transform.position + new Vector3(1.2f, 0.2f, 0f);

                        rollinganimator.transform.rotation = Quaternion.Euler(0, 0, 300);
                        break;
                    default:
                        break;
                }
                rollingeffect = false;
            }
        }
    }

    private IEnumerator RollingTriggerSet()
    {

        bodyanimator.SetBool("Rolling", true);
        rollingeffect = true;
        playAudio(1);

        yield return new WaitForSeconds(0.5f);

        bodyanimator.SetBool("Rolling", false);

        rollingtrigger = false;
    }

    public void PlayAnimation(string name)
    {

        if (stopanimation)
            return;


        bodyanimator.Play(name);
    }

    public void PlayGunShot()
    {

        if (stopanimation)
            return;


        gunshotanimator.transform.localPosition = ((Vector3)Unit.Weapon.Bullet_Pos + (Vector3.right * 0.6f));
        AttackCard card = Unit.Cards.FindCard(Card.Properties.Attack).GetCard<AttackCard>();

        if (card == null)
            gunshotanimator.Play("GunShot");
        else if (card.Name == "FlameCard")
            gunshotanimator.Play("FlameShot");
        else if (card.Name == "FreezeCard")
            gunshotanimator.Play("FreezeShot");
        else if (card.Name == "ThunderCard")
            gunshotanimator.Play("ThunderShot");
    }

    public void WeaponChange()
    {

        if (stopanimation)
            return;


        if (gunanimator != null)
            gunanimator.Play(Unit.Weapon.Name);
    }

    public void WeaponShot()
    {

        if (stopanimation)
            return;


        if (gunanimator != null)
            gunanimator.Play(Unit.Weapon.Name + "Animation");
    }

    public void WeaponReload()
    {

        if (stopanimation)
            return;


        if (gunanimator != null)
            gunanimator.Play(Unit.Weapon.Name + "Reload");
    }

    public void playAudio(int num)
    {
        if (AudioPlayer == null)
            return;

        if (Unit.unitName == "Player")
        {
            //AudioPlayer.volume = SoundMng.Instance.SFX_volume;
            AudioPlayer.PlayOneShot((clips[num] != null ? clips[num] : clips[2]));
        }
    }

    public void DeadAnimation()
    {

        if (stopanimation)
            return;


        bodyanimator.Play("Dead");

        Invoke("destroy", 1f);
    }

    public void destroy()
    {
        if (Unit.unitName != "Player")
            Destroy(gameObject);
    }

    public void playAudio(AudioClip clip)
    {
        if (AudioPlayer == null)
            return;

        //AudioPlayer.volume = SoundMng.Instance.SFX_volume;
        AudioPlayer.PlayOneShot(clip);
    }

    public override void Init()
    {
        bodyanimator = Unit.transform.GetComponent<Animator>();
        gunanimator = Unit.Attack.transform.GetComponent<Animator>();

        if(Unit.unitName == "Player")
        {
            gunshotanimator = Unit.Attack.transform.GetChild(0).GetComponent<Animator>();
        }
    }


    #region Properties
    public AnimationTypes AnimationTrigger
    {
        get
        {
            return animationTrigger;
        }
        set
        {
            animationTrigger = value;
            PlayAnimation();
        }
    }

    public AudioSource AudioPlayer
    {
        get
        {
            if (audioplayer == null)
                audioplayer = transform.GetComponent<AudioSource>();

            return audioplayer;
        }
    }

    public bool RollingTrigger
    {
        get
        {
            return rollingtrigger;
        }
        set
        {
            if (value)
            {
                StartCoroutine("RollingTriggerSet");
            }

            rollingtrigger = value;
            PlayAnimation();
        }
    }

    public Animator BodyAnimator
    {
        get
        {
            return bodyanimator;
        }
    }

    public Animator WeaponAnimator
    {
        get
        {
            return gunanimator;
        }
    }
#endregion
}
