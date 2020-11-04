using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput input;

    public float speed;
    public float dodgeSpeed;
    public float startDodgeTime = 1f;
    public float nextDodgeTime = 1f;
    public GameObject dodgeEffect;
    public CameraShake cameraShake;
    public float shakeDuration;
    public float shakeMagnitude;
    public float shootInput;

    private float dodgeTime = 0f;
    private float dodgeWaitTime = 0f;
    private Vector2 lastMove;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private PlayerManager player;
    private Weapon weapon;
    private CircleCollider2D hitbox;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerManager>();
        weapon = GetComponent<Weapon>();
        hitbox = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        shootInput = input.controls.Gameplay.Shoot.ReadValue<float>();

        Move();
        
        //Move();

        /*if(player.isAlive) 
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

            if(Time.time > dodgeTime)
                Move();
        }
        else
            rb.velocity = Vector2.zero;*/
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(input.moveDireciton.x, input.moveDireciton.y);
        moveVelocity = moveInput * speed;
        rb.velocity = moveVelocity * Time.deltaTime;
    }

    private void Dodge(Vector2 _moveInput)
    {
        if(Time.time > dodgeWaitTime) 
        {
            dodgeTime = Time.time + startDodgeTime;
            dodgeWaitTime = dodgeTime + nextDodgeTime;
            lastMove = _moveInput;
            GameObject effect = Instantiate(dodgeEffect, transform.position, transform.rotation);
            Destroy(effect, 2f);
            StartCoroutine(cameraShake.Shake(shakeDuration, shakeMagnitude));
        }

        rb.velocity = (lastMove * dodgeSpeed) * Time.deltaTime;
    }
}
