using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    // Data to be loaded
    [SerializeField]
    private LifeSpanData lifeSpanData;

    // Variables required for lifespan
    [SerializeField]
    private bool hasLifespan = false;
    [SerializeField]
    private float lifespan = 20f;
    [SerializeField]
    private bool randomiseLifespan = false;
    [SerializeField]
    private float minLifespan = 5f;
    [SerializeField]
    private float maxLifespan = 20f;

    // Destroy effect
    [SerializeField]
    private float effectSpeed = 0.05f;
    [SerializeField]
    private float effectTransparancy = 0.6f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // Load the data
        hasLifespan = lifeSpanData.hasLifespan;
        lifespan = lifeSpanData.lifespan;
        randomiseLifespan = lifeSpanData.randomiseLifespan;
        minLifespan = lifeSpanData.minLifespan;
        maxLifespan = lifeSpanData.maxLifespan;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        // Check if the object has a lifespan
        if(hasLifespan)
        {
            // If the lifespan is random then generate a random life between the min and max lifespan
            if (randomiseLifespan)
            {
                lifespan = Random.Range(minLifespan, maxLifespan);
            }
            // Start a coroutine to destroy the object
            StartCoroutine(EndObjectLife());
            StartCoroutine(EndEffectStart());
        }
    }

    IEnumerator EndEffectStart()
    {
        // Wait for the objects lifespan
        yield return new WaitForSeconds(lifespan - 1.5f);
        StartCoroutine(EndEffect());
    }

    IEnumerator EndEffect()
    {
        if(spriteRenderer.color.a == effectTransparancy)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        } else
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, effectTransparancy);
        }
        // Wait for the objects lifespan
        yield return new WaitForSeconds(effectSpeed);
        StartCoroutine(EndEffect());
    }

    // Handles ending the objects life
    IEnumerator EndObjectLife()
    {
        // Wait for the objects lifespan
        yield return new WaitForSeconds(lifespan);
        // Destroy the object
        Destroy(gameObject);
    }
}
