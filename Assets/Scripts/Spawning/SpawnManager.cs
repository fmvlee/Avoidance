using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    public List<SpawnWave> spawnWaves;
    // Spawn Pattern
    [SerializeField]
    public enum SpawnPattern { OnPlayer, Random }
    //NEED TO ADD A SPAWN PATTERN ENUM HERE

    private int currentWave;
    private float spawnTime;

    private void Start()
    {
        currentWave = 0;
        spawnTime = 0f;
    }
    private void Update()
    {
        if(GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // All waves are complete then dont try and spawn the next wave
            if(currentWave >= spawnWaves.Count)
            {
                return; 
            }

            // Keep track of time when playing
            spawnTime += Time.deltaTime;

            if (spawnTime > spawnWaves[currentWave].waveTime)
            {
                Debug.Log("Spawn Wave: " + spawnWaves[currentWave].waveName);
                Debug.Log("Spawnables in wave: " + (spawnWaves[currentWave].spawnables.Count));
                // Spawn the wave
                for (int i = 0; i < spawnWaves[currentWave].spawnables.Count; i++)
                {
                    Debug.Log("Start coroutine for: "   + spawnWaves[currentWave].spawnables[i].name);
                    StartCoroutine(Spawn(spawnWaves[currentWave].spawnables[i]));
                }
                currentWave++;
            }
        }
    }

    IEnumerator Spawn(Spawn spawn)
    {        
        for(int i = 0; i < spawn.spawnQuantity; i++)
        {
            //Instantiate
            Debug.Log(spawn.name);
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            switch (spawn.spawnPattern)
            {
                case (SpawnManager.SpawnPattern.OnPlayer):
                    spawnPosition = GameManager.Instance.player.transform.position;
                    break;
                case (SpawnManager.SpawnPattern.Random):
                    spawnPosition = RandomScreenPosition();
                    break;
            }
            GameObject spawned = Instantiate(spawn.spawnItem,spawnPosition, Quaternion.identity); // NEED TP SMANGE SPAWN POSITION HERE
            yield return new WaitForSeconds(spawn.spawnInterval);
        }      
    }

    private Vector3 RandomScreenPosition()
    {
        float randomX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float randomY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        return new Vector3(randomX, randomY, 0);
    }
}