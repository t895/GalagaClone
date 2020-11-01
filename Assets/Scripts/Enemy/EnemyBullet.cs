using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 2f;
    public float damage = 10f;
    public GameObject explosionEffect;
    private Rigidbody2D body;
    private bool hasCollided = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform.parent = null;
        body.velocity = transform.up * speed;
        Destroy(gameObject, 15f);
    }

    void OnTriggerEnter2D(Collider2D _object)
    {
        if(!hasCollided)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(explosion, 2f);
            PlayerManager _player = _object.GetComponent<PlayerManager>();
            if(_player != null)
                _player.TakeDamage(damage);
            //Debug.Log(_object.name);
            hasCollided = true;
            Destroy(gameObject);
        }
    }
}
