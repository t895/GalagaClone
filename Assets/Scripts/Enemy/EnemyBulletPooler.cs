using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
In all honesty, adding this pooling system is overengineering
especially for such a low memory game like this. Thought 
it would be cool though and help out when the game still
somehow decides to dip below 144 fps. My biggest flaw here was
naively not checking the profiler before diving into this.
Hopefully this doesn't cause any serious issues in the future...
**/

public class EnemyBulletPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static EnemyBulletPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

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

    public GameObject SpawnFromPool(string _tag, Vector3 _position, Quaternion _rotation)
    {
        if(!poolDictionary.ContainsKey(_tag))
        {
            Debug.LogWarning("Pool with tag " + _tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[_tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = _position;
        objectToSpawn.transform.rotation = _rotation;

        poolDictionary[_tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
