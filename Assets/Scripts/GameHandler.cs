using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;

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
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Scene_0" + SceneManager.GetActiveScene().buildIndex, "Stage_01", "Level_Progress");
    }

    void Update()
    {
        if (gameEnded) { return; }        
        if (PlayerStats.Lives <= 0)
        {
            LostLevel();
        }
    }
    void LostLevel()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Gold", EnemyScript.moneyFromEnemiesKilled, "Enemy", "Enemies");
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Lives", EnemyMovement.DamageAccumulatedToPlayer, "Enemy", "Enemies");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Scene_0" + SceneManager.GetActiveScene().buildIndex, "Stage_01", "Level_Progress");
        
    }

    public void WinLevel()
    {
        gameEnded = true;
        winUI.SetActive(true);
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Gold", EnemyScript.moneyFromEnemiesKilled, "Enemy", "Enemies");
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Lives", EnemyMovement.DamageAccumulatedToPlayer, "Enemy", "Enemies");
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Scene_0" + SceneManager.GetActiveScene().buildIndex, "Stage_01", "Level_Progress");
    }
}
