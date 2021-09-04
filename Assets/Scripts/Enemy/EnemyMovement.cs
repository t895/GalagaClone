using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum movementType { singleTarget, multipleTargets, multipleTargetLoop, follow };

public enum turnType { look, spin };

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float lookSpeed;
    public float spinSpeed;
    public movementType movement;
    public turnType turn;
    [SerializeField] private List<Transform> targets;
    [SerializeField] private int currentTarget = 0;
    private bool isReady = false;

    void Update()
    {
        if(isReady)
        {    
            if(movement == movementType.singleTarget) 
                SingleTarget();
            else if(movement == movementType.multipleTargets)
                MultipleTargets();
            else if(movement == movementType.multipleTargetLoop)
                MultipleTargetLoop();
            else if(movement == movementType.follow)
                Follow();
            
            if(turn == turnType.look)
                Look();
            else if(turn == turnType.spin)
                Spin();
        }
    }

    public void SetTargets(List<Transform> _positions)
    {
        targets = _positions;
        isReady = true;
    }

    void Look()
    {
        transform.up = Vector2.Lerp(transform.up, (PlayerVariables.playerManager.gameObject.transform.position - transform.position), lookSpeed * Time.deltaTime);
    }

    void Spin()
    {
        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }

    void SingleTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targets[0].position, speed * Time.deltaTime);
    }

    void MultipleTargets()
    {
        if(currentTarget >= targets.Count)
            return;
        
        if(transform.position != targets[currentTarget].position)
            transform.position = Vector2.MoveTowards(transform.position, targets[currentTarget].position, speed * Time.deltaTime);
        else
            currentTarget++;
    }

    void MultipleTargetLoop()
    {
        MultipleTargets();
        if(currentTarget >= targets.Count)
            currentTarget = 0;
    }

    void Follow()
    {
        Vector2 followForce = Vector2.zero;

        Vector2 direction = (transform.position - PlayerVariables.playerManager.gameObject.transform.position).normalized;
        float distance = Vector2.Distance(transform.position, PlayerVariables.playerManager.gameObject.transform.position);
        
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
            transform.position = Vector2.MoveTowards(transform.position, PlayerVariables.playerManager.gameObject.transform.position, followForce.magnitude * Time.deltaTime);
    }
}
