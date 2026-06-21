using System.Collections;
using UnityEngine;

public class Skate : MonoBehaviour
{
    bool jumpingWithoutBoard;
    public GameObject Player;
    public SpinningWheel spinningW;
    bool tempEnLair;
    float YAuMomentDuSaut;
    Vector3 PositionLocale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PositionLocale = transform.localPosition;
        tempEnLair = false;
        jumpingWithoutBoard = false;
        spinningW.SmallJumpAction += SautSansPlanche;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpingWithoutBoard)
        {
            if (!tempEnLair) { transform.SetParent(null, true); StartCoroutine(WaitToLeaveTheGround()); }
            transform.position = new Vector3(Player.transform.position.x, YAuMomentDuSaut, Player.transform.position.z);
        }
    }

    private void SautSansPlanche()
    {
        jumpingWithoutBoard = true;
        YAuMomentDuSaut = transform.position.y;
    }

    public void ReparentingSkate()
    {
        print("repar");
        if (!tempEnLair) { return; }
        jumpingWithoutBoard = false;
        tempEnLair = false;
        transform.SetParent(Player.transform.GetChild(0));
        transform.localPosition = PositionLocale;
        transform.localRotation = Quaternion.Euler(0,0,0);
    }

    IEnumerator WaitToLeaveTheGround()
    {
        yield return new WaitForSeconds(0.2f);
        tempEnLair = true;
    }
}
