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
    public AudioClip explosionClip;
    public AudioClip hitClip;
    private CircleCollider2D hitbox;
    private PlayerController controller;
    private AudioSource audioPlayer;

    void Start()
    {
        health = maxHealth;
        hitbox = GetComponent<CircleCollider2D>();
        controller = GetComponent<PlayerController>();
        audioPlayer = GetComponent<AudioSource>();
    }

    public void TakeDamage(float _damage)
    {
        if(canTakeDamage)
        {
            audioPlayer.PlayOneShot(hitClip);
            health -= _damage;
            StartCoroutine(controller.cameraShake.Shake(controller.shakeDuration, controller.shakeMagnitude));
            if(health <= 0 && isAlive)
                Die();
        }
    }

    private void Die()
    {
        audioPlayer.PlayOneShot(explosionClip);
        isAlive = false;
        DisableComponents();
        GameObject explosion = Instantiate(deathExplosion, transform.position, transform.rotation);
        GameState.currentState = GameState.LevelStatus.levelFailed;
    }

    private void DisableComponents()
    {
        playerSprite.enabled = false;
        hitbox.enabled = false;
        highlight.enabled = false;
    }
}
