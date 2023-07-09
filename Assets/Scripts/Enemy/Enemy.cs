using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Time before fading in
    [SerializeField]
    private float fadeInWaitTime = 2f;
    // Fade in speed
    [SerializeField]
    private float fadeInSpeed = 0.1f;
    // Fade in amount perspeed
    [SerializeField]
    private float fadeInAmount = 0.1f;
    // Alpha from 0 to 1 to start at
    [SerializeField]
    private float startAlpha = 0.4f;
    // Disable movement whilst faded out
    [SerializeField]
    private bool disableMovementOnStart = true;

    // Sprite to fade in
    private SpriteRenderer spriteRenderer;

    // Components to disable
    private EnemyMovement movement;
    private Lifespan lifeSpan;
    private Pulse pulse;
    private Rigidbody2D rb;

    private Spawner childSpawner;

    void Start()
    {
        // Get components to disable/enable
        movement = GetComponent<EnemyMovement>();
        lifeSpan = GetComponent<Lifespan>();
        pulse = GetComponent<Pulse>();
        rb = GetComponent<Rigidbody2D>();

        // Get the sprite render and set the could to the starting alpha
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Clamp(startAlpha,0 , 1));

        if (GetComponent<Spawner>())
        {
            childSpawner = GetComponent<Spawner>();
        }

        // Disable all but the sprite render
        DisableComponents();
        // Start fade in routine
        StartCoroutine(FadeInEnemy());
    }

    // Disable the components we dont want to be active
    private void DisableComponents()
    {
        // Movement disabled if the option is true
        if (disableMovementOnStart)
        {
            movement.enabled = false;
        }
        lifeSpan.enabled = false;
        pulse.enabled = false;
    }
    // Enable the components
    private void EnableComponents()
    {
        movement.enabled = true;
        lifeSpan.enabled = true;
        pulse.enabled = true;
        gameObject.layer = 6;
    }

    // Coroutine to begin fade in operation
    IEnumerator FadeInEnemy()
    {
        // Wait for fadeInTime
        yield return new WaitForSeconds(fadeInWaitTime);
        // Enable relevent components
        StartCoroutine(FadeInAlpha());
        
    }

    IEnumerator FadeInAlpha()
    {
        // Fade in the Alpha
        float newAlpha = Mathf.Clamp(spriteRenderer.color.a + fadeInAmount, 0, 1);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
        // Wait for fade in speed
        yield return new WaitForSeconds(fadeInSpeed);
        // Recursivly call the function untilfully faded in
        if(spriteRenderer.color.a < 1)
        {
            StartCoroutine(FadeInAlpha());
        } else
        {
            EnableComponents();
            if (childSpawner)
            {
                childSpawner.enabled = true;
            }
        }
    }
}
