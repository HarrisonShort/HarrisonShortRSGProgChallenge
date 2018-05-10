using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : AbstractProjectile
{
    
    // Update is called once per frame
    void Update ()
    {
        ProjectileMovement(Vector3.up);
        DestroySelfWhenOffScreen(10.0f);
    }
}
