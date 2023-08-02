
using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField]
    private string fileName;

    void Start()
    {
        PlayVideo();
    }

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer)
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
            videoPlayer.url = filePath;
            videoPlayer.Play();
        }
    }

    private void OnEnable()
    {
        PlayVideo();
    }
}
