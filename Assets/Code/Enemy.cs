using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for a single enemy, to enable dying and dropping powerups
/// </summary>
public class Enemy : MonoBehaviour, IKillable
{
    public void Die()
    {
        Destroy(gameObject);
        GameController.instance.IncreaseScore();
        // Play animation

        // Determine possibility of dropping item and drop item if it works


    }

    /// <summary>
    /// Determines the likelihood of an enemy dropping a power up
    /// when killed.
    /// </summary>
    private void DetermineChanceOfDroppingPowerUp(GameObject enemyKilled)
    {
        //if (Random.Range(0f, 1f) > chanceOfDrop)
        //{
        //    EnemyWaveController enemy = enemyKilled.GetComponent<EnemyWaveController>();
        //    print("test");
        //    if (enemy != null)
        //    {
        //        enemy.DropPowerUp();
        //        print("test2");

        //    }
        //}
    }
}
