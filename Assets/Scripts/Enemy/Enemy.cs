using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public bool randomDropsEnabled = false;
    public GameObject parent;
    public GameObject deathExplosion;
    public List<GameObject> randomDrops;
    public AudioClip explosionClip;

    private AudioSource audioPlayer;
    private Collider2D hitbox;

    private bool isDead = false;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        hitbox = GetComponent<Collider2D>();
        health = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        if(!isDead)
        {
            health -= _damage;
            if(health <= 0)
                Die();
        }
    }

    void Die()
    {
        isDead = true;
        hitbox.enabled = false;
        audioPlayer.PlayOneShot(explosionClip);
        GameObject explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
        if(randomDropsEnabled)
            Drop();
        PlayerVariables.player.IncreaseHealingMultiplyer(0.25f);
        Destroy(parent);
    }

    void Drop()
    {
        System.Random random = new System.Random();
        int spawn = random.Next(0, randomDrops.Count);
        if(spawn == 1)
        {
            int item = random.Next(0, randomDrops.Count);
            GameObject drop = Instantiate(randomDrops[item], transform.position, Quaternion.identity);
        }
    }
}
