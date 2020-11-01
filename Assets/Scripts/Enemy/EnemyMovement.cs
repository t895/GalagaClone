using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum movementTypes { target, follow };

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public GameObject target;
    public movementTypes movement;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.up = Vector2.Lerp(transform.up, (player.transform.position - transform.position), turnSpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
