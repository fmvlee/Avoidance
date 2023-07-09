using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public int countDownTime;
    public GameObject countDownText;

    [SerializeField]
    private AudioClip countDownSound;
    [SerializeField]
    private AudioClip countDownCompleteSound;

    private AudioSource countDownAudioSource;
    void Start()
    {
        countDownAudioSource = GetComponent<AudioSource>();
       // StartCoroutine(StartCountDown());
    }

    public void TriggerStart()
    {
        StartCoroutine(StartCountDown());
    }
    private IEnumerator StartCountDown()
    {
        while (countDownTime > 0)
        {
            countDownAudioSource.Play();
            countDownText.GetComponent<TMPro.TextMeshProUGUI>().text = countDownTime.ToString();  
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }

        countDownAudioSource.clip = countDownCompleteSound;
        countDownAudioSource.Play();
        countDownText.GetComponent<TMPro.TextMeshProUGUI>().text = "GO!";
        
        // Start Game
        GameManager.Instance.StartGame();

        yield return new WaitForSeconds(1f);

        countDownText.SetActive(false);
    }
}
