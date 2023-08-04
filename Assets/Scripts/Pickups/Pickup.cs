using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // The amount of additional score the pickup will give
    [SerializeField]
    private int scoreIncreaseValue = 0;
    // Pickup text
    [SerializeField]
    private GameObject textObject;
    private TMPro.TextMeshPro pickupText;

    // Handles the type of pickup
    [SerializeField]
    private PickupType pickupType;
    public enum PickupType { Life, Score }

    // Handles the audio
    private AudioSource audioSource;

    // Fade in speed
    [SerializeField]
    private float fadeOutSpeed = 0.1f;
    // Fade in amount perspeed
    [SerializeField]
    private float fadeOutAmount = 0.1f;

    private void Start()
    {
        // Initialise the Audio Source
        audioSource = GetComponent<AudioSource>();
        pickupText = textObject.GetComponent<TMPro.TextMeshPro>();
        pickupText.text = "+" + scoreIncreaseValue.ToString();
        pickupText.enabled = false;
    }

    // Detects if there is a collision with the pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides and the game is playing
        if (collision.tag == "Player" && GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // Disable the current objects collider to prevent retriggering
            GetComponent<CapsuleCollider2D>().enabled = false;
            // Play sound and destroy object
            StartCoroutine(PickupItem());

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

    IEnumerator PickupItem()
    {
        // Show the pickup text and start fading it
        pickupText.enabled = true;
        StartCoroutine(StartFade());
        // Hide the object
        gameObject.GetComponent<Renderer>().enabled = false;
        // Play the pickup sound
        audioSource.Play();
        // Wait until the sound has finished playing
        yield return new WaitForSeconds(audioSource.clip.length);
        // Destroy the pickup
        Destroy(gameObject);
    }

    IEnumerator StartFade()
    {
        // Fade out the Alpha
        float newAlpha = Mathf.Clamp(pickupText.color.a - fadeOutAmount, 0, 1);
        pickupText.color = new Color(pickupText.color.r, pickupText.color.g, pickupText.color.b, newAlpha);
        // Wait for fade out speed
        yield return new WaitForSeconds(fadeOutSpeed);
        // Recursivly call the function untilfully faded in
        if (pickupText.color.a > 0)
        {
            StartCoroutine(StartFade());
        }
        else
        {
            // Destroy once the object has faded out
            Destroy(gameObject);
        }
    }
}
