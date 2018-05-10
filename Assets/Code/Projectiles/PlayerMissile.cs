using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class derived from AbstractProjectile for the 
/// missiles that the player shoots
/// </summary>
public class PlayerMissile : AbstractProjectile
{
    void Update ()
    {
        ProjectileMovement(Vector3.up);
        DestroySelfWhenOffScreen(true, 10.0f);
    }
}
