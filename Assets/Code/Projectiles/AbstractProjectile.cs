using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all projectiles, allowing additional projectiles to
/// be added later (currently only Enemy and Player Missiles)
/// </summary>
public abstract class AbstractProjectile : MonoBehaviour
{
    protected Transform projectileTransform;

    [SerializeField]
    [Tooltip("The speed that missiles travel")]
    private float projectileSpeed = 0.2f;

    void Start()
    {
        projectileTransform = GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check to see if object is a projectile
        AbstractProjectile projectile = collider.GetComponent<AbstractProjectile>();
        if (projectile == null && collider.tag != tag)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            Enemy enemy = collider.GetComponent<Enemy>();
            if (player != null)
            {
                player.GetHit();
            }
            else if (enemy != null)
            {
                enemy.GetHit();
            }

            Destroy(gameObject);
        }
    }

    protected void ProjectileMovement(Vector3 direction)
    {
        projectileTransform.position += direction * projectileSpeed;
    }

    /// <summary>
    /// Method that destroys the projectile once it is far enough off screen
    /// TODO: Come up with more elegant solution
    /// </summary>
    protected void DestroySelfWhenOffScreen(bool playerShot, float distanceToDestroySelf)
    {
        if (playerShot)
        {
            if (projectileTransform.position.y >= distanceToDestroySelf) // TODO: determine by screen height rather than hard code
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (projectileTransform.position.y <= distanceToDestroySelf) // TODO: determine by screen height rather than hard code
            {
                Destroy(gameObject);
            }
        }
    }
}
