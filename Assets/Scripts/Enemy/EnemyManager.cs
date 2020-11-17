﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public bool levelIsComplete = false;
    [SerializeField] private Wave[] waveArray = default;
    private int currentWave = 0;

    void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        if(waveArray.Length > currentWave && !levelIsComplete)
        {
            waveArray[currentWave].Check();
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            if(enemies.Length < 1 && !(waveArray[currentWave].waitingToSpawn))
            {
                currentWave++;
                SpawnWave();
            }
        }

        if(waveArray.Length <= currentWave && !levelIsComplete)
            levelIsComplete = true;
    }

    void SpawnWave()
    {
        if(waveArray.Length - 1 >= currentWave)
            waveArray[currentWave].Initialize();
    }

    [System.Serializable]
    private class Wave
    {
        public List<EnemySpawner> enemies = default;
        public float timer = 0;
        public bool waitingToSpawn = true;
        private float timeToSpawn;
        
        public void Initialize()
        {
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
