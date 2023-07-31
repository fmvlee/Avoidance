using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // The amount of additional score the pickup will give
    [SerializeField]
    private int scoreIncreaseValue = 0;

    // Handles the type of pickup
    [SerializeField]
    private PickupType pickupType;
    public enum PickupType { Life, Score }

    // Handles the audio
    private AudioSource audioSource;

    private void Awake()
    {
        // Initialise the Audio Source
        audioSource = GetComponent<AudioSource>();
    }

    // Detects if there is a collision with the pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides and the game is playing
        if (collision.tag == "Player" && GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // Play sound and destroy object
            StartCoroutine(PickupSound());

            // Decide on the action to take depending on the pickupType
            switch (pickupType)
            {
                case PickupType.Life:
                    GameManager.Instance.lifeManager.AddLife();
                    break;
                case PickupType.Score:
                    GameManager.Instance.scoreManager.AddScore(scoreIncreaseValue);
                    break;
                default:
                    return;
            }            
        }
    }

    IEnumerator PickupSound()
    {
        // Hide the object
        gameObject.GetComponent<Renderer>().enabled = false;
        // Play the pickup sound
        audioSource.Play();
        // Wait until the sound has finished playing
        yield return new WaitForSeconds(audioSource.clip.length);
        // Destroy the pickup
        Destroy(gameObject);
    }
}
