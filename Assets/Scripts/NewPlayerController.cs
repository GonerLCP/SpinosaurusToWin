using System;
using System.Collections;
using UnityEngine;
public enum PlayerState
{   BaseSpinoWoSkate,
    JumpWindup,
    DuringJump,
    OllieWindup,
    DuringOllie,
    Dead,
    SpeedingWindup,
    DuringSpeeding,
    Screaming
}
public class NewPlayerController : MonoBehaviour
{
    public float gravity = -20f;
    public float bigJumpForce = 10f;
    public float smallJumpForce = 10f;
    public float jumpWindupLength;
    public float speedingWindupLength;

    private float verticalSpeed;
    public LayerMask layerMask;

    RaycastHit2D Hit2D;

    public float minSpeed;
    public float actualSpeed;
    public float speedIncrease;
    public float speedDecrease;

    public SpinningWheel spinningW;

    public float DistanceToGround;

    public Skate skate;

    bool fail=false;
    public bool skateFail = false;

    public Sprite spriteDead;
    public Sprite baseSkate;
    public Sprite ollieSkate;

    public GameObject Panel;

    SpriteChanger spriteChanger;

    PlayerState stateOfThePlayer;

    public FadeInOut fade;

    public AudioClip[] jumpSound;
    public AudioClip landingSound;
    public AudioClip[] rolling;
    public AudioClip deathSound;

    AudioSource playerAudioSource;

    private IEnumerator coroutineRolling;

    bool rollingCLipDone;
    bool landingSoundTrigger;
    private void Start()
    {
        rollingCLipDone = true;
        landingSoundTrigger = false;
        spinningW.BigJumpAction += SautAvecPlanche;
        spinningW.SmallJumpAction += SautSansPlanche;
        spinningW.AccelAction += Accelleration;
        actualSpeed = minSpeed;
        spriteChanger = GetComponent<SpriteChanger>();
        playerAudioSource = GetComponent<AudioSource>();
        //Panel.SetActive(false);
    }
    void Update()
    {
        //Raycast si si la famille
        Hit2D = Physics2D.Raycast(transform.position, -Vector2.up, DistanceToGround, layerMask);
        if (Hit2D) 
        { 
            skate.ReparentingSkate();
            //rolling clip audio
            if (rollingCLipDone)
            {
                int rand = UnityEngine.Random.Range(0, rolling.Length);
                playerAudioSource.clip = rolling[rand];
                playerAudioSource.Play();
                rollingCLipDone = false;
                coroutineRolling = rollingSoundClipHandler(rolling[rand].length);
                StartCoroutine(coroutineRolling);
            }
            if (landingSoundTrigger)
            {
                SoundFXManager.Instance.PlaySoundFXClip(landingSound, transform, 1f);
                landingSoundTrigger = false;
            }
        }
        else
        {
            playerAudioSource.Stop();
            StopCoroutine(coroutineRolling);
            rollingCLipDone = true;
            landingSoundTrigger = true;
        }

        //Skate heurter collision check
        if (Hit2D && skateFail == true) { failing(); } 

        //Remettre sprite player base
        if (Hit2D && stateOfThePlayer != PlayerState.BaseSpinoWoSkate && stateOfThePlayer != PlayerState.OllieWindup && stateOfThePlayer != PlayerState.JumpWindup && stateOfThePlayer != PlayerState.SpeedingWindup)
        {
            stateOfThePlayer = PlayerState.BaseSpinoWoSkate;
            spriteChanger.ChangeSprite(stateOfThePlayer);
            skate.GetComponent<SpriteRenderer>().sprite = baseSkate;
        }

        //Handle gravity
        verticalSpeed += gravity * Time.deltaTime;
        if (Hit2D != false && verticalSpeed < 0)
        {
            verticalSpeed = 0;
        }


        //deplacement personnage
        if (!fail) //Non fail , fonctionnement normal
        {
            if (actualSpeed > minSpeed)
            {
                actualSpeed -= speedDecrease;
                if (actualSpeed < minSpeed) { actualSpeed = minSpeed; }
            }
            transform.Translate(new Vector2(actualSpeed * Time.deltaTime * 5, verticalSpeed * Time.deltaTime), Space.World);
        }
        else //fail, decrease de la speed + arret + afficher screen defaite
        {
            actualSpeed -= speedDecrease; if (actualSpeed <0) {actualSpeed = 0; fade.FadeIn(); }
            transform.Translate(new Vector2(actualSpeed * Time.deltaTime * 5, verticalSpeed * Time.deltaTime), Space.World);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteDead; //spriteChanger.ChangeSprite(PlayerState.Dead);
        }


    }

    public void SautAvecPlanche()
    {
        if ( Hit2D != false && !fail)
        {
            StartCoroutine(DelayBeforeJump(PlayerState.OllieWindup, PlayerState.DuringOllie, smallJumpForce));
            skate.GetComponent<SpriteRenderer>().sprite = ollieSkate;
            StartCoroutine(SlightDelayForOllie());
        }
    }

    public void SautSansPlanche()
    {
        if (Hit2D != false && !fail)
        {
            StartCoroutine(DelayBeforeJump(PlayerState.JumpWindup, PlayerState.DuringJump,bigJumpForce));
        }
    }

    public void Accelleration()
    {
        StartCoroutine(DelayBeforeSpeeding(PlayerState.SpeedingWindup,PlayerState.DuringSpeeding));
    }

    void failing()
    {
        fail = true;
        spinningW.IsPlayerDead = true;
        SoundFXManager.Instance.PlaySoundFXClip(deathSound, transform, .5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && this.tag == "Player")
        {
            failing();
        }
    }

    IEnumerator SlightDelayForOllie()
    {
        yield return new WaitForSeconds(jumpWindupLength);
        skate.GetComponent<SpriteRenderer>().sprite = baseSkate;
    }

    IEnumerator DelayBeforeSpeeding(PlayerState windUpState, PlayerState StateState)
    {
        stateOfThePlayer = windUpState;
        spriteChanger.ChangeSprite(windUpState);
        yield return new WaitForSeconds(speedingWindupLength);
        spriteChanger.ChangeSprite(StateState);
        actualSpeed += speedIncrease;
        yield return new WaitForSeconds(0.4f);
        stateOfThePlayer = StateState;
    }

    IEnumerator DelayBeforeJump(PlayerState windUpState, PlayerState JumpState, float jumpForce)
    {
        stateOfThePlayer = windUpState;
        spriteChanger.ChangeSprite(windUpState);
        yield return new WaitForSeconds(jumpWindupLength);
        spriteChanger.ChangeSprite(JumpState);
        verticalSpeed = bigJumpForce;
        SoundFXManager.Instance.PlayRandomSoundFXClip(jumpSound, transform, 1f);
        yield return new WaitForSeconds(0.2f);
        stateOfThePlayer = JumpState;
    }

    IEnumerator rollingSoundClipHandler(float rollingClipDuration)
    {
        yield return new WaitForSeconds(rollingClipDuration);
        rollingCLipDone = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawRay(transform.position, -Vector2.up * DistanceToGround);
    }
}
