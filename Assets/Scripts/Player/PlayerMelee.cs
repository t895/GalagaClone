using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public PlayerManager player;
    public GameObject explosionEffect;
    public float damage;
    public float hitboxTime;
    public AudioClip meleeClip;
    private ParticleSystem particles;
    private CircleCollider2D hitbox;
    private AudioSource audioPlayer;

    private void Awake()
    {
        PlayerVariables.playerMelee = this;
    }

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        hitbox = GetComponent<CircleCollider2D>();
        audioPlayer = transform.parent.gameObject.GetComponent<AudioSource>();
    }

    public void Attack()
    {
        audioPlayer.PlayOneShot(meleeClip);
        player.meleePower -= player.meleePower;
        particles.Play();
        StartCoroutine(HitboxEnable());
    }

    private IEnumerator HitboxEnable()
    {
        hitbox.enabled = true;
        yield return new WaitForSeconds(hitboxTime);
        hitbox.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D _object)
    {
        GameObject explosion = Instantiate(explosionEffect, _object.transform.position, transform.rotation);
        Destroy(explosion, 1f);
        if(_object.GetComponent<Enemy>() != null)
        {
            if(PlayerVariables.playerMultiplyer > 0.5f)
                _object.GetComponent<Enemy>().TakeDamage(damage * PlayerVariables.playerMultiplyer);
            else
                _object.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
