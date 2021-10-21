using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    public int waveNumber = 0;
    public Text waveCountdownText;

    private float countdown = 4f;

    private void Update() 
    {
        if (EnemiesAlive > 0) { return; }
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
        
        PlayerStats.wavesSurvived++;
        countdown = timeBetweenWaves;

        Wave wave = waves[waveNumber];
        
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f*wave.spawnRate);            
        }
        waveNumber++;

        if(waveNumber == waves.Length)
        {
            Debug.Log("level won");
            this.enabled = false;
        }
        
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
        
    }
    
    


}
