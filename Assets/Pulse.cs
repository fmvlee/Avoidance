using NUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    // Checks if the object is pulsing
    [SerializeField]
    private bool isPulsing = true;
    // Scale to pulse towards
    [SerializeField]
    private Vector3 pulseTo;
    // Scale to Pulse From
    private Vector3 pulseFrom;
    // Speed of pulsing effect
    [SerializeField]
    private float pulseSpeed = 1.25f;
    // Start time of movement
    private float startTime;
    // Where the object has scaled to so far
    private float scaledAmount;

    private void Start()
    {
        // Get the objects current scale
        pulseFrom = gameObject.transform.localScale;
        // Sets time and distance
        PulseSetup();
    }

    void FixedUpdate()
    {
        // Check the object should be pulsing
        if (isPulsing)
        {
            // Calculate the scaled amount over time
            float scaleMoved = (Time.time - startTime) * pulseSpeed;
            // Calculate the fraction of scale moved
            float fractionOfScale = scaleMoved / scaledAmount;

            // Update the transforms scale
            transform.localScale = Vector3.Lerp(pulseFrom, pulseTo, fractionOfScale);

            // When the transform reaches the end point
            if (transform.localScale == pulseTo)
            {
                // Switch start and end scale values
                SwitchScaleTargets(pulseTo, pulseFrom);
                // Reset time and distance
                PulseSetup();
            }
        }
    }

    // Setup the pulsing object
    private void PulseSetup()
    {
        // Get the current time
        startTime = Time.time;
        // Set the scale to transform
        scaledAmount = Vector3.Distance(pulseFrom, pulseTo);
    }

    // Switch the start and end vectors so the scale changes in the opposite way
    private void SwitchScaleTargets(Vector3 start, Vector3 end)
    {
        pulseTo = end;
        pulseFrom = start;
    }        
}
