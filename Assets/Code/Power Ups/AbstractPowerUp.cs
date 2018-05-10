using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for power ups
/// </summary>
public abstract class AbstractPowerUp : MonoBehaviour
{
    protected PlayerController player;
    protected bool pickedUp = false;
    protected Transform powerUpTransform;

    [SerializeField]
    [Tooltip("The speed downwards that the power up travels")]
    protected float dropSpeed = 0.05f;

    [SerializeField]
    [Tooltip("The distance below the player character that the power up should destroy itself")]
    private float distanceFromPlayerToDestroy = 2.0f;

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            pickedUp = true;
        }
    }

    /// <summary>
    /// Method that moves the power up downwards when it has
    /// been dropped by an enemy
    /// </summary>
    protected void DroppedMovement()
    {
        powerUpTransform.position += Vector3.down * dropSpeed;
    }

    /// <summary>
    /// Method that allows the power up to destroy itself if 
    /// it has been missed by the player
    /// </summary>
    protected void DestroySelfWhenPastPlayer()
    {
        if (powerUpTransform.position.y <= player.transform.position.y - distanceFromPlayerToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
