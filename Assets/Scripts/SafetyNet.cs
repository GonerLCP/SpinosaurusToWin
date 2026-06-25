using UnityEngine;

public class SafetyNet : MonoBehaviour
{
    public FadeInOut fade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fade.FadeIn(false);
        }

        if (collision.tag == "Steroids")
        {
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
