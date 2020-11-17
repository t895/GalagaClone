using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject deathExplosion;
    public SpriteRenderer playerSprite;
    public HealthBar healthBar;
    public Light highlight;
    public float maxHealth = 100f;
    public float health;
    public int score = 0;
    public bool isAlive = true;
    public bool canTakeDamage = true;
    private CircleCollider2D hitbox;
    private PlayerController controller;

    void Start()
    {
        health = maxHealth;
        hitbox = GetComponent<CircleCollider2D>();
        controller = GetComponent<PlayerController>();
    }

    public void TakeDamage(float _damage)
    {
        if(canTakeDamage)
        {
            health -= _damage;
            StartCoroutine(controller.cameraShake.Shake(controller.shakeDuration, controller.shakeMagnitude));
            if(health <= 0 && isAlive)
                Die();
        }
    }

    private void Die()
    {
        isAlive = false;
        GameObject explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
        playerSprite.enabled = false;
        hitbox.enabled = false;
        highlight.enabled = false;
    }
}
