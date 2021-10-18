using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    [SerializeField] ParticleSystem deathFX;

    [Header("Tune")]

    public float health = 100;
    public int moneyLoot = 50;
    public float startSpeed = 10f;
    [HideInInspector]public float speed;

    private void Start()
    {
        speed = startSpeed;
    }

    public void Slow (float slowAmount)
    {
        speed = startSpeed * (1f-slowAmount);
    }

    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= Mathf.Epsilon)
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

