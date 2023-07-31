using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Object to spawn
    [SerializeField]
    private GameObject spawnObject;
    // Time before next spawn
    [SerializeField]
    private float timeBetweenSpawn = 1f;

    private void Start()
    {
        // Start spawning the child object
        StartCoroutine(SpawnChild());
    }

    private IEnumerator SpawnChild()
    {
        // Wait for specified time before spawning
        yield return new WaitForSeconds(timeBetweenSpawn);
        //Spawn the object
        GameObject spawned = Instantiate(spawnObject, gameObject.transform.position, Quaternion.identity);

        // If the Game State is playing
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // Continue spawning child objects
            StartCoroutine(SpawnChild());
        }
    }
}
