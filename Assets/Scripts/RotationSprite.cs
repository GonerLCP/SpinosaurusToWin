using UnityEngine;

public class RotationSprite : MonoBehaviour
{
    public LayerMask groundLayer;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 3.5f, groundLayer);

        if (hit)
        {
            Vector2 normal = hit.normal;

            float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        }
    }
}
