using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;
    [SerializeField]
    public GameObject livesText;

    private void Start()
    {
        UpdateLives();
    }

    public void UpdateLives()
    {
        livesText.GetComponent<TMPro.TextMeshProUGUI>().text = lives.ToString();
    }

    public void AddLife()
    {
        lives++;
        UpdateLives();
    }

    public void RemoveLife()
    {
        lives--;
        UpdateLives();
    }
}

