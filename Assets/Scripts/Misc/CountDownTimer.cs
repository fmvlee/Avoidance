using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This handles the start of game countdown 3, 2, 1, GO
public class CountDownTimer : MonoBehaviour
{
    // Number of secnds to count down
    public int countDownTime;
    // The countdown text to display
    public GameObject countDownText;

    // Countdown Sounds and audio source
    [SerializeField]
    private AudioClip countDownSound;
    [SerializeField]
    private AudioClip countDownCompleteSound;
    private AudioSource countDownAudioSource;

    void Start()
    {
        // Initialise variable required
        countDownAudioSource = GetComponent<AudioSource>();
    }

    // A GameEvent triggers this start function
    public void TriggerStart()
    {
        // Start the coundown
        StartCoroutine(StartCountDown());
    }

    // Countdown for game start
    private IEnumerator StartCountDown()
    {
        // While there is time to countdown
        while (countDownTime > 0)
        {
            // Play the countdown sound
            countDownAudioSource.Play();
            // Set the text to the amount of seconds left
            countDownText.GetComponent<TMPro.TextMeshProUGUI>().text = countDownTime.ToString();  
            // Wait for a second
            yield return new WaitForSeconds(1f);
            // Reduce count down time remaining
            countDownTime--;
        }

        // Play the completed countdown sound
        countDownAudioSource.clip = countDownCompleteSound;
        countDownAudioSource.Play();
        // Update the countdown text
        countDownText.GetComponent<TMPro.TextMeshProUGUI>().text = "GO!";
        
        // Starts the Game
        GameManager.Instance.StartGame();
        // Wait for a second and the hide the coundown text
        yield return new WaitForSeconds(1f);
        countDownText.SetActive(false);
    }
}
