using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarrierSpawner : MonoBehaviour
{
    public GameObject barrier;
    public List<Transform> positions;
    public Vector3 size = new Vector3(9f, 90f, 0.2f);

    public void Spawn()
    {
        GameObject _barrier = Instantiate(barrier, transform.position, transform.rotation);
        _barrier.GetComponentInChildren<EnergyBarrier>().Initialize(positions, size);
    }

}
