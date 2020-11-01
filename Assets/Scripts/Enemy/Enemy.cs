using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float amplitude;
    public GameObject deathExplosion;
    private float maxHealth = 100;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;
        if(health <= 0)
            Die();
    }

    void Die()
    {
        GameObject explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
}
