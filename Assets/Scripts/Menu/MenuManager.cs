using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private void Start()
    {
            //NOT WORKING AS THE SCORES ARE NOT UPDATED IN TIME FOR START
        hiScore.GetComponent<TMPro.TextMeshProUGUI>().text = "You Scored: " + leaderboard.playerHighScore.ToString("D8");
        globalHiScore.GetComponent<TMPro.TextMeshProUGUI>().text = "You Scored: " + leaderboard.globalHighScore.ToString("D8");
    }
    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            // Load the game
            Debug.Log("Load game");
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
        //if (!scoresUpdated)
       // {
            
          //  scoresUpdated = true;
       // }
    }
}
