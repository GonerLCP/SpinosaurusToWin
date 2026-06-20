using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float minSpeed;
    public float maxSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp = new Vector2(minSpeed * Time.deltaTime * 5, 0);
        //translate our player transform via the temp vector2 amount
        transform.Translate(temp);
        //Vector2 velocity = Vector2.right;
        //velocity = velocity * minSpeed + (velocity.normalized * Time.deltaTime * minSpeed);
        //rb.linearVelocity = new Vector2(velocity.x,0f);
    }
}
