using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MenuManager : MonoBehaviour
{
    private LeaderboardManager leaderboard;
    private GameObject hiScore;
    private GameObject globalHiScore;

    //private bool scoresUpdated = false;

    private void Awake()
    {
        leaderboard = GetComponent<LeaderboardManager>();
        hiScore = GameObject.FindWithTag("HiScoreText");
        globalHiScore = GameObject.FindWithTag("GlobalHiScoreText");
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            // Load the game
            Debug.Log("Load game");
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }

        // Updates the score in the UI - Would be better to move to start metod
        hiScore.GetComponent<TMPro.TextMeshProUGUI>().text = leaderboard.playerHighScore.ToString("D8");
        globalHiScore.GetComponent<TMPro.TextMeshProUGUI>().text = leaderboard.globalHighScore.ToString("D8");
    }
}
