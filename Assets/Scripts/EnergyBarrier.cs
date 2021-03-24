using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarrierType { spin, shift, shiftOnce };

public class EnergyBarrier : MonoBehaviour
{
    [SerializeField] private BarrierType barrier;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<Transform> targets;
    private int currentTarget;

    [SerializeField] private float damage;

    void Start()
    {
        damage *= Time.deltaTime;
    }

    void Update()
    {
        if(barrier == BarrierType.spin)
            Spin();
        else if(barrier == BarrierType.shift)
            Shift();
        else if(barrier == BarrierType.shiftOnce)
            ShiftOnce();
    }

    void OnTriggerStay2D(Collider2D _object)
    {
        PlayerManager _player = _object.GetComponent<PlayerManager>();

        if(_player != null)
        {
            if(_player.canTakeDamage)
                _player.TakeDamage(damage);
        }
    }

    private void Spin()
    {
        transform.Rotate(Vector3.forward * turnSpeed * Time.deltaTime);
    }

    void ShiftOnce()
    {
        if(currentTarget >= targets.Count)
            return;
        
        if(transform.position != targets[currentTarget].position)
            transform.position = Vector2.MoveTowards(transform.position, targets[currentTarget].position, moveSpeed * Time.deltaTime);
        else
            currentTarget++;
    }

    void Shift()
    {
        ShiftOnce();
        if(currentTarget >= targets.Count)
            currentTarget = 0;
    }

}
