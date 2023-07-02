using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public GameObject scoreText;
    private int score;

    public GameObject justScoreText;

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
        leaderboard.AddScore(score);
        justScoreText.GetComponent<TMPro.TextMeshProUGUI>().text = "You Scored: " + score.ToString("D8");
    }
}
