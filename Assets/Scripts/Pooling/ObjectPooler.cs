using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
In all honesty, adding this pooling system is overengineering
especially for such a low memory game like this. Thought 
it would be cool though and help out when the game still
somehow decides to dip below 144 fps. My biggest flaw here was
not checking the profiler before diving into this (like an idiot).
Hopefully this doesn't cause any serious issues in the future...
**/

public enum PooledObject 
{ 
    Bullet, 
    BulletExplosion, 
    EnemyDeathExplosion
}

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public PooledObject tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<PooledObject, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<PooledObject, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(PooledObject _tag, Vector3 _position, Quaternion _rotation)
    {
        if(!poolDictionary.ContainsKey(_tag))
        {
            Debug.LogWarning("Pool with tag " + _tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = null;
        if(!poolDictionary[_tag].Peek().activeSelf)
        {
            objectToSpawn = poolDictionary[_tag].Dequeue();

            SetObjectPosition(objectToSpawn, _position, _rotation);

            poolDictionary[_tag].Enqueue(objectToSpawn);
        }
        else
        {
            Debug.Log("Adding instance to the " + _tag + " pool");
            foreach(Pool pool in pools)
            {
                if(pool.tag.Equals(_tag))
                {
                    objectToSpawn = Instantiate(pool.prefab);

                    SetObjectPosition(objectToSpawn, _position, _rotation);

                    poolDictionary[_tag].Enqueue(objectToSpawn);
                    break;
                }
            }
        }

        if(objectToSpawn == null)
            Debug.LogWarning("Object spawned from pool is null");

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if(pooledObj != null)
            pooledObj.OnObjectSpawn();

        return objectToSpawn;
    }

    private void SetObjectPosition(GameObject _objectToSpawn, Vector3 _position, Quaternion _rotation)
    {
        _objectToSpawn.SetActive(true);
        _objectToSpawn.transform.position = _position;
        _objectToSpawn.transform.rotation = _rotation;
    }
}
