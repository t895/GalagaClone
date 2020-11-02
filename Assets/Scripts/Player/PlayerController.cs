using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float dodgeSpeed;
    public float startDodgeTime = 1f;
    public float nextDodgeTime = 1f;
    private float dodgeTime = 0f;
    private float dodgeWaitTime = 0f;
    private Vector2 lastMove;

    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private PlayerManager player;
    private CircleCollider2D hitbox;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerManager>();
        hitbox = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;
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
        }
    }

    private void Move()
    {
        //rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
        rb.velocity = moveVelocity * Time.deltaTime;
    }

    private void Dodge(Vector2 _moveInput)
    {
        if(Time.time > dodgeWaitTime) 
        {
            dodgeTime = Time.time + startDodgeTime;
            dodgeWaitTime = dodgeTime + nextDodgeTime;
            lastMove = _moveInput;
        }

        //rb.MovePosition(rb.position + (lastMove * dodgeSpeed) * Time.deltaTime);
        rb.velocity = (lastMove * dodgeSpeed) * Time.deltaTime;
    }
}
