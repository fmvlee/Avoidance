using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Score on gameplay screen
    public GameObject scoreText;
    // Players score
    private int score;

    // Scores on game over screen
    public GameObject justScoreText;
    public GameObject playerHiScore;

    // How often the score updates
    public float scoreUpdateTime = 0.01f;
    // How much the score updates
    public int scoreAmountPerTime = 1;

    // Manages leaderboard
    private LeaderboardManager leaderboard;

    private void Awake()
    {
        // Find score text to update
        justScoreText = GameObject.FindGameObjectWithTag("JustScored");
        playerHiScore = GameObject.FindGameObjectWithTag("HiScoreText");
        // Hide the gameover panel
        GameObject.FindGameObjectWithTag("GameOverPanel").SetActive(false); 
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialise variables
        leaderboard = GetComponent<LeaderboardManager>();
        score = 0;
        scoreText = GameObject.FindGameObjectWithTag("Score");
        // Start updating score
        StartCoroutine(UpdateScore());
    }

    // Update the score in fixed update
    private void FixedUpdate()
    {
        // If game state is playing
        if (GameManager.Instance.currentState == GameManager.GameState.Playing && scoreText != null)
        {
            // Update the score text
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString("D8");
        }
    }

    // Update the score
    IEnumerator UpdateScore()
    {
        // If game state is playing and the player is not immune
        if (GameManager.Instance.currentState == GameManager.GameState.Playing && !GameManager.Instance.player.GetComponent<EnemyCollision>().isImmune)
        {
            // Increase score
            score += scoreAmountPerTime;
        }
        // Wait for score update time
        yield return new WaitForSeconds(scoreUpdateTime);
        // Recursively call UpdateScore
        yield return StartCoroutine(UpdateScore());
    }

    // Called on GameOver
    public void GameOver()
    { 
        // Add score to the unity service
        leaderboard.AddScore(score);
        leaderboard.UpdatePlayerHiScore();

        // Update the score just achieved
        justScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "You Scored: " + score.ToString("D8");

        // Check if the new score is bigger than the players other scores and update the scores appropriately
        if (score > leaderboard.playerHighScore) { 
            playerHiScore.GetComponent<TMPro.TextMeshProUGUI>().text = "Your HI Score: " + score.ToString("D8");
        }
        else
        {
            playerHiScore.GetComponent<TMPro.TextMeshProUGUI>().text = "Your HI Score: " + leaderboard.playerHighScore.ToString("D8");
        }
    }

    // Adds to the players score
    public void AddScore(int additionalScore)
    {
        // Increase score
        score += additionalScore;
    }
}
