using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

// Handles the main loop of the main menu and the actions to take on key presses
public class MenuManager : MonoBehaviour
{
    // Variable to handle scores
    private LeaderboardManager leaderboard;
    private GameObject hiScore;
    private GameObject globalHiScore;

    // Canvas to switch between
    private GameObject mainCanvas;
    private GameObject recordingCanvas;

    // Variables to support playing recording
    [SerializeField]
    private bool isPlayingRecording;
    [SerializeField]
    private float recordingWaitTime;
    [SerializeField]
    private float recordingDefaultWaitTime = 3f;

  // VidPlayer gameplayVideo;

    // Initialise variable on Awake
    private void Awake()
    {
        // Initialise recording and main canvas
        recordingWaitTime = recordingDefaultWaitTime;
        isPlayingRecording = false;
       // gameplayVideo = GameObject.FindWithTag("VidPlayer").GetComponent<VidPlayer>();
        mainCanvas = GameObject.FindWithTag("MainCanvas");
        recordingCanvas = GameObject.FindWithTag("RecordingCanvas");
        recordingCanvas.SetActive(false);

        // Initialise scores
        leaderboard = GetComponent<LeaderboardManager>();
        hiScore = GameObject.FindWithTag("HiScoreText");
        globalHiScore = GameObject.FindWithTag("GlobalHiScoreText");        
    }

    private void Update()
    {   
        // If no key have been pressed the wait time will decrease
        if (recordingWaitTime <= 0)
        {
            // Show the recording canvas
            recordingCanvas.SetActive(true);
            mainCanvas.SetActive(false);
            isPlayingRecording = true;
            // Play Video
            //gameplayVideo.PlayVideo();
        } else
        {
            // Decrease wait time
            recordingWaitTime -= Time.deltaTime;
        }

        // If a key is pressed and no recording playing
        if (Keyboard.current.anyKey.wasPressedThisFrame && isPlayingRecording == false)
        {
            // Load the game
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        // Else of a key is pressed whilst the recording plays
        } else if (Keyboard.current.anyKey.wasPressedThisFrame && isPlayingRecording == true)
        {
            // Stop the recording playing and reset the wait time
            recordingCanvas.SetActive(false);
            mainCanvas.SetActive(true);
            recordingWaitTime = recordingDefaultWaitTime;
            isPlayingRecording = false;
        }

        // Updates the score in the UI
        if(leaderboard.globalHighScore != 0)
        {
            // Update the text with the scores formatted to 8 digits
            hiScore.GetComponent<TMPro.TextMeshProUGUI>().text = leaderboard.playerHighScore.ToString("D8");
            globalHiScore.GetComponent<TMPro.TextMeshProUGUI>().text = leaderboard.globalHighScore.ToString("D8");
        }
        
    }
}
