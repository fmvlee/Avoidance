
using UnityEngine;
using UnityEngine.Video;

// Handles the video playing. Video files must be in the StreamingAssets folder
public class VidPlayer : MonoBehaviour
{
    // File name of the video e.g. File.mp4
    [SerializeField]
    private string fileName;

    // Play the video on start
    void Start()
    {
        PlayVideo();
    }

    // Play the video
    public void PlayVideo()
    {
        // Get the video player
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        // If a video player found
        if (videoPlayer)
        {
            // Get the path to the video
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
            // Set the video url and play the video
            videoPlayer.url = filePath;
            videoPlayer.Play();
        }
    }

    // When enabled play the video
    private void OnEnable()
    {
        PlayVideo();
    }
}
