using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { DestructibleEnemyBullet, IndestructibleEnemyBullet }

[CreateAssetMenu(fileName = "New Bullet", menuName = "Enemy Bullet")]
public class EnemyBulletObject : ScriptableObject
{
    public new string name;

    public Sprite sprite;
    public Color color;
    public AudioClip audio;
    public PooledObject pooledObject;
    public BulletType bulletType;

    public float timeToDestroy;
}
