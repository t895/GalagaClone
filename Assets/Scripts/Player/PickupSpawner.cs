using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickup;

    public void Spawn()
    {
        GameObject _pickup = Instantiate(pickup, transform.position, transform.rotation);
    }
}
