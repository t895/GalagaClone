using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType { health, melee };

public class StatPickup : MonoBehaviour
{
    [SerializeField] private PickupType pickupType;
    [SerializeField] private float amount = 0f;
    public AudioClip pickupClip;
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
            PickedUp();
        }
        else if(pickupType == PickupType.melee && _player.meleePower < _player.maxMeleePower)
        {
            _player.Recharge(amount);
            PickedUp();
        }
    }

    void PickedUp()
    {
        audioPlayer.PlayOneShot(pickupClip);
        pickupAnimation.Play();
        sprite.enabled = false;
        hitbox.enabled = false;
        Destroy(gameObject, 2f);
    }
}
