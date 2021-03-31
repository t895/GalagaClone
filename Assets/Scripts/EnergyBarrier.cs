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

    [SerializeField] private float animationTransitionTime;

    void Start()
    {
        damage *= Time.deltaTime;
        StartCoroutine(AnimateIn());
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

    private IEnumerator AnimateIn()
    {
        Vector3 initialTransform = new Vector3(0.01f, 0.01f, 0.01f);
        Vector3 finalTransform = transform.localScale;

        float currentTime = 0.0f;
        
        do
        {
            //Create lerp information
            currentTime += Time.deltaTime;
            float t = currentTime / animationTransitionTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            //Lerp!
            gameObject.transform.localScale = Vector3.Lerp(initialTransform, finalTransform, t);
            Debug.Log(currentTime / animationTransitionTime);
            yield return null;
        }
        while(currentTime <= animationTransitionTime);
    }

}
