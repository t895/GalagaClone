using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType { none, bigBullets, tripleShot, octaShot };

public class WeaponPowerup : MonoBehaviour
{
    [SerializeField] private PowerupType powerupType;
    [SerializeField] private int powerupDuration;
    [SerializeField] private GameObject customBullet;
    [SerializeField] private float customRate;
    [SerializeField] private float timeToDespawn;
    public AudioClip pickupClip;
    public AudioClip despawnClip;

    private AudioSource audioPlayer;
    private ParticleSystem pickupAnimation;
    private SpriteRenderer sprite;
    private BoxCollider2D hitbox;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        pickupAnimation = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        hitbox = GetComponent<BoxCollider2D>();
        StartCoroutine(WaitForDespawn());
    }

    void OnTriggerEnter2D(Collider2D _collider) 
    {
        if(_collider.GetComponent<Gun>() != null)
            Take(_collider.GetComponent<Gun>());
    }

    void Take(Gun _gun)
    {
        _gun.PowerupTaken(powerupType, powerupDuration, customBullet, customRate);
        Despawn(pickupClip);
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
