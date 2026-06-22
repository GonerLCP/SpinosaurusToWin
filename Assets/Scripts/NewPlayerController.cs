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
    Speeding,
    Screaming
}
public class NewPlayerController : MonoBehaviour
{
    public float gravity = -20f;
    public float bigJumpForce = 10f;
    public float smallJumpForce = 10f;
    public float windupLength;

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
    public GameObject Panel;

    SpriteChanger spriteChanger;

    PlayerState stateOfThePlayer;

    public FadeInOut fade;
    private void Start()
    {
        spinningW.BigJumpAction += SautAvecPlanche;
        spinningW.SmallJumpAction += SautSansPlanche;
        spinningW.AccelAction += Accelleration;
        actualSpeed = minSpeed;
        spriteChanger = GetComponent<SpriteChanger>();
        //Panel.SetActive(false);
    }
    void Update()
    {
        Hit2D = Physics2D.Raycast(transform.position, -Vector2.up, DistanceToGround, layerMask);
        if (Hit2D) { skate.ReparentingSkate(); }
        if(Hit2D && skateFail == true) { fail = true; }
        if (Hit2D && stateOfThePlayer != PlayerState.BaseSpinoWoSkate && stateOfThePlayer != PlayerState.OllieWindup && stateOfThePlayer != PlayerState.JumpWindup)
        {
            stateOfThePlayer = PlayerState.BaseSpinoWoSkate;
            spriteChanger.ChangeSprite(stateOfThePlayer);
        }
        verticalSpeed += gravity * Time.deltaTime;
        if (Hit2D != false && verticalSpeed < 0)
        {
            verticalSpeed = 0;
        }

        if (!fail)
        {
            if (actualSpeed > minSpeed)
            {
                actualSpeed -= speedDecrease;
                if (actualSpeed < minSpeed) { actualSpeed = minSpeed; }
            }
            transform.Translate(new Vector2(actualSpeed * Time.deltaTime * 5, verticalSpeed * Time.deltaTime), Space.World);
        }
        else
        {
            actualSpeed -= speedDecrease; if (actualSpeed <0) {actualSpeed = 0; fade.FadeIn(); }
            transform.Translate(new Vector2(actualSpeed * Time.deltaTime * 5, verticalSpeed * Time.deltaTime), Space.World);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteDead;
        }


    }

    public void SautAvecPlanche()
    {
        if ( Hit2D != false && !fail)
        {
            StartCoroutine(DelayBeforeJump(PlayerState.OllieWindup, PlayerState.DuringOllie, smallJumpForce));
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
        actualSpeed += speedIncrease;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && this.tag == "Player")
        {
            fail = true;
        }
    }

    IEnumerator DelayBeforeJump(PlayerState windUpState, PlayerState JumpState, float jumpForce)
    {
        stateOfThePlayer = windUpState;
        spriteChanger.ChangeSprite(windUpState);
        yield return new WaitForSeconds(windupLength);
        spriteChanger.ChangeSprite(JumpState);
        verticalSpeed = bigJumpForce;
        yield return new WaitForSeconds(0.2f);
        stateOfThePlayer = JumpState;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawRay(transform.position, -Vector2.up * DistanceToGround);
    }
}
