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
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerManager>();
        weapon = GetComponent<Weapon>();
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

            if(Time.time > dodgeTime)
                Move();

            if(Time.time > dodgeTime && Input.GetKeyDown(KeyCode.Mouse1))
                weapon.Melee();
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
