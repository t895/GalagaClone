using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public List<Transform> positions;

    public void Spawn()
    {
        GameObject _enemy = Instantiate(enemy, transform.position, transform.rotation);
        _enemy.GetComponentInChildren<EnemyMovement>().SetTargets(positions);
    }
}
