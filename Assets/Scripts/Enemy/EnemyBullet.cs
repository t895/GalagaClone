using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private EnemyBulletObject bullet;
    private float damage;
    private float speed;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private Collider2D hitbox;

    private Coroutine gc;

    private void Awake()
    {
        body                = GetComponent<Rigidbody2D>();
        spriteRenderer      = GetComponent<SpriteRenderer>();
        hitbox              = GetComponent<Collider2D>();
        transform.parent    = null;
    }

    public void Initialize(EnemyBulletObject _bullet,  float _damage, float _speed)
    {
        bullet = _bullet;

        spriteRenderer.sprite   = bullet.sprite;
        spriteRenderer.color    = bullet.color;
        damage                  = _damage;
        speed                   = _speed;
        body.velocity           = transform.up * speed;
        spriteRenderer.enabled  = true;
        gameObject.layer        = LayerMask.NameToLayer(bullet.bulletType.ToString());
        hitbox.enabled          = true;

        if(gc != null)
            StopCoroutine(gc);
        gc = StartCoroutine(GC());
    }

    private void OnTriggerEnter2D(Collider2D _object)
    {
        PlayerManager _player = _object.GetComponent<PlayerManager>();
        PlayerMelee _melee = _object.GetComponent<PlayerMelee>();

        if(_player != null && _player.canTakeDamage)
            _player.TakeDamage(damage);

        if(_melee != null)
            PlayerVariables.playerManager.HealWithMultiplyer();

        Explode();
    }

    private IEnumerator GC()
    {
        yield return new WaitForSeconds(bullet.timeToDestroy);
        Explode();
    }

    private void Explode()
    {
        ObjectPooler.Instance
            .SpawnFromPool(PooledObject.BulletExplosion, transform.position, transform.rotation);
        hitbox.enabled = false;
        spriteRenderer.enabled = false;
        body.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
