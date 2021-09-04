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
    public Camera mainCamera;
    public float shakeDuration;
    public float shakeMagnitude;
    public AudioClip dodgeClip;

    private float dodgeTime = 0f;
    private float dodgeWaitTime = 0f;
    private Vector2 lastMove;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    private bool charged = false;
    private AudioSource audioPlayer;

    private PlayerControls playerControls;
    public Vector2 movementInput, lookInput;

    #region InitializeControls
    private void Awake()
    {
        playerControls = new PlayerControls();
        PlayerVariables.playerControls = playerControls;
        PlayerVariables.playerController = this; 
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    #endregion

    private void Start()
    {
        playerControls.InGame.Dodge.performed += cxt => AttemptDodge();
        playerControls.InGame.Melee.performed += cxt => AttemptMelee();
        playerControls.InGame.Move.performed += cxt => movementInput = cxt.ReadValue<Vector2>();
        playerControls.InGame.Look.performed += cxt => lookInput = cxt.ReadValue<Vector2>();
        rb = GetComponent<Rigidbody2D>();
        audioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(PlayerVariables.playerManager.isAlive && !GameState.paused) 
        {
            //Movement
            if(Time.time > dodgeTime)
                Move(movementInput);

            //Dodge FX and mechanics
            if(Time.time > dodgeTime && !charged)
            {
                PlayerVariables.playerManager.canTakeDamage = true;
                ObjectPooler.Instance
                    .SpawnFromPool(PooledObject.EnemyTelegraph, transform.position, transform.rotation);
                charged = true;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Move(Vector2 _movementInput)
    {
        moveVelocity = _movementInput * speed;
        rb.velocity = SpeedRelativeToCamera(moveVelocity);
    }

    private void AttemptMelee()
    {
        if(Time.time > dodgeTime && PlayerVariables.playerManager.meleePower == 100f) 
        {
            StartCoroutine(PlayerVariables.cameraShake.Shake(shakeDuration, shakeMagnitude));
            PlayerVariables.playerMelee.Attack();
        }
    }

    private void AttemptDodge()
    {
        if(PlayerVariables.playerManager.isAlive && !GameState.paused)
        {
            if(movementInput.magnitude > 0 && Time.time > dodgeWaitTime)
            {
                //Time increment/movement tracking
                dodgeTime = Time.time + startDodgeTime;
                dodgeWaitTime = dodgeTime + nextDodgeTime;
                lastMove = movementInput;
                charged = false;

                //FX
                audioPlayer.PlayOneShot(dodgeClip);
                GameObject effect = Instantiate(dodgeEffect, transform.position, transform.rotation);
                StartCoroutine(PlayerVariables.cameraShake.Shake(shakeDuration, shakeMagnitude));

                PlayerVariables.playerManager.canTakeDamage = false;
                rb.velocity = SpeedRelativeToCamera(lastMove * dodgeSpeed);
            }
        }
    }

    private Vector2 SpeedRelativeToCamera(Vector2 _speed)
    {
        return _speed * mainCamera.orthographicSize * Time.fixedDeltaTime;
    }
}
