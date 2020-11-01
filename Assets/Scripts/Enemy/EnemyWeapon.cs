using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform shotPosition;
    public GameObject enemyBullet;
    public List<GameObject> bulletTypes;

    void Start()
    {
        StartCoroutine(ShootLoop());
    }

    IEnumerator ShootLoop()
    {
        for(int i = 0; i < bulletTypes.Count; i++) 
        {
            Shoot(i);
            yield return new WaitForSeconds(1f);
            if(i >= bulletTypes.Count - 1)
                i = -1;
        }
    }

    private void Shoot(int _index)
    {
        GameObject bullet = Instantiate(bulletTypes[_index], shotPosition);
    }
}
