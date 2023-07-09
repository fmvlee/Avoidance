using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnEnemy;
    [SerializeField]
    private float timeBetweenSpawn = 1f;

    private void Start()
    {
        StartCoroutine(SpawnChild());
    }

    private IEnumerator SpawnChild()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        GameObject spawned = Instantiate(spawnEnemy, gameObject.transform.position, Quaternion.identity);
        StartCoroutine(SpawnChild());
    }
}
