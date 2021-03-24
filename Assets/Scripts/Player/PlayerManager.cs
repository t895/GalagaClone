using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject deathExplosion;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Light highlight;
    public float maxHealth = 100f;
    public float maxMeleePower = 100f;
    public float meleePower;
    public float meleeRechargeRate = 5f;
    public float health;
    public bool isAlive = true;
    public bool canTakeDamage = true;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip hitClip;

    private CircleCollider2D hitbox;
    private PlayerController controller;
    private AudioSource audioPlayer;

    void Start()
    {
        health = maxHealth;
        meleePower = maxMeleePower;
        hitbox = GetComponent<CircleCollider2D>();
        controller = GetComponent<PlayerController>();
        audioPlayer = GetComponent<AudioSource>();
        meleeRechargeRate *= Time.deltaTime;
    }
    void Update()
    {
        if(!GameState.paused)
        {
            if(meleePower < maxMeleePower)
                meleePower += meleeRechargeRate;

            if(meleePower > 100f)
                meleePower = 100f;
        }
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

    public void Heal(float _healAmount)
    {
        if(health < 100)
            health += _healAmount;
    }

    public void Recharge(float _rechargeAmount)
    {
        if(meleePower < 100)
            meleePower += _rechargeAmount;
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
