using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerBulletObject bullet;
    private int collisions;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(PlayerBulletObject _bullet)
    {
        bullet                          = _bullet;
        collisions                      = bullet.collisions;
        gameObject.transform.localScale = bullet.size;
        spriteRenderer.sprite           = bullet.sprite;
        spriteRenderer.color            = bullet.color;
        spriteRenderer.enabled          = true;
        body.velocity                   = transform.right * bullet.speed;

        //Crappy hardcoded values to ignore collision
        if(bullet.canDestroyIndestructableBullets)
            Physics2D.IgnoreLayerCollision(8, 12, false);
        else
            Physics2D.IgnoreLayerCollision(8, 12, true);

        StartCoroutine(GC());
    }

    private void OnTriggerEnter2D(Collider2D _object)
    {
        Enemy _enemy = _object.GetComponent<Enemy>();
        EnemyBullet _bullet = _object.GetComponent<EnemyBullet>();

        if(collisions > 0)
        {
            ObjectPooler.Instance
                .SpawnFromPool(PooledObject.BulletExplosion, gameObject.transform.position, gameObject.transform.rotation);
            if(_enemy != null)
                _enemy.TakeDamage(bullet.damage);

            if(_bullet != null)
                PlayerVariables.playerManager.HealWithMultiplyer();

            collisions--;
        }

        if(collisions == 0)
            Explode(false);
    }

    private IEnumerator GC()
    {
        yield return new WaitForSeconds(bullet.timeToDestroy);
        Explode(false);
    }

    private void Explode(bool _playAudio)
    {
        ObjectPooler.Instance
            .SpawnFromPool(PooledObject.BulletExplosion, gameObject.transform.position, gameObject.transform.rotation);
        spriteRenderer.enabled = false;
        body.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

}
