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

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        
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

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }        
        Destroy(gameObject);
        
    }

    void Explode()
    {
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
