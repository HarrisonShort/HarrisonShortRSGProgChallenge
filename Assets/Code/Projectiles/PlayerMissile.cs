using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : AbstractProjectile
{
    void Update ()
    {
        ProjectileMovement(Vector3.up);
        DestroySelfWhenOffScreen(true, 10.0f);
    }
}
