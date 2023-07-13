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

    private GameObject mainCanvas;
    private GameObject recordingCanvas;

    [SerializeField]
    private bool isPlayingRecording;
    [SerializeField]
    private float recordingWaitTime;
    [SerializeField]
    private float recordingDefaultWaitTime = 3f;
    //private bool scoresUpdated = false;

    private void Awake()
    {
        recordingWaitTime = recordingDefaultWaitTime;
        isPlayingRecording = false;
        leaderboard = GetComponent<LeaderboardManager>();
        hiScore = GameObject.FindWithTag("HiScoreText");
        globalHiScore = GameObject.FindWithTag("GlobalHiScoreText");
        mainCanvas = GameObject.FindWithTag("MainCanvas");
        recordingCanvas = GameObject.FindWithTag("RecordingCanvas");
        recordingCanvas.SetActive(false);
    }

    private void Update()
    {        
        if (recordingWaitTime <= 0)
        {
            recordingCanvas.SetActive(true);
            mainCanvas.SetActive(false);
            isPlayingRecording = true;
        } else
        {
            recordingWaitTime -= Time.deltaTime;
        }

        if (Keyboard.current.anyKey.wasPressedThisFrame && isPlayingRecording == false)
        {
            // Load the game
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        } else if (Keyboard.current.anyKey.wasPressedThisFrame && isPlayingRecording == true)
        {
            recordingCanvas.SetActive(false);
            mainCanvas.SetActive(true);
            recordingWaitTime = recordingDefaultWaitTime;
            isPlayingRecording = false;
        }

        // Updates the score in the UI - Would be better to move to start metod
        hiScore.GetComponent<TMPro.TextMeshProUGUI>().text = leaderboard.playerHighScore.ToString("D8");
        globalHiScore.GetComponent<TMPro.TextMeshProUGUI>().text = leaderboard.globalHighScore.ToString("D8");
    }
}
