using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    
    [SerializeField] ParticleSystem deathFX;

    [Header("Tune")]

    public float startingHealth = 100;
    public int moneyLoot = 50;
    public float startSpeed = 10f;
    [HideInInspector] public float speed;
    private float currentHealth;

    [Header("Unity Stuff")]
    public Image healthBar;

    private void Start()
    {
        speed = startSpeed;
        currentHealth = startingHealth;
    }

    public void Slow (float slowAmount)
    {
        speed = startSpeed * (1f-slowAmount);
    }

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth/startingHealth;
        if (currentHealth <= Mathf.Epsilon)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        PlayerStats.Money += moneyLoot;
        Instantiate<ParticleSystem>(deathFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    
}

