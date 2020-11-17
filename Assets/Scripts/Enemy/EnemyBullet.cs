using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float autoExplodeTime;
    public GameObject explosionEffect;
    private Rigidbody2D body;
    private bool hasCollided = false;
    private float damage;
    private float speed;

    public void Initialize(float _damage, float _speed)
    {
        damage = _damage;
        speed = _speed;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform.parent = null;
        body.velocity = transform.up * speed;
        StartCoroutine(GC());
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
        hasCollided = true;
        Destroy(gameObject);
    }

    private IEnumerator GC()
    {
        yield return new WaitForSeconds(autoExplodeTime);
        Explode();
    }
}
