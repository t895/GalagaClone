using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class BulletObject : ScriptableObject
{
    public new string name;

    public Sprite sprite;
    public Color color;
    public AudioClip audio;

    public float speed;
    public float damage;
    public float timeToDestroy;
    public float fireRate;
    public int collisions;
    public int powerupDuration;

    public Vector3 size;
    
    public bool canDestroyIndestructableBullets;

    public PooledObject pooledObject;
    public PowerupType powerupType;
    public Sprite icon;
    
}
