﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 10f;
    public GameObject explosionEffect;
    [SerializeField] private int collisions = 1;
    [SerializeField] private bool canDestroyIndestructableBullets = false;
    
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform.parent = null;
        body.velocity = transform.right * speed;
        Destroy(gameObject, 2f);
        if(canDestroyIndestructableBullets)
            Physics2D.IgnoreLayerCollision(8, 12, false);
    }

    void OnTriggerEnter2D(Collider2D _object)
    {
        if(collisions > 0)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            if(_object.GetComponent<Enemy>() != null)
                _object.GetComponent<Enemy>().TakeDamage(damage);
            collisions--;
        }

        if(collisions == 0)
            Destroy(gameObject);
    }
}
