using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static bool gameEnded;
    public GameObject gameOverUI;
    

    private void Start()
    {
        gameEnded = false;
        gameOverUI.SetActive(false);
        

    }

    void Update()
    {
        if (gameEnded) { return; }
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }
    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }
}
