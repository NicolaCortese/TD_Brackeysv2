using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public void Play()
    {
        Debug.Log("playing");
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Quit()
    {
        Debug.Log("quitting");
        Application.Quit();
    }


}
