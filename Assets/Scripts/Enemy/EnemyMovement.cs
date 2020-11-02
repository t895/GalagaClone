using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum movementType { target, follow };

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public GameObject target;
    private GameObject player;
    public movementType movement;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(movement == movementType.target) 
            Target();
        else if (movement == movementType.follow)
            Follow();

        Look();
    }

    void Look()
    {
        transform.up = Vector2.Lerp(transform.up, (player.transform.position - transform.position), turnSpeed * Time.deltaTime);
    }

    void Target()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    void Follow()
    {
        Vector2 followForce = Vector2.zero;

        Vector2 direction = (transform.position - player.transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        
        float targetDistance = 1f;
        float springStrength = (distance - targetDistance);

        followForce += direction * springStrength;

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach(Enemy enemy in enemies) 
        {
            direction = (transform.position - enemy.transform.position).normalized;
            distance = Vector2.Distance(transform.position, enemy.transform.position);
            
            springStrength = 1f / (1f + distance * distance * distance);

            followForce -= direction * springStrength;
        }

        if(followForce.magnitude > 0.5f)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, followForce.magnitude * Time.deltaTime);
    }
}
