using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float minSpeed;
    public float maxSpeed;

    // Update is called once per frame
    void Update()
    {
        Vector2 temp = new Vector2(minSpeed * Time.deltaTime * 5, 0);
        transform.Translate(temp);
    }
}
