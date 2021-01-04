using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 10f;
    public GameObject explosionEffect;
    
    [SerializeField] private AudioClip shotSound;
    private Rigidbody2D body;
    private bool hasCollided = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform.parent = null;
        body.velocity = transform.right * speed;
        Destroy(gameObject, 2f);
    }

    public void Initialize(Gun _gun)
    {
        if(shotSound != null)
            _gun.audioPlayer.PlayOneShot(shotSound);
    }

    void OnTriggerEnter2D(Collider2D _object)
    {
        if(!hasCollided)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
            if(_object.GetComponent<Enemy>() != null)
                _object.GetComponent<Enemy>().TakeDamage(damage);
            hasCollided = true;
            Destroy(gameObject);
        }
    }
}
