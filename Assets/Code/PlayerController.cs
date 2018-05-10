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
    private float leftBoundary = -7.5f; // TODO: Make this change to the size of the screen
    [SerializeField]
    [Tooltip("The distance furthest right that the player can move")]
    private float rightBoundary = 7.5f; // TODO: Make this change to the size of the screen

    [Header("Missile Parameters")]
    [SerializeField]
    [Tooltip("The missile gameObject we want to spawn")]
    private GameObject missile;
    [SerializeField]
    [Tooltip("The rate at which the player can fire missiles")]
    private float missileRate = 0.5f;

    [Header("Particle Effect Parameters")]
    [SerializeField]
    private ParticleSystem secondChanceParticles;
    [SerializeField]
    private ParticleSystem deathParticles;

    private Transform missileSpawnLocation;
    private float nextMissile;

    private bool isAlive = true;

    // PowerUp Parameters
    private bool secondChancePowerUpApplied = false;

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

        if (isAlive)
        {
            playerTransform.position += Vector3.right * horizontalInput * playerSpeed;
        }
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
            Instantiate(secondChanceParticles, playerTransform.position, playerTransform.rotation);
            return;
        }

        isAlive = false;
        StartCoroutine(WaitUntilParticlesDoneThenGameOver());
    }

    /// <summary>
    /// Coroutine that allows the particle system to play, before
    /// calling the EnableGameOver method in GameController
    /// </summary>
    IEnumerator WaitUntilParticlesDoneThenGameOver()
    {
        Instantiate(deathParticles, playerTransform.position, playerTransform.rotation);

        // Get MeshRenderer and hide player, so it looks like they've blown up
        MeshRenderer playerMesh = GetComponent<MeshRenderer>();
        playerMesh.enabled = false;

        yield return new WaitForSeconds(deathParticles.main.duration);
        Destroy(gameObject);
        GameController.instance.EnableGameOver();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the enemy has reached the bottom of the screen and collides with the player
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            GetHit();
            enemy.GetHit();
        }
    }
}
