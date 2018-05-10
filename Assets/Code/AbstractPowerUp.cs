using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for power ups
/// </summary>
public abstract class AbstractPowerUp : MonoBehaviour
{
    protected PlayerController player;

    protected void OnTriggerEnter2D(Collider collider)
    {
        var player = collider.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            Destroy(gameObject);
        }
    }


}
