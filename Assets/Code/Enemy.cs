﻿using System.Collections;
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

    public void GetHit()
    {
        DetermineChanceOfDroppingPowerUp();
        GameController.instance.IncreaseScore();
        Destroy(gameObject);
        
        // Play animation
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
