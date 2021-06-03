﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private int collisions = 1;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private bool canDestroyIndestructableBullets = false;
    [SerializeField] private AudioClip hitClip;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        transform.parent = null;
        body.velocity = transform.right * speed;
        StartCoroutine(GC());
        if(canDestroyIndestructableBullets)
            Physics2D.IgnoreLayerCollision(8, 12, false);
        else
            Physics2D.IgnoreLayerCollision(8, 12, true);

    }

    void OnTriggerEnter2D(Collider2D _object)
    {
        Enemy _enemy = _object.GetComponent<Enemy>();
        EnemyBullet _bullet = _object.GetComponent<EnemyBullet>();

        if(collisions > 0)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            if(_enemy != null)
                _enemy.TakeDamage(damage);

            if(_bullet != null)
                PlayerVariables.player.HealWithMultiplyer();

            collisions--;
        }

        if(collisions == 0)
            Explode(false);
    }

    private void Explode(bool _playAudio)
    {
        sprite.enabled = false;
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        if(_playAudio)
            explosion.GetComponent<AudioSource>().PlayOneShot(hitClip);
        Destroy(gameObject);
    }

    private IEnumerator GC()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Explode(false);
    }
}
