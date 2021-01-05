using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Wave[] waveArray = default;
    private int currentWave = 0;

    void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        if(waveArray.Length > currentWave && GameState.currentState == GameState.LevelStatus.levelInProgress)
        {
            waveArray[currentWave].Check();
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            if(enemies.Length < 1 && !(waveArray[currentWave].waitingToSpawn))
            {
                currentWave++;
                SpawnWave();
            }
        }

        if(waveArray.Length <= currentWave && GameState.currentState == GameState.LevelStatus.levelInProgress)
        {
            GameState.currentState = GameState.LevelStatus.levelComplete;
            Debug.Log("Level is complete");
        }
    }

    void SpawnWave()
    {
        if(waveArray.Length - 1 >= currentWave)
            waveArray[currentWave].Initialize();
    }

    [System.Serializable]
    private class Wave
    {
        public PickupSpawner pickup;
        public List<EnemySpawner> enemies = default;
        public float timer = 0;
        public bool waitingToSpawn = true;
        private float timeToSpawn;
        
        public void Initialize()
        {
            if(pickup != null)
                pickup.Spawn();
            timeToSpawn = Time.time + timer;
        }

        public void Check()
        {
            if(Time.time > timeToSpawn && waitingToSpawn)
            {
                SpawnEnemies();
                waitingToSpawn = false;
            }
        }

        public void SpawnEnemies()
        {
            foreach(EnemySpawner enemy in enemies)
                enemy.Spawn();
        }
    }
}
