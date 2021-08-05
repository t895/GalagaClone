using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType { none, bigBullets, tripleShot, octaShot };

public class WeaponPowerup : MonoBehaviour
{
    [SerializeField] private BulletObject customBullet;
    [SerializeField] private float timeToDespawn;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip despawnClip;

    private AudioSource audioPlayer;
    private ParticleSystem pickupAnimation;
    private SpriteRenderer sprite;
    private BoxCollider2D hitbox;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        pickupAnimation = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        hitbox = GetComponent<BoxCollider2D>();
        StartCoroutine(WaitForDespawn());
    }

    private void OnTriggerEnter2D(Collider2D _collider) 
    {
        if(_collider.GetComponent<PlayerGun>() != null)
            Take(_collider.GetComponent<PlayerGun>());
    }

    private void Take(PlayerGun _gun)
    {
        _gun.PowerupTaken(customBullet);
        Despawn(pickupClip);
    }

    private void Despawn(AudioClip _clip)
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
