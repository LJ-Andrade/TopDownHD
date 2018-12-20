using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;
    
    int enemiesRemaingingToSpawn;
    int enemiesRemaingingToAlive;
    float nextSpawnTime;
    Wave currentWave;
    int currentWaveNumber;

    void Start()
    {
        NextWave();   
    }

    void Update()
    {
        if(enemiesRemaingingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemaingingToSpawn --;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            
            // This trigger an event on LivingEntitie
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemaingingToAlive --;

        if(enemiesRemaingingToAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber ++;
        print("Wave: "  + currentWaveNumber);
        if(currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];
            enemiesRemaingingToSpawn = currentWave.enemyCount;
            enemiesRemaingingToAlive = enemiesRemaingingToSpawn;
        }
        else
        {
            print("Waves ended");
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
