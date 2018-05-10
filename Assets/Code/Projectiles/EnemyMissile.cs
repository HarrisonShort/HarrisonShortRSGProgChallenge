using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : AbstractProjectile
{

	
	
	// Update is called once per frame
	void Update ()
    {
        // movement and kill off screen
        ProjectileMovement(Vector3.down);
        //DestroySelfWhenOffScreen(-10.0f);
	}
}
