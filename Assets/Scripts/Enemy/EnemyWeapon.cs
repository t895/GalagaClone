﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float timeToNextShot;
    public float damage;
    public float speed;
    public List<GameObject> bulletTypes;
    public List<Transform> shotSources;

    void Start()
    {
        StartCoroutine(ShootLoop());
    }

    IEnumerator ShootLoop()
    {
        int _typeIndex = 0;
        int _typePosition = 0;
        while(true)
        {
            Shoot(_typeIndex, _typePosition, damage, speed);

            _typeIndex++;
            _typePosition++;

            if(_typeIndex >= bulletTypes.Count)
                _typeIndex = 0;
            if(_typePosition >= shotSources.Count)
                _typePosition = 0;

            yield return new WaitForSeconds(timeToNextShot);
        }
    }

    private void Shoot(int _typeIndex, int _positionIndex, float _damage, float _speed)
    {
        GameObject bullet = Instantiate(bulletTypes[_typeIndex], shotSources[_positionIndex]);
        bullet.GetComponent<EnemyBullet>().Initialize(_damage, _speed);
    }
}