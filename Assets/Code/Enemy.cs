using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for a single enemy, to enable dying and dropping powerups
/// </summary>
public class Enemy : MonoBehaviour, IKillable
{
    // Array of different powerups that can be dropped
    [SerializeField]
    private GameObject[] powerUpsToDrop;

    [SerializeField]
    [Tooltip("Chance of a power up dropping when this enemy dies")]
    private float chanceOfDrop = 0.5f;

    [SerializeField]
    private GameObject deathParticles;

    // Determine whether enemy is alive so one enemy cannot give multiple points
    private bool isAlive = true;

    public void GetHit()
    {
        
        DetermineChanceOfDroppingPowerUp();
        if (isAlive)
        {
            GameController.instance.IncreaseScore();
            isAlive = false;
        }
        Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    /// <summary>
    /// Determines the likelihood of an enemy dropping a power up
    /// when killed.
    /// </summary>
    private void DetermineChanceOfDroppingPowerUp()
    {
        if (Random.Range(0f, 1f) > chanceOfDrop)
        {
            DropPowerUp();
        }
    }

    private void DropPowerUp()
    {
        if (powerUpsToDrop.Length >= 1)
        {
            Instantiate(powerUpsToDrop[0], transform.position, transform.rotation);

        }
    }
}
