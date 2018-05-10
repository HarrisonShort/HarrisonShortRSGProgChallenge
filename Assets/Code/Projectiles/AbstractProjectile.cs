using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all projectiles, allowing additional projectiles to
/// be added later (currently only Enemy and Player Missiles)
/// </summary>
public abstract class AbstractProjectile : MonoBehaviour
{
    protected Transform missileTransform;

    [SerializeField]
    [Tooltip("The speed that missiles travel")]
    private float missileSpeed = 0.2f;

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check to see if object is a projectile
        AbstractProjectile projectile = GetComponent<AbstractProjectile>();
        if (projectile == null)
        {
            if (collider.tag != tag)
            {
                // Trigger kill event here
                Destroy(gameObject);
            }
        }
    }

    protected void ProjectileMovement(Vector3 direction)
    {
        missileTransform.position += direction * missileSpeed;
    }

    protected void DestroySelfWhenOffScreen()
    {
        // Destroy missile object once past screen
        if (missileTransform.position.y >= 10.0f) // determine by screen height rather than hard code
        {
            Destroy(gameObject);
        }
    }









    /// <summary>
    /// Method that destroys missile and opposing character when missile
    /// hits. Opposing character for player is enemy, and vice versa. 
    /// </summary>
    /// <param name="oppositionToDestroy">The opposing character to destroy</param>
    private void HitOpposition(GameObject oppositionToDestroy)
    {
        Destroy(oppositionToDestroy);
        Destroy(gameObject);
    }


    /// <summary>
    /// Determines the likelihood of an enemy dropping a power up
    /// when killed.
    /// </summary>
    private void DetermineChanceOfDroppingPowerUp(GameObject enemyKilled)
    {
        //if (Random.Range(0f, 1f) > chanceOfDrop)
        //{
        //    EnemyWaveController enemy = enemyKilled.GetComponent<EnemyWaveController>();
        //    print("test");
        //    if (enemy != null)
        //    {
        //        enemy.DropPowerUp();
        //        print("test2");

        //    }
        //}
    }

}
