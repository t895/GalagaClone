using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public GameObject parent;
    public GameObject deathExplosion;
    public AudioClip explosionClip;
    public AudioClip hitClip;
    private AudioSource audioPlayer;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        health = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        audioPlayer.PlayOneShot(hitClip);
        health -= _damage;
        if(health <= 0)
            Die();
    }

    void Die()
    {
        audioPlayer.PlayOneShot(explosionClip);
        GameObject explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
        Destroy(parent);
    }
}
