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
        PlayerManager _player = _object.GetComponent<PlayerManager>();

        if(_player != null)
        {
            if(_player.canTakeDamage && !hasCollided)
            {
                Explode();
                _player.TakeDamage(damage);
            }
        }

        if(_player == null && !hasCollided)
            Explode();
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(explosion, 2f);
        hasCollided = true;
        Destroy(gameObject);
    }
}
