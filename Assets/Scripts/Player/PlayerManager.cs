using System.Collections;
using UnityEngine;

public static class PlayerVariables
{
    public static PlayerManager playerManager;
    public static PlayerController playerController;
    public static PlayerControls playerControls;
    public static PlayerGun playerGun;
    public static PlayerMelee playerMelee;
    public static CameraShake cameraShake;
    public static float playerHealAmount = 0.3f;
    public static float playerMultiplyer = 0f;
}

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
    public bool invincible = false;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip hitClip;

    [SerializeField] private int multiplyerDecayTime = 1;
    [SerializeField] private float multiplyerDecay = 0.5f;

    private CircleCollider2D hitbox;
    private AudioSource audioPlayer;

    private void Awake()
    {
        PlayerVariables.playerManager = this;
    }

    private void Start()
    {
        health = maxHealth;
        meleePower = maxMeleePower;
        hitbox = GetComponent<CircleCollider2D>();
        audioPlayer = GetComponent<AudioSource>();
        meleeRechargeRate *= Time.deltaTime;
        PlayerVariables.playerMultiplyer = 0f;

        StartCoroutine(MultiplyerReduction(multiplyerDecayTime, multiplyerDecay));
    }
    private void Update()
    {
        if(!GameState.paused)
        {
            if(meleePower < maxMeleePower)
                meleePower += meleeRechargeRate * Time.deltaTime;

            if(meleePower > 100f)
                meleePower = 100f;
        }
    }

    public void TakeDamage(float _damage)
    {
        if(canTakeDamage && !invincible)
        {
            audioPlayer.PlayOneShot(hitClip);
            health -= _damage;
            StartCoroutine(PlayerVariables.cameraShake.Shake(PlayerVariables.playerController.shakeDuration, PlayerVariables.playerController.shakeMagnitude));
            if(health <= 0 && isAlive)
                Die();
        }
    }

    public void Heal(float _healAmount)
    {
        if(health < 100)
            health += _healAmount;
        if(health > 100)
            health = maxHealth;
    }

    public void HealWithMultiplyer()
    {
        Heal(PlayerVariables.playerHealAmount * PlayerVariables.playerMultiplyer);
    }

    public void IncreaseMultiplyer(float _amount)
    {
        if(_amount > 0)
            PlayerVariables.playerMultiplyer += _amount;
        else
            Debug.LogWarning("Tried to increase multiplyer with a negative number! - " + _amount);
    }

    private IEnumerator MultiplyerReduction(int _multiplyerDecayTime, float _multiplyerDecay)
    {
        while(true)
        {
            yield return new WaitForSeconds(_multiplyerDecayTime);
            if(PlayerVariables.playerMultiplyer - _multiplyerDecay <= 0)
                PlayerVariables.playerMultiplyer = 0;
            else
                PlayerVariables.playerMultiplyer -= _multiplyerDecay;
        }
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
