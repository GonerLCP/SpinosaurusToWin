using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasgroup;
    public bool fadein = false;
    public bool fadeout = false;
    public float TimeToFade;
    // Start is called before the first frame update
    void Start()
    {

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
    public void FadeIn()
    {
        fadein = true;
    }
    public void FadeOut()
    {
        fadeout = true;
    }
}