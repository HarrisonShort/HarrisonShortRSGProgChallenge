using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class derived from AbstractProjectile for the 
/// missiles that the enemies shoot
/// </summary>
public class EnemyMissile : AbstractProjectile
{
    void Update ()
    {
        ProjectileMovement(Vector3.down);
        DestroySelfWhenOffScreen(false, -10.0f);
    }
}
