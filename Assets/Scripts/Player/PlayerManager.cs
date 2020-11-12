using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject deathExplosion;
    public SpriteRenderer playerSprite;
    public HealthBar healthBar;
    public Light highlight;
    private float maxHealth = 100f;
    public float health;
    public int score = 0;
    public bool isAlive = true;
    public bool canTakeDamage = true;
    private CircleCollider2D hitbox;
    private PlayerController controller;
    private float pastHealth = 100;

    void Start()
    {
        health = maxHealth;
        hitbox = GetComponent<CircleCollider2D>();
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(health != pastHealth)
            healthBar.SetHealth(health);
        pastHealth = health;
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
        Destroy(explosion, 2f);
        playerSprite.enabled = false;
        hitbox.enabled = false;
        highlight.enabled = false;
    }
}
