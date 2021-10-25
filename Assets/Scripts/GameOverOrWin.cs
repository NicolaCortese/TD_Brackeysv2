using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverOrWin : MonoBehaviour
{
    
    public Text wavesText;
    public SceneFader sceneFader;

    private void OnEnable()
    {
        
        StartCoroutine(WavesSurvived());
    }

    IEnumerator WavesSurvived()
    {
        wavesText.text = "0";
        int waves = 0;
        yield return new WaitForSeconds(0.8f);

        while (waves < PlayerStats.wavesSurvived)
        {
            waves++;
            wavesText.text = waves.ToString();
            yield return new WaitForSeconds(0.3f);
        }


    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex);
    } 
    public void NextLevel()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void SelectLevel()
    {
        sceneFader.FadeTo(1);
    }
    public void Menu() 
    {        
        sceneFader.FadeTo(0);
    }
}
