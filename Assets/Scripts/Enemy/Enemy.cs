using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public GameObject parent;
    public GameObject deathExplosion;

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
        Destroy(parent);
    }
}
