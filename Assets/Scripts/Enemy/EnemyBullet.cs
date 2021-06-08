using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float autoExplodeTime;
    public GameObject explosionEffect;
    [SerializeField] private AudioClip hitClip;
    private Rigidbody2D body;

    public Color indestructibleSpriteColor;
    public Color destructibleSpriteColor;
    private SpriteRenderer spriteRenderer;

    private bool hasCollided = false;
    private float damage;
    private float speed;

    private Coroutine gc;

    public void Initialize(string _tag,  float _damage, float _speed)
    {
        Debug.Log(_tag);
        if(_tag.Equals("EnemyBullet"))
        {
            Debug.Log("Enemy Bullet Tag" + LayerMask.NameToLayer(_tag));
            spriteRenderer.color = destructibleSpriteColor;
            gameObject.layer = LayerMask.NameToLayer(_tag);
        }
        else if(_tag.Equals("IEnemyBullet"))
        {
            Debug.Log("Indestructible Enemy Bullet Tag" + LayerMask.NameToLayer(_tag));
            spriteRenderer.color = indestructibleSpriteColor;
            gameObject.layer = LayerMask.NameToLayer(_tag);
        }

        if(gc != null)
            StopCoroutine(gc);
        gc = StartCoroutine(GC());

        damage = _damage;
        speed = _speed;
        body.velocity = transform.up * speed;
        hasCollided = false;
    }

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.parent = null;
    }

    void OnTriggerEnter2D(Collider2D _object)
    {
        PlayerManager _player = _object.GetComponent<PlayerManager>();
        Melee _melee = _object.GetComponent<Melee>();

        if(!hasCollided) 
        {
            if(_player != null)
            {
                if(_player.canTakeDamage)
                {
                    Explode(true);
                    _player.TakeDamage(damage);
                }
            }
            if(_player == null)
                Explode(true);

            if(_melee != null)
                PlayerVariables.player.HealWithMultiplyer();
        }
    }

    private void Explode(bool _playAudio)
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        if(_playAudio)
            explosion.GetComponent<AudioSource>().PlayOneShot(hitClip);
        hasCollided = true;
        //spriteRenderer.enabled = false;
        gameObject.SetActive(false);
    }

    private IEnumerator GC()
    {
        yield return new WaitForSeconds(autoExplodeTime);
        Explode(false);
    }
}
