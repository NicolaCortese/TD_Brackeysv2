using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static bool gameEnded;
    public GameObject gameOverUI;
    public GameObject winUI;    
        
    public int levelToUnlock = 2;
    public SceneFader sceneFader;

    private void Start()
    {
        gameEnded = false;
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        GameOverOrWin.startingMoneyForRetry = PlayerStats.Money;
        GameOverOrWin.startingLivesForRetry = PlayerStats.Lives;

    }

    void Update()
    {
        if (gameEnded) { return; }
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
        if (Input.GetKeyDown("r"))
        {
            WinLevel();
        }
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }
    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        gameEnded = true;
        winUI.SetActive(true);
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        
    }
}
