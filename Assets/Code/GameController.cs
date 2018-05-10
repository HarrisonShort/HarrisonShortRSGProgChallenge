using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that keeps track of player score and controls the current state of the game 
/// (playing, game over, player wins), and then updates UI elements accordingly
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField]
    private GameObject gameResultObject;
    private Text gameResultText;

    private bool isGameOver = false;

    [SerializeField]
    private int score = 0;
    [SerializeField]
    [Tooltip("The score needed to win the game (defined as 10 by challenge parameters)")]
    private int scoreToWin = 10;

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
    }
    
    void Update ()
    {
        //if game win
        if (score == scoreToWin)
        {
            TriggerGameComplete("YOU WON!");
        }
        
        // if game lose
        if (isGameOver)
        {
            TriggerGameComplete("YOU DIED.");
        }
    }

    private void TriggerGameComplete(string textToDisplay)
    {
        gameResultText.text = textToDisplay;
        gameResultObject.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Public method allowing MissileController class to increase score when
    /// a player missile successfully destroys an Enemy
    /// </summary>
    public void IncreaseScore()
    {
        score++;
    }

    /// <summary>
    /// Public method allowing MissileController class to indicate that
    /// player has died when they have been shot, therefore endin the game
    /// </summary>
    public void EnableGameOver()
    {
        isGameOver = true;
    }


}
