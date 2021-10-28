using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public AudioClip turretLoadSFX;
    public void Play()
    {
        GetComponent<AudioSource>().PlayOneShot(turretLoadSFX,0.8f);
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Quit()
    {
        Debug.Log("quitting");
        Application.Quit();
    }


}
