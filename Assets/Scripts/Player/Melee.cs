using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public PlayerManager player;
    public GameObject explosionEffect;
    public float damage;
    public float meleeRate = 0f;
    public float hitboxTime;
    public float nextMelee = 0f;
    private ParticleSystem particles;
    private CircleCollider2D hitbox;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        hitbox = GetComponent<CircleCollider2D>();
    }

    public void Attack()
    {
        if(player.isAlive && Time.time > nextMelee)
        {
            nextMelee = Time.time + meleeRate;
            particles.Play();
            StartCoroutine(HitboxEnable());
        }
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
