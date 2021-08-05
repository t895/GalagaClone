using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public float multiplyerIncrease = 0.25f;
    public bool randomDropsEnabled = false;
    public GameObject parent;
    public GameObject deathExplosion;
    public List<GameObject> randomDrops;
    public AudioClip explosionClip;

    private AudioSource audioPlayer;
    private Collider2D hitbox;

    private bool isDead = false;

    private void Start()
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

    private void Die()
    {
        isDead = true;
        hitbox.enabled = false;
        audioPlayer.PlayOneShot(explosionClip);
        //GameObject explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
        ObjectPooler.Instance
            .SpawnFromPool(PooledObject.EnemyDeathExplosion, gameObject.transform.position, gameObject.transform.rotation);
        if(randomDropsEnabled)
            Drop();
        PlayerVariables.playerManager.IncreaseMultiplyer(multiplyerIncrease);
        Destroy(parent);
    }

    private void Drop()
    {
        System.Random random = new System.Random();
        int spawn = random.Next(0, randomDrops.Count);
        if(spawn == 1)
        {
            int item = random.Next(0, randomDrops.Count);
            Instantiate(randomDrops[item], transform.position, Quaternion.identity);
        }
    }
}
