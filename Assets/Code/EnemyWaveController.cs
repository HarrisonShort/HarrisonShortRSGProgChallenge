using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{

    private Transform enemyWaveTransform;
    [SerializeField]
    private float enemyWaveSpeed;

    [SerializeField]
    private GameObject enemyMissile;

    [SerializeField]
    [Tooltip("The rate at which the enemy can shoot missiles")]
    private float missileRate = 0.977f; //why


    void Start ()
    {
        InvokeRepeating("MoveEnemyWave", 0.1f, 0.3f);
        enemyWaveTransform = GetComponent<Transform>();
    }
    
    void MoveEnemyWave()
    {
        //Vector3 nextPosition = new Vector3(enemyWaveTransform.position.x + 1.0f, enemyWaveTransform.position.y, enemyWaveTransform.position.z);
        enemyWaveTransform.position += Vector3.right * enemyWaveSpeed;
        //enemyWaveTransform = Vector3.Lerp(enemyWaveTransform.position, nextPosition, enemyWaveSpeed);

        foreach (Transform enemy in enemyWaveTransform)
        {
            if (enemy.position.x < -7.5f || enemy.position.x > 7.5f)
            {
                enemyWaveSpeed = -enemyWaveSpeed;
                enemyWaveTransform.position += Vector3.down * 0.5f;
                return;
            }

            if (Random.value > missileRate)
            {
                GameObject spawnedEnemyMissile = Instantiate(enemyMissile, enemy.position, enemy.rotation);
                spawnedEnemyMissile.tag = tag;
            }

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

        if (enemyWaveTransform.childCount == 0)
        {
            // Player has won
            // Pause game
            // Should present restart game functionality
            Time.timeScale = 0;
        }
    }
}
