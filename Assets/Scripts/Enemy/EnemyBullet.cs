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
    
    private ParticleSystem particles;
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
        particles = GetComponent<ParticleSystem>();
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
            {
                Explode(true);
                _player.TakeDamage(damage);
            }
        }
        else
        {
            Explode(true);
        }

        if(_melee != null)
            PlayerVariables.player.HealWithMultiplyer();
    }

    private IEnumerator GC()
    {
        yield return new WaitForSeconds(autoExplodeTime);
        Explode(false);
    }

    private void Explode(bool _playAudio)
    {
        if(_playAudio)
            audioSource.PlayOneShot(hitClip);
        hitbox.enabled = false;
        spriteRenderer.enabled = false;
        body.velocity = Vector2.zero;
        particles.Play();
    }
}
