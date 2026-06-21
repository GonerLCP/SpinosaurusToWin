using UnityEngine;
using UnityEngine.InputSystem.HID;

public class NewPlayerController : MonoBehaviour
{
    public float gravity = -20f;
    public float jumpForce = 10f;

    private float verticalSpeed;
    public LayerMask layerMask;

    RaycastHit2D Hit2D;

    public float minSpeed;

    public SpinningWheel spinningW;

    public float DistanceToGround;

    private void Start()
    {
        spinningW.BigJumpAction += SautAvecPlanche;
    }
    void Update()
    {
        Hit2D = Physics2D.Raycast(transform.position, -Vector2.up, DistanceToGround, layerMask);

        //if (Input.GetKeyDown(KeyCode.E) && Hit2D!=false)
        //{
        //    verticalSpeed = jumpForce;
        //    print("EUHHH");
        //}

        verticalSpeed += gravity * Time.deltaTime;
        if (Hit2D != false && verticalSpeed < 0)
        {
            verticalSpeed = 0;
        }
        transform.Translate(new Vector2(minSpeed * Time.deltaTime * 5, verticalSpeed * Time.deltaTime),Space.World); 
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
            verticalSpeed = jumpForce;
            print("EUHHH");
        }
    }
}
