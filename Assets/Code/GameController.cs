﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that keeps track of player score and controls the current state of the game 
/// (playing, game over, player wins), and then updates UI elements accordingly
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Score Parameters")]
    [SerializeField]
    private int score = 0;
    [SerializeField]
    [Tooltip("The score needed to win the game (defined as 10 by challenge parameters)")]
    private int scoreToWin = 10;

    [Header("UI Element Objects")]
    [SerializeField]
    private GameObject gameResultObject;
    [SerializeField]
    private GameObject scoreObject;
    [SerializeField]
    private GameObject restartPromptObject;
    [SerializeField]
    private GameObject powerUpIconObject;

    private Text gameResultText;
    private Text scoreText;

    [Header("Text Parameters")]
    [SerializeField]
    private string scoreDisplayString = "SCORE: ";
    [SerializeField]
    private string winString = "YOU WON!";
    [SerializeField]
    private string loseString = "YOU DIED.";

    private bool isPlayerDead = false;
    // Whether the player has won OR lost
    private bool isGameOver = false;
    
    // Get scene index to ensure we load correct scene each time
    private int mainSceneIndex;

    // Provide animation to determine how long we should wait for the animations to finish
    [Header("Other Parameters")]
    [SerializeField]
    [Tooltip("Provide animation to determine how long we should wait for the animations to finish")]
    private AnimationClip sampleAnimation; 

    void Awake()
    {
        // Simple singleton pattern to ensure
        // only one GameController can exist at one time
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        gameResultText = gameResultObject.GetComponent<Text>();
        scoreText = scoreObject.GetComponent<Text>();
        mainSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    
    void Update ()
    {
        // Display win
        if (score == scoreToWin)
        {
            TriggerGameComplete(winString, Color.green);
        }
        
        // Display loss
        if (isPlayerDead)
        {
            TriggerGameComplete(loseString, Color.red);
        }

        // Let player restart game using the fire button when game is over 
        if (isGameOver)
        {
            if (Input.GetButton("Fire1"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(mainSceneIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Method which updates the end-game text and then displays the
    /// text on screen, prompting player to restart the game. Also stops
    /// the game time, so that no more movement occurs until game is reset.
    /// </summary>
    /// <param name="textToDisplay">The end-game text to display</param>
    private void TriggerGameComplete(string textToDisplay, Color colour)
    {
        isGameOver = true;
        gameResultText.text = textToDisplay;
        gameResultText.color = colour;
        gameResultObject.SetActive(true);
        restartPromptObject.SetActive(true);
        gameResultObject.GetComponent<Animation>().Play("YouLostAnim");
        restartPromptObject.GetComponent<Animation>().Play("RestartPromptFadeIn");

        StartCoroutine(WaitForAnimationToStop());
    }

    /// <summary>
    /// Public method allowing MissileController class to increase score variable 
    /// when a player missile successfully destroys an Enemy
    /// </summary>
    public void IncreaseScore()
    {
        score++;
        scoreText.text = scoreDisplayString + score.ToString();
    }

    /// <summary>
    /// Public method allowing MissileController class to indicate that
    /// player has died when they have been shot, therefore ending the game
    /// </summary>
    public void EnableGameOver()
    {
        isPlayerDead = true;
    }

    /// <summary>
    /// Toggles the power up UI icon in the top right of the screen,
    /// which allows the player to see when they have a power up
    /// </summary>
    public void EnablePowerUpIcon(bool powerUpObtained)
    {
        powerUpIconObject.SetActive(powerUpObtained);
    }

    /// <summary>
    /// Coroutine that waits for the animations to stop before setting
    /// Time.timeScale to 0
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForAnimationToStop()
    {
        // Subtract 0.1f so animation doesn't reset
        // TODO: Find better way of implementing this (Animator instead?)
        yield return new WaitForSeconds(sampleAnimation.length - 0.1f);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Method to multiply the new amount of enemies
    /// whenever a JSON file has been read and waves
    /// are spawning from that data
    /// </summary>
    public void UpdateScoreToWin(int enemies, int waves)
    {
        scoreToWin = enemies * waves;
    }
}
