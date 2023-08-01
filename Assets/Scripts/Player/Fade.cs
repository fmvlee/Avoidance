using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // Fade in speed
    [SerializeField]
    private float fadeOutSpeed = 0.1f;
    // Fade in amount perspeed
    [SerializeField]
    private float fadeOutAmount = 0.1f;
    // Alpha from 0 to 1 to start at
    [SerializeField]
    private float startAlpha = 1f;

    // Object Renderer
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Start the fade routine
        StartCoroutine(StartFade());
    }

    // Handles the fading in effect
    IEnumerator StartFade()
    {
        // Fade out the Alpha
        float newAlpha = Mathf.Clamp(spriteRenderer.color.a - fadeOutAmount, 0, 1);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
        // Wait for fade out speed
        yield return new WaitForSeconds(fadeOutSpeed);
        // Recursivly call the function untilfully faded in
        if (spriteRenderer.color.a > 0)
        {
            StartCoroutine(StartFade());
        } else
        {
            // Destroy once the object has faded out
            Destroy(gameObject);
        }
    }
}
