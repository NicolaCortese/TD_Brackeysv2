using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverOrWin : MonoBehaviour
{
    
    public Text wavesText;
    public SceneFader sceneFader;
    public static int startingLivesForRetry = 20;
    public static int startingMoneyForRetry = 250;
    public static int startingWavesSurvivedForRetry = 0;

    
    private void OnEnable()
    {
        Time.timeScale = 1f;
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
            yield return new WaitForSeconds(0.1f);
        }


    }

    public void Retry()
    {
        PlayerStats.Lives = startingLivesForRetry;
        PlayerStats.Money = startingMoneyForRetry;
        PlayerStats.wavesSurvived = startingWavesSurvivedForRetry;
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex);
    } 
    public void NextLevel()
    {
        PlayerStats.Money += 200;
        sceneFader.FadeTo(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void SelectLevel()
    {
        PlayerStats.Money += 250;
        sceneFader.FadeTo(1);
    }
    public void Menu() 
    {        
        sceneFader.FadeTo(0);
    }
}
