using System.Collections;
using UnityEngine;
using System;


public class BirdScript : MonoBehaviour
{
    public SpinningWheel spinningW;
    bool playerInRadius;
    bool movingBirds;
    public event Action Scream;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spinningW.ScreamingAction += CriPourLesOiseaux;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingBirds == true) { transform.Translate(new Vector2(0.3f, 0.3f)); }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerChild")
        {
            playerInRadius = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerChild")
        {
            playerInRadius = false;
        }
    }
    void CriPourLesOiseaux()
    {
        if (playerInRadius)
        {
            transform.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(AnimationFlyingBird());
        }
    }

    IEnumerator AnimationFlyingBird()
    {
        movingBirds = true;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);

    }
}
