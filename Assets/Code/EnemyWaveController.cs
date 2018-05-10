using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls the movement and shooting of the entire enemy wave
/// </summary>
public class EnemyWaveController : MonoBehaviour
{
    private Transform enemyWaveTransform;

    [Header("Movement Parameters")]
    [SerializeField]
    [Tooltip("The distance enemies move each movement cycle")]
    private float enemyWaveDistance;
    [SerializeField]
    [Tooltip("How often a movement cycle is invoked")]
    private float enemyWaveSpeed = 0.3f;

    [Header("Missile Parameters")]
    [SerializeField]
    [Tooltip("The missile gameObject we want to spawn")]
    private GameObject enemyMissile;
    [SerializeField]
    [Tooltip("The rate at which the enemy can shoot missiles")]
    private float missileRate = 0.99f;

    [Header("Spawn Parameters")]
    [SerializeField]
    [Tooltip("The spawn points in the game, exposed in inspector for convenience")] // TODO: Find spawn points via code
    private GameObject[] spawnPoints;
    [SerializeField]
    private GameObject enemyToSpawn;
    [SerializeField]
    [Tooltip("The physical distance between waves")]
    private float distanceBetweenWaves = 1.5f;
    private int enemiesToSpawn;
    private int wavesToSpawn;

    void Start ()
    {
        InvokeRepeating("MoveEnemyWave", 0.1f, enemyWaveSpeed);
        enemyWaveTransform = GetComponent<Transform>();

        LimitEnemiesAndWavesToSpawn();
        SpawnWaves();
        if(enemiesToSpawn > 0 && wavesToSpawn > 0)
        {
            GameController.instance.UpdateScoreToWin(enemiesToSpawn, wavesToSpawn);
        }
    }
    
    /// <summary>
    /// Method that is invoked to repeat, which moves enemy wave a certain distance
    /// each time it is invoked, in typical Space Invaders-fashion.
    /// </summary>
    private void MoveEnemyWave()
    {
        enemyWaveTransform.position += Vector3.right * enemyWaveDistance;

        foreach (Transform enemy in enemyWaveTransform)
        {
            // Move downward and reverse movement when wave hits side of screen
            if (enemy.position.x < -7.5f || enemy.position.x > 7.5f) // TODO: get rid of magic numbers
            {
                enemyWaveDistance = -enemyWaveDistance;
                enemyWaveTransform.position += Vector3.down * 0.5f;
                return;
            }

            // Randomly decide if this enemy will fire missile
            if (Random.value > missileRate)
            {
                GameObject spawnedEnemyMissile = Instantiate(enemyMissile, enemy.position, enemy.rotation);
                spawnedEnemyMissile.tag = tag;
            }
        }

        // Refactor out to own method, allow to have varying levels of difficulty
		// Makes last enemy move fast, making it harder to kill and win the game
        if(enemyWaveTransform.childCount == 1)
        {
            CancelInvoke();
            InvokeRepeating("MoveEnemyWave", 0.1f, 0.25f);
        }
    }

    /// <summary>
    /// Method that allows EnemyWaveController to obtain
    /// JSON data from the LoadJSONData class
    /// </summary>
    public void GetJSONData(int enemies, int waves)
    {
        enemiesToSpawn = enemies;
        wavesToSpawn = waves;
    }
    
    /// <summary>
    /// Method that limits the amount of enemies and waves to spawn,
    /// see in method for specific reasons.
    /// </summary>
    private void LimitEnemiesAndWavesToSpawn()
    {
        // Limit enemies able to spawn to 5, due to screen space
        if (enemiesToSpawn > 5)
        {
            enemiesToSpawn = 5;
        }
        
        // Limit waves able to spawn to 10, due to performance
        if (wavesToSpawn > 10)
        {
            wavesToSpawn = 10;
        }
    }

    /// <summary>
    /// Method that spawns the amount of waves and enemies per wave specified
    /// in the JSON file
    /// </summary>
    private void SpawnWaves()
    {
        // Spawns enemies vertically from each necessary spawn point
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            for (int j = 0; j < wavesToSpawn; j++)
            {
                Instantiate(enemyToSpawn, new Vector3(spawnPoints[i].transform.position.x, spawnPoints[i].transform.position.y + (j * distanceBetweenWaves), 0),
                        spawnPoints[i].transform.rotation, enemyWaveTransform);
            }
        }
    }
}
