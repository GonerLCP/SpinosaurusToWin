using UnityEngine;


public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroupFail;
    public CanvasGroup canvasGroupSuccess;
    CanvasGroup canvasgroup;
    public bool fadein = false;
    public bool fadeout = false;
    public float TimeToFade;
    public AudioClip Explosions;
    public float ExplosionSound;
    bool doOnce;
    // Start is called before the first frame update
    void Start()
    {
        doOnce = true;
        canvasGroupFail.gameObject.SetActive(false);
        canvasGroupSuccess.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadein == true)
        {
            if (canvasgroup.alpha < 1)
            {
                canvasgroup.alpha += TimeToFade * Time.deltaTime;
                if (canvasgroup.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
    }
    public void FadeIn(bool IsEnding)
    {
        fadein = true;        
        DoOnce(IsEnding);
    }
    public void FadeOut()
    {
        fadeout = true;
    }

    void DoOnce(bool IsEnding)
    {
        if (doOnce ==false)
        {
            return;
        }
        canvasgroup = IsEnding ? canvasGroupSuccess : canvasGroupFail;
        canvasgroup.gameObject.SetActive(true);
        SoundFXManager.Instance.PlaySoundFXClip(Explosions, transform, ExplosionSound);
        doOnce = false;
    }
}