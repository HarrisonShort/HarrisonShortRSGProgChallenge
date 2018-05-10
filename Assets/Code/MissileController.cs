using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls missile movement and functionality
/// </summary>
public class MissileController : MonoBehaviour
{
    private Transform missileTransform;

    [SerializeField]
    [Tooltip("The speed that missiles travel")]
    private float missileSpeed = 0.2f;

    void Start ()
    {
        missileTransform = GetComponent<Transform>();
    }
    
    void Update ()
    {
        // Determine movement direction of missile based on assigned tag
        if (tag == "Player")
        {
            missileTransform.position += Vector3.up * missileSpeed;
        }
        else if (tag == "Enemy")
        {
            missileTransform.position += Vector3.down * missileSpeed;
        }
        else
        {
            Debug.LogWarning("Missile tag not defined.");
        }

        // Destroy missile object once past screen
        if (missileTransform.position.y >= 10.0f) // determine by screen height rather than hard code
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Player-fired missile should destroy Enemy 
        if (collider.tag != tag && collider.tag == "Enemy")
        {
            HitOpposition(collider.gameObject);
            GameController.instance.IncreaseScore();
        }
        else if (collider.tag != tag && collider.tag == "Player")
        {
            HitOpposition(collider.gameObject);
            GameController.instance.EnableGameOver();
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
}
