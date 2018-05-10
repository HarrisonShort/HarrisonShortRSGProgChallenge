using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the Second Chance power up, which allows the player
/// to take one extra hit before dying
/// </summary>
public class SecondChancePowerUp : AbstractPowerUp
{
    void Start ()
    {
        powerUpTransform = GetComponent<Transform>();
        player = FindObjectOfType<PlayerController>();
    }
    
    void Update ()
    {
        DroppedMovement();

        if (pickedUp)
        {
            player.EnableSecondChancePowerUp();
            Destroy(gameObject);
        }
    }
}
