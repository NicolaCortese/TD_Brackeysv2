using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemiesAlive = 0;
    public GameHandler gameHandler;

    public Wave[] waves;

    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;
    [HideInInspector]public int waveNumber = 0;
    private float countdown = 4f;


    private void Update() 
    {
        if (GameHandler.gameEnded) { return; }
        if (EnemiesAlive > 0) { return; }
        if (waveNumber == waves.Length)
        {
            gameHandler.WinLevel();
            this.enabled = false;
            return;
        }
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            return;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}",countdown);

    }
    IEnumerator SpawnWave(){
        countdown = timeBetweenWaves;
        PlayerStats.wavesSurvived++;
        Wave wave = waves[waveNumber];
        EnemiesAlive = wave.count;
        waveNumber++;
        
        
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f*wave.spawnRate);            
        }
        

        

    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        
        
    }
    
    


}
