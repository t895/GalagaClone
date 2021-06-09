using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float autoExplodeTime;
    [SerializeField] private AudioClip hitClip;
    private Rigidbody2D body;

    public Color indestructibleSpriteColor;
    public Color destructibleSpriteColor;
    private SpriteRenderer spriteRenderer;
    
    private AudioSource audioSource;
    private Collider2D hitbox;

    private float damage;
    private float speed;

    private Coroutine gc;
    private Coroutine finishParticles;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        hitbox = GetComponent<Collider2D>();
        transform.parent = null;
    }

    public void Initialize(string _tag,  float _damage, float _speed)
    {
        if(_tag.Equals("EnemyBullet"))
        {
            spriteRenderer.color = destructibleSpriteColor;
            gameObject.layer = LayerMask.NameToLayer(_tag);
        }
        else if(_tag.Equals("IEnemyBullet"))
        {
            spriteRenderer.color = indestructibleSpriteColor;
            gameObject.layer = LayerMask.NameToLayer(_tag);
        }

        if(gc != null)
            StopCoroutine(gc);
        gc = StartCoroutine(GC());

        damage = _damage;
        speed = _speed;
        body.velocity = transform.up * speed;
        spriteRenderer.enabled = true;
        hitbox.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D _object)
    {
        PlayerManager _player = _object.GetComponent<PlayerManager>();
        Melee _melee = _object.GetComponent<Melee>();

        if(_player != null)
        {
            if(_player.canTakeDamage)
                _player.TakeDamage(damage);
        }
        Explode();

        if(_melee != null)
            PlayerVariables.player.HealWithMultiplyer();
    }

    private IEnumerator GC()
    {
        yield return new WaitForSeconds(autoExplodeTime);
        Explode();
    }

    private void Explode()
    {
        ObjectPooler.Instance
            .SpawnFromPool(PooledObject.BulletExplosion, gameObject.transform.position, gameObject.transform.rotation);
        hitbox.enabled = false;
        spriteRenderer.enabled = false;
        body.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
