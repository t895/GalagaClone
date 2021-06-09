using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private int collisions = 1;
    [SerializeField] private float timeToDestroy;
    [SerializeField] private bool canDestroyIndestructableBullets = false;

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
            ObjectPooler.Instance
                .SpawnFromPool(PooledObject.BulletExplosion, gameObject.transform.position, gameObject.transform.rotation);
            if(_enemy != null)
                _enemy.TakeDamage(damage);

            if(_bullet != null)
                PlayerVariables.player.HealWithMultiplyer();

            collisions--;
        }

        if(collisions == 0)
            Explode(false);
    }

    private IEnumerator GC()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Explode(false);
    }

    private void Explode(bool _playAudio)
    {
        sprite.enabled = false;
        body.velocity = Vector2.zero;
        ObjectPooler.Instance
            .SpawnFromPool(PooledObject.BulletExplosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
