using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    float countdown = 2f;
    public int waveNumber = 1;
    public Text waveCountdownText;

    private void Update() 
    {       
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}",countdown);
    }
    IEnumerator SpawnWave(){
        
        waveNumber++;
        PlayerStats.wavesSurvived++;

        countdown = timeBetweenWaves;

        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
            
        }
        
        
    }
    void SpawnEnemy(){
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        
    }
    
    


}
