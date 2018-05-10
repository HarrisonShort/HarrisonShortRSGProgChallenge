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

}
