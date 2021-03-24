using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType { health, melee };

public class StatPickup : MonoBehaviour
{
    [SerializeField] private PickupType pickupType;
    [SerializeField] private float amount = 0f;
    [SerializeField] private float timeToDespawn;
    public AudioClip pickupClip;
    public AudioClip despawnClip;
    private AudioSource audioPlayer;
    private ParticleSystem pickupAnimation;
    private SpriteRenderer sprite;
    private BoxCollider2D hitbox;
    
    void Start()
    {
        pickupAnimation = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        audioPlayer = GetComponent<AudioSource>();
        hitbox = GetComponent<BoxCollider2D>();
        StartCoroutine(WaitForDespawn());
    }

    void OnTriggerEnter2D(Collider2D _collider)
    {
        if(_collider.GetComponent<PlayerManager>() != null)
            Take(_collider.GetComponent<PlayerManager>());
    }

    void Take(PlayerManager _player)
    {
        if(pickupType == PickupType.health && _player.health < _player.maxHealth)
        {
            _player.Heal(amount);
            Despawn(pickupClip);
        }
        else if(pickupType == PickupType.melee && _player.meleePower < _player.maxMeleePower)
        {
            _player.Recharge(amount);
            Despawn(pickupClip);
        }
    }

    void Despawn(AudioClip _clip)
    {
        audioPlayer.PlayOneShot(_clip);
        pickupAnimation.Play();
        sprite.enabled = false;
        hitbox.enabled = false;
        Destroy(gameObject, 1f);
    }

    private IEnumerator WaitForDespawn()
    {
        yield return new WaitForSeconds(timeToDespawn);
        Despawn(despawnClip);
    }
}
