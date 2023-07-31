using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

// LifeManager handles the action, animations and sounds of loosing or gaining a life
public class LifeManager : MonoBehaviour
{
    // Lives that the player starts with
    private int lives = 3;

    // Events to trigger
    public GameEvent gameOverEvent;
    public GameEvent loseLifeEvent;

    // Life images
    public GameObject[] livesImages;
    [SerializeField]
    public Sprite lifeLostSprite;
    [SerializeField]
    public Sprite lifeSprite;

    // Life animator
    private Animator animator;

    // Life audiosource
    private AudioSource audioSource;
    // Sounds for loosing a life and gameover
    [SerializeField]
    private AudioClip lifeLostClip;
    [SerializeField]
    private AudioClip gameOverClip;

    // Start initialises the variable required
    private void Start()
    {
        // Get the life images
        livesImages = GameObject.FindGameObjectsWithTag("Lives");
        // Get the audio component and set the clip
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = lifeLostClip;
    }

    // Updates the life image and opacity
    public void UpdateLifeImage(string life)
    {
        // Get the image to update
        Image lifeImage = livesImages[lives].GetComponent<Image>();
        // Animate the image
        lifeImage.GetComponent<Animator>().SetTrigger("StartPop");
        // Set the sprite and its opacity
        if (life == "Remove")
        {
            lifeImage.sprite = lifeLostSprite;
            lifeImage.color = new Color(lifeImage.color.r, lifeImage.color.g, lifeImage.color.b, 0.33f);
        } else
        {
            lifeImage.sprite = lifeSprite;
            lifeImage.color = new Color(lifeImage.color.r, lifeImage.color.g, lifeImage.color.b, 1f);
        }
    }

    // Adds a life to the player
    public void AddLife()
    {
        // Max lives = 3
        if (lives < 3)
        {
            // Update the life image and update the players lives
            UpdateLifeImage("Add");
            lives++;            
        }
    }

    // Removes a life from the player
    public void RemoveLife()
    {
        // Trigger event on life lost
        loseLifeEvent.TriggerEvent();
        // Remove life and play sound
        lives--;
        audioSource.Play();
        // Update the life images
        UpdateLifeImage("Remove");  
        // If no lives remain
        if (lives == 0)
        {
            // GAME OVER - Play gameover sound
            audioSource.clip = gameOverClip;
            audioSource.Play();
            // Trigger game over event
            gameOverEvent.TriggerEvent();
        }
    }
}