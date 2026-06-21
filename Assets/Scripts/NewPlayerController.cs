using UnityEngine;
using UnityEngine.InputSystem.HID;

public class NewPlayerController : MonoBehaviour
{
    public float gravity = -20f;
    public float bigJumpForce = 10f;
    public float smallJumpForce = 10f;

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

    private void Start()
    {
        spinningW.BigJumpAction += SautAvecPlanche;
        spinningW.SmallJumpAction += SautSansPlanche;
        spinningW.AccelAction += Accelleration;
        actualSpeed = minSpeed;
    }
    void Update()
    {
        Hit2D = Physics2D.Raycast(transform.position, -Vector2.up, DistanceToGround, layerMask);
        if (Hit2D){skate.ReparentingSkate();}
        verticalSpeed += gravity * Time.deltaTime;
        if (Hit2D != false && verticalSpeed < 0)
        {
            verticalSpeed = 0;
        }

        if (actualSpeed > minSpeed)
        {
            actualSpeed -= speedDecrease;
            if(actualSpeed < minSpeed) { actualSpeed = minSpeed; }
        }
        transform.Translate(new Vector2(actualSpeed * Time.deltaTime * 5, verticalSpeed * Time.deltaTime),Space.World); 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawRay(transform.position, -Vector2.up * DistanceToGround);
    }

    public void SautAvecPlanche()
    {
        if ( Hit2D != false)
        {
            verticalSpeed = smallJumpForce;
        }
    }

    public void SautSansPlanche()
    {
        if (Hit2D != false)
        {
            verticalSpeed = bigJumpForce;
        }
    }

    public void Accelleration()
    {
        actualSpeed += speedIncrease;
    }
}
