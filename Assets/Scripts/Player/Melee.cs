﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public PlayerManager player;
    public GameObject explosionEffect;
    public float damage;
    public float hitboxTime;
    public float meleePower = 100f;
    public float meleeRechargeRate = 5f;
    public AudioClip meleeClip;
    private ParticleSystem particles;
    private CircleCollider2D hitbox;
    private AudioSource audioPlayer;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        hitbox = GetComponent<CircleCollider2D>();
        audioPlayer = transform.parent.gameObject.GetComponent<AudioSource>();
        meleeRechargeRate *= Time.deltaTime;
    }

    void Update()
    {
        if(!GameState.paused)
        {
            if(meleePower < 100f)
                meleePower += meleeRechargeRate;
            else if(meleePower > 100f)
                meleePower = 100f;
        }
    }

    public void Attack()
    {
        audioPlayer.PlayOneShot(meleeClip);
        meleePower -= meleePower;
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
            _object.GetComponent<Enemy>().TakeDamage(damage);
    }
}
