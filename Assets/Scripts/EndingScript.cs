using UnityEngine;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{
    public GameObject player;
    public GameObject PlayeSprite;
    public GameObject camera;
    public GameObject Panel;
    public FadeInOut fade;
    public AudioClip winningCasino;
    public GameObject Sky;

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
            SoundFXManager.Instance.PlaySoundFXClip(winningCasino, transform, 1f);
            player.GetComponent<AudioSource>().mute = true;
            camera.transform.SetParent(null);
            Sky.transform.SetParent(null); //Le ciel qui bouge avec le joueur
            fade.FadeIn(true);
        }
    }
}
