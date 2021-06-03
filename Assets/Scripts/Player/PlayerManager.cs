using System.Collections;
using UnityEngine;

[System.Serializable]
public static class PlayerVariables
{
    public static PlayerManager player;
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
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip hitClip;

    [SerializeField] private float healingReduction = 0.05f;
    [SerializeField] private float healingIncrease = 0.075f;

    [SerializeField] private int multiplyerDecayTime = 1;
    [SerializeField] private float multiplyerDecay = 0.5f;

    private CircleCollider2D hitbox;
    private PlayerController controller;
    private AudioSource audioPlayer;

    void Awake()
    {
        PlayerVariables.player = this;
    }

    void Start()
    {
        health = maxHealth;
        meleePower = maxMeleePower;
        hitbox = GetComponent<CircleCollider2D>();
        controller = GetComponent<PlayerController>();
        audioPlayer = GetComponent<AudioSource>();
        meleeRechargeRate *= Time.deltaTime;

        StartCoroutine(MultiplyerReduction(multiplyerDecayTime, multiplyerDecay));
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
        if(health > 100)
            health = maxHealth;
    }

    public void HealWithMultiplyer()
    {
        Heal(PlayerVariables.playerHealAmount * PlayerVariables.playerMultiplyer);
    }

    public void IncreaseHealingMultiplyer(float _amount)
    {
        if(_amount != 0)
            if((PlayerVariables.playerMultiplyer + _amount) > 0)
                PlayerVariables.playerMultiplyer += _amount;
        else
            PlayerVariables.playerMultiplyer += healingIncrease;
    }

    public void ReduceHealingMultiplyer()
    {
        if((PlayerVariables.playerMultiplyer - healingReduction) > 0)
            PlayerVariables.playerMultiplyer -= healingReduction;
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
