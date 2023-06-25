using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public int countDownTime;
    public GameObject countDownText;

    void Start()
    {
        
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        while (countDownTime > 0)
        {
            countDownText.GetComponent<TMPro.TextMeshProUGUI>().text = countDownTime.ToString();  
            yield return new WaitForSeconds(1f);
            countDownTime--;
        }

        countDownText.GetComponent<TMPro.TextMeshProUGUI>().text = "GO!";
        
        // Start Game
        GameManager.Instance.StartGame();

        yield return new WaitForSeconds(1f);

        countDownText.SetActive(false);
    }
}
