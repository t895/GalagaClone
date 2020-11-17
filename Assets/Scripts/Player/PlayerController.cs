using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float dodgeSpeed;
    public float startDodgeTime = 1f;
    public float nextDodgeTime = 1f;
    public GameObject dodgeEffect;
    public ParticleSystem rechargeEffect;
    public CameraShake cameraShake;
    public Melee melee;
    public Camera mainCamera;
    public float shakeDuration;
    public float shakeMagnitude;

    private float dodgeTime = 0f;
    private float dodgeWaitTime = 0f;
    private Vector2 lastMove;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private PlayerManager player;
    private Vector2 moveInput;
    private bool charged = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerManager>();
    }

    void Update()
    {
        if(player.isAlive) 
        {
            if((moveInput.magnitude > 0 && Input.GetKeyDown(KeyCode.Space)) || Time.time < dodgeTime) 
            {
                if(!(Time.time > dodgeTime && Time.time < nextDodgeTime))
                {
                    Dodge(moveInput);
                    player.canTakeDamage = false;
                }
            }
            else
            {
                player.canTakeDamage = true;
            }

            if(Time.time > dodgeTime && melee.meleePower == 100f && Input.GetKeyDown(KeyCode.Mouse1)) 
            {
                StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
                melee.Attack();
            }

            if(Time.time > dodgeTime)
                Move();

            if(Time.time > dodgeTime && !charged)
            {
                rechargeEffect.Play();
                charged = true;
            }

        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Move()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;
        rb.velocity = SpeedRelativeToCamera(moveVelocity);
    }

    private void Dodge(Vector2 _moveInput)
    {
        if(Time.time > dodgeWaitTime) 
        {
            dodgeTime = Time.time + startDodgeTime;
            dodgeWaitTime = dodgeTime + nextDodgeTime;
            lastMove = _moveInput;
            GameObject effect = Instantiate(dodgeEffect, transform.position, transform.rotation);
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
            charged = false;
        }

        rb.velocity = SpeedRelativeToCamera(lastMove * dodgeSpeed);
    }

    private Vector2 SpeedRelativeToCamera(Vector2 _speed)
    {
        return _speed * mainCamera.orthographicSize * Time.fixedDeltaTime;
    }
}
