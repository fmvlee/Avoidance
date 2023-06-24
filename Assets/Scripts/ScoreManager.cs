using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public GameObject scoreText;
    private int score;

    public float scoreUpdateTime = 0.01f;
    public int scoreAmountPerTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        StartCoroutine(UpdateScore());
    }

    private void FixedUpdate()
    {
        //StartCoroutine(UpdateScore());
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString("D8");
    }

    IEnumerator UpdateScore()
    {
        score += scoreAmountPerTime;
        yield return new WaitForSeconds(scoreUpdateTime);
        yield return StartCoroutine(UpdateScore());
    }
}
