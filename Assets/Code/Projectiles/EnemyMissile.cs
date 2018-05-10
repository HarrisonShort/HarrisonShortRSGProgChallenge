using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : AbstractProjectile
{
    void Update ()
    {
        ProjectileMovement(Vector3.down);
        DestroySelfWhenOffScreen(false, -10.0f);
    }
}
