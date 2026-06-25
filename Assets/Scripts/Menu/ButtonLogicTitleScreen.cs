using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLogicTitleScreen : MonoBehaviour
{
    public GameObject ControlsPanel;
    public GameObject BGmusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ControlsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Controls()
    {
        ControlsPanel.SetActive(true);
    }

    public void Close()
    {
        ControlsPanel.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitLetruc()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        GameObject BGmusicInstantiated = GameObject.FindGameObjectsWithTag("Music")[0];
        Destroy(BGmusicInstantiated);
        if (BGmusic != null) {Destroy(BGmusic);}
        SceneManager.LoadScene("TitleScreen");
    }

}
