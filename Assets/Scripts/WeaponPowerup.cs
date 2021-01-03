using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType { none, bigBullets, multiShot };

public class WeaponPowerup : MonoBehaviour
{
    [SerializeField] private PowerupType powerupType;
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
        if(_collider.GetComponent<Gun>() != null)
            Take(_collider.GetComponent<Gun>());
    }

    void Take(Gun _gun)
    {
        _gun.powerupType = powerupType;
        PickedUp();
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
