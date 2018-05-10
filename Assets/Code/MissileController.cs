using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{

    private Transform missileTransform;

    [SerializeField]
    private float missileSpeed = 0.2f;

    void Start ()
    {
        missileTransform = GetComponent<Transform>();
    }
    
    void Update ()
    {
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
            Debug.Log("Missile tag not defined.");
        }

        // Destroy missile object once past screen
        if (missileTransform.position.y >= 10.0f) // determine by screen height rather than hard code
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Player-fired missile should destroy enemy 
        if (collider.tag != tag && collider.tag == "Enemy")
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
            // Increase player score
        }
        else
        {
            // Trigger life lost/game over
        }
    }
}
