using UnityEngine;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{
    public GameObject player;
    public GameObject PlayeSprite;
    public GameObject camera;
    public GameObject Panel;
    public FadeInOut fade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            camera.transform.SetParent(null);
            fade.FadeIn(true);
        }
    }
}
