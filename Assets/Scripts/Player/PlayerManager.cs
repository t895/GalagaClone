using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject deathExplosion;
    public SpriteRenderer playerSprite;
    public float health;
    public int score = 0;
    public bool isAlive = true;
    public bool canTakeDamage = true;
    private float maxHealth = 100f;
    private CircleCollider2D hitbox;
    private Light highlight;

    void Start()
    {
        health = maxHealth;
        hitbox = GetComponent<CircleCollider2D>();
        highlight = GetComponent<Light>();
    }

    public void TakeDamage(float _damage)
    {
        if(canTakeDamage)
        {
            health -= _damage;
            if(health <= 0 && isAlive)
                Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        GameObject explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
        Destroy(explosion, 2f);
        playerSprite.enabled = false;
        hitbox.enabled = false;
        highlight.enabled = false;
    }
}
