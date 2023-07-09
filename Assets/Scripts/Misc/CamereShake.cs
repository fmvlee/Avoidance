using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CamereShake : MonoBehaviour
{
    private Vector3 startPosition;

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
        startPosition = transform.localPosition;
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }
    private IEnumerator Shake()
    {
        float timePassed = 0f;

        while (timePassed < duration)
        {
            float xMovement = Random.Range(minXMovement, maxXMovement) * magnitude;
            float yMovement = Random.Range(minYMovement, maxYMovement) * magnitude;

            transform.localPosition = new Vector3(xMovement, yMovement, startPosition.z);

            timePassed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = startPosition;
    }
}
