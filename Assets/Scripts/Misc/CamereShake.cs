using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CamereShake : MonoBehaviour
{
    // Cameras start  position
    private Vector3 startPosition;

    // Variable for the movement/shake of the camera
    [SerializeField]
    private float minXMovement = 0f;
    [SerializeField]
    private float maxXMovement = 0f;
    [SerializeField]
    private float minYMovement = 0f;
    [SerializeField]
    private float maxYMovement = 0f;
    [SerializeField]
    private float duration = 1f;
    [SerializeField]
    private float magnitude = 0.5f;

    void Awake()
    {
        // Gets the camera start position
        startPosition = transform.localPosition;
    }

    public void StartShake()
    {
        // Start shaking the camera
        StartCoroutine(Shake());
    }

    // Handles shaking the camera by looping of random positions over a short duration
    private IEnumerator Shake()
    {
        // Holder for time passed during shake
        float timePassed = 0f;

        // While there is more time left to shake
        while (timePassed < duration)
        {
            // Get a random X and Y movement
            float xMovement = Random.Range(minXMovement, maxXMovement) * magnitude;
            float yMovement = Random.Range(minYMovement, maxYMovement) * magnitude;
            // Move the camera
            transform.localPosition = new Vector3(xMovement, yMovement, startPosition.z);
            // Add time passed
            timePassed += Time.deltaTime;
            // return
            yield return null;
        }

        // Set the camera to its original position
        transform.localPosition = startPosition;
    }
}
