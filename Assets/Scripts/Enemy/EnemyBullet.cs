using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float autoExplodeTime;
    public GameObject explosionEffect;
    [SerializeField] private AudioClip hitClip;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
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
        sprite = GetComponent<SpriteRenderer>();
        transform.parent = null;
        body.velocity = transform.up * speed;
        StartCoroutine(GC());
    }

    void OnTriggerEnter2D(Collider2D _object)
    {
        PlayerManager _player = _object.GetComponent<PlayerManager>();
        Melee _melee = _object.GetComponent<Melee>();

        if(_player != null)
        {
            if(_player.canTakeDamage && !hasCollided)
            {
                Explode(true);
                _player.TakeDamage(damage);
            }
        }

        if(_melee != null)
            PlayerVariables.player.HealWithMultiplyer();

        if(_player == null && !hasCollided)
            Explode(true);
    }

    private void Explode(bool _playAudio)
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        if(_playAudio)
            explosion.GetComponent<AudioSource>().PlayOneShot(hitClip);
        hasCollided = true;
        sprite.enabled = false;
        Destroy(gameObject);
    }

    private IEnumerator GC()
    {
        yield return new WaitForSeconds(autoExplodeTime);
        Explode(false);
    }
}
