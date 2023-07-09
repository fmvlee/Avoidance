using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;
    public GameEvent gameOverEvent;
    public GameObject[] livesImages;
    [SerializeField]
    public Sprite lifeLostSprite;
    [SerializeField]
    public Sprite lifeSprite;
    private Animator animator;

    [SerializeField]
    private GameEvent loseLifeEvent;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip lifeLostClip;
    [SerializeField]
    private AudioClip gameOverClip;

    private void Start()
    {
        livesImages = GameObject.FindGameObjectsWithTag("Lives");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = lifeLostClip;
    }

    public void RemoveLifeImage()
    {
        Image lifeImage = livesImages[lives].GetComponent<Image>();
        lifeImage.GetComponent<Animator>().SetTrigger("StartPop");
        lifeImage.sprite = lifeLostSprite;
        lifeImage.color = new Color(lifeImage.color.r, lifeImage.color.g, lifeImage.color.b, 0.33f);
    }

    public void AddLifeImage()
    {
        Image lifeImage = livesImages[lives].GetComponent<Image>();
        lifeImage.GetComponent<Animator>().SetTrigger("StartPop");
        lifeImage.sprite = lifeSprite;
        lifeImage.color = new Color(lifeImage.color.r, lifeImage.color.g, lifeImage.color.b, 1f);
    }

    public void AddLife()
    {
        if (lives < 3)
        {
            AddLifeImage();
            lives++;            
        }
    }

    public void RemoveLife()
    {
        loseLifeEvent.TriggerEvent();
        lives--;
        audioSource.Play();
        RemoveLifeImage();       
        if (lives == 0)
        {
            //GAME OVER
            audioSource.clip = gameOverClip;
            audioSource.Play();
            gameOverEvent.TriggerEvent();
        }
    }
}