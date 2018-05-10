using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Transform playerTransform;

    [SerializeField]
    private float playerSpeed = 0.2f;
    [SerializeField]
    [Tooltip("The distance furthest left that the player can move")]
    private float leftBoundary = -7.5f; //TODO: Make this change to the size of the screen (dynamically too?)
    [SerializeField]
    [Tooltip("The distance furthest right that the player can move")]
    private float rightBoundary = 7.5f; //TODO: Make this change to the size of the screen (dynamically too?)

    [SerializeField]
    private GameObject missile;
    private Transform missileSpawnLocation;
    private float missileRate = 0.5f;

    private float nextMissile;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        missileSpawnLocation = playerTransform;
    }
    
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (playerTransform.position.x < leftBoundary && horizontalInput < 0)
        {
            horizontalInput = 0;
        }
        else if (playerTransform.position.x > rightBoundary && horizontalInput > 0)
        {
            horizontalInput = 0;
        }

        playerTransform.position += Vector3.right * horizontalInput * playerSpeed;
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextMissile)
        {
            nextMissile = Time.time + missileRate;
            GameObject spawnedMissile = Instantiate(missile, missileSpawnLocation.position, missileSpawnLocation.rotation);
            spawnedMissile.tag = tag;
        }
    }
}
