using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float timeToNextShot;
    public float damage;
    public float speed;
    public AudioClip shotSound;
    public bool telegraphShot;
    
    public List<EnemyBulletObject> bulletTypes;
    public List<Transform> shotSources;

    private AudioSource audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        StartCoroutine(ShootLoop());
    }

    private IEnumerator ShootLoop()
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

            yield return new WaitForSeconds(timeToNextShot / 4);
            if(telegraphShot)
                ObjectPooler.Instance
                    .SpawnFromPool(PooledObject.EnemyTelegraph, transform.position, transform.rotation);
            yield return new WaitForSeconds((timeToNextShot / 4) * 3);
        }
    }

    private void Shoot(int _typeIndex, int _positionIndex, float _damage, float _speed)
    {
        audioPlayer.PlayOneShot(shotSound);
        ObjectPooler.Instance
            .SpawnFromPool(PooledObject.EnemyBullet, shotSources[_positionIndex].position, shotSources[_positionIndex].rotation)
            .GetComponent<EnemyBullet>().Initialize(bulletTypes[_typeIndex], _damage, _speed);
    }
}
