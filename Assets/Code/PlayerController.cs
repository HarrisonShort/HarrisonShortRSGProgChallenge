using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls player's movement and shooting mechanics
/// </summary>
public class PlayerController : MonoBehaviour, IKillable
{
    private Transform playerTransform;

    [Header("Movement Parameters")]
    [SerializeField]
    private float playerSpeed = 0.2f;
    [SerializeField]
    [Tooltip("The distance furthest left that the player can move")]
    private float leftBoundary = -7.5f; //TODO: Make this change to the size of the screen (dynamically too?)
    [SerializeField]
    [Tooltip("The distance furthest right that the player can move")]
    private float rightBoundary = 7.5f; //TODO: Make this change to the size of the screen (dynamically too?)

    [Header("Missile Parameters")]
    [SerializeField]
    [Tooltip("The missile gameObject we want to spawn")]
    private GameObject missile;
    [SerializeField]
    [Tooltip("The rate at which the player can fire missiles")]
    private float missileRate = 0.5f;

    private Transform missileSpawnLocation;
    private float nextMissile;

    // PowerUp Parameters
    public bool secondChancePowerUpApplied = false;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        missileSpawnLocation = playerTransform;
    }
    
    void FixedUpdate()
    {
        PlayerMovement();
    }

    void Update()
    {
        FireMissile();
    }

    /// <summary>
    /// Method that handles player's horizontal (i.e. only) movement
    /// </summary>
    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Do not allow player to move past left and right boundaries of screen
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

    /// <summary>
    /// Method that allows the player to fire a missile when they've pressed the button
    /// </summary>
    private void FireMissile()
    {
        if (Input.GetButton("Fire1") && Time.time > nextMissile)
        {
            nextMissile = Time.time + missileRate;
            GameObject spawnedMissile = Instantiate(missile, missileSpawnLocation.position, missileSpawnLocation.rotation);
            spawnedMissile.tag = tag;
        }
    }

    /// <summary>
    /// Allows the Second Chance Power Up to enable
    /// </summary>
    public void EnableSecondChancePowerUp()
    {
        secondChancePowerUpApplied = true;
        GameController.instance.EnablePowerUpIcon(true);
    }

    public void GetHit()
    {
        if (secondChancePowerUpApplied)
        {
            secondChancePowerUpApplied = false;
            GameController.instance.EnablePowerUpIcon(false);
            return;
        }
        
        // Play death animation

        Destroy(gameObject);
        GameController.instance.EnableGameOver();
    }
}
