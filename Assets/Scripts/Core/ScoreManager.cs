using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public GameObject scoreText;
    private int score;

    public GameObject justScoreText;
    public GameObject playerHiScore;

    public float scoreUpdateTime = 0.01f;
    public int scoreAmountPerTime = 1;

    private LeaderboardManager leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        leaderboard = GetComponent<LeaderboardManager>();
        score = 0;
        scoreText = GameObject.FindGameObjectWithTag("Score");
        StartCoroutine(UpdateScore());
    }

    private void FixedUpdate()
    {
        //StartCoroutine(UpdateScore());
        if (GameManager.Instance.currentState == GameManager.GameState.Playing && scoreText != null)
        {
            scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString("D8");
        }
    }

    IEnumerator UpdateScore()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Playing && !GameManager.Instance.player.GetComponent<EnemyCollision>().isImmune)
        {
            score += scoreAmountPerTime;
        }
        yield return new WaitForSeconds(scoreUpdateTime);
        yield return StartCoroutine(UpdateScore());
    }

    public void GameOver()
    {
        justScoreText = GameObject.FindGameObjectWithTag("JustScored");
        playerHiScore = GameObject.FindGameObjectWithTag("HiScoreText");
        leaderboard.AddScore(score);
        leaderboard.UpdatePlayerHiScore();
        justScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "You Scored: " + score.ToString("D8");
        if (score > leaderboard.playerHighScore) { 
            playerHiScore.GetComponent<TMPro.TextMeshProUGUI>().text = "Your HI Score: " + score.ToString("D8");
        }
        else
        {
            playerHiScore.GetComponent<TMPro.TextMeshProUGUI>().text = "Your HI Score: " + leaderboard.playerHighScore.ToString("D8");
        }
    }

    public void AddScore(int additionalScore)
    {
        score += additionalScore;
    }
}
