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

    void Start ()
    {
        InvokeRepeating("MoveEnemyWave", 0.1f, enemyWaveSpeed);
        enemyWaveTransform = GetComponent<Transform>();
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
            if (enemy.position.x < -7.5f || enemy.position.x > 7.5f)
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

            //TODO: make some sense of this
            if (enemy.position.y <= -4)
            {
                // Kill player
                // Pause game time
            }
        }

        // Refactor out to own method, allow to have varying levels of difficulty
        if(enemyWaveTransform.childCount == 1)
        {
            CancelInvoke();
            InvokeRepeating("MoveEnemyWave", 0.1f, 0.25f);
        }
    }
}
