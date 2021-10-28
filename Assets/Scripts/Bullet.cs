using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public ParticleSystem HitFX;
    public float bulletSpeed = 50f;
    public float explosionRadius = 0f;
    public int damage = 10;
    public AudioClip hitSFX;
    private bool hasHitTarget = false;
    public bool isMissile = false;


    public void Seek(Transform _target)
    {
        target = _target;
        if (isMissile){GetComponent<AudioSource>().Play();}
    }

    
    void Update()
    {
        if (target == null)
        {
            if (!hasHitTarget&&isMissile)
            {
                hasHitTarget = true;                
                Impact();
                return;
            }
            if (hasHitTarget)
            {
                return;                
            }
            Destroy(gameObject);
            return;
        }
        if (hasHitTarget) { return; }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            Impact();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);


    }
    void Impact()
    {
        Instantiate(HitFX,transform.position,transform.rotation);
        if (isMissile) { GetComponent<AudioSource>().Stop();};            
        GetComponent<AudioSource>().PlayOneShot(hitSFX,0.3f);

        if (isMissile)
        {
            Explode();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(gameObject,2f);
            return;
        }
        else
        {
            hasHitTarget = true;
            Damage(target);
            Destroy(gameObject,0.1f);
        }        
    }

    void Explode()
    {
        hasHitTarget = true;
        Collider [] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hitobject in hitObjects)
        {
            if (hitobject.tag == "Enemy")
            {
                Damage(hitobject.transform);
            }
        }
    }
    void Damage(Transform enemy)
    {
        EnemyScript e = enemy.GetComponent<EnemyScript>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
