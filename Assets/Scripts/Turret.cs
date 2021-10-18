using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private EnemyScript targetEnemy;

    [Header("General")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    
    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 20;
    public float slowRate = 0.3f;

    public LineRenderer lineRenderer;
    public ParticleSystem laserImpactFX;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";
    public Transform TurretToRotate;


    private void Start()
    {
        InvokeRepeating("CheckForTargets", 0f, 0.5f);
    }

    void CheckForTargets()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<EnemyScript>();
        }
        else
        {
            target = null;
        }
    }


    void Update()
    {
        if(target==null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserImpactFX.Stop();
                    impactLight.enabled = false;
                    
                }
            }
            return;
        }
        AimTarget();

        if (useLaser)
        {
            Lasering();
        }
        else 
        {
            if (fireCountdown <= Mathf.Epsilon)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime; //Change this so that the tower reloads also when there is no target!
        }

    }

    private void Lasering()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserImpactFX.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;
        laserImpactFX.transform.position = target.position + dir.normalized;
        laserImpactFX.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

   
    void AimTarget()
    {
        //ADD LERP
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;

        TurretToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
