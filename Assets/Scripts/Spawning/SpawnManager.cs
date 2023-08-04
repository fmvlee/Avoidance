using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

// Handles the spawning of all spawnable objects
public class SpawnManager : MonoBehaviour
{
    // A list of SpawnWaves to spawn
    [SerializeField]
    public List<SpawnWave> spawnWaves;
    // Spawn Pattern Enum
    [SerializeField]
    public enum SpawnPattern { OnPlayer, Random }

    // GUI wave text
    private GameObject waveText;

    // Variable for the wave
    private int currentWave;
    private float spawnTime;

    // Audo for spawning
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;

    private void Awake()
    {
        // Initialise variables
        currentWave = 0;
        spawnTime = 0f;
        waveText = GameObject.FindWithTag("WaveText");
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        // IF the game is playing
        if(GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // All waves are complete then dont try and spawn the next wave
            if(currentWave >= spawnWaves.Count)
            {
                return; 
            }

            // Keep track of time when playing
            spawnTime += Time.deltaTime;

            // Check if its time to spawn the next wave
            if (spawnTime > spawnWaves[currentWave].waveTime)
            {
                // Update the wave text
                UpdateWaveGUIText();
                // Spawn the wave using coroutine for each spawn
                for (int i = 0; i < spawnWaves[currentWave].spawnables.Count; i++)
                {
                    StartCoroutine(Spawn(spawnWaves[currentWave].spawnables[i]));
                }
                // Keep a track of the current wave
                currentWave++;
            }
        }
    }

    // Updates the GUI wave text 
    private void UpdateWaveGUIText()
    {
        // activate the text
        waveText.SetActive(true);
        // Update the text
        waveText.GetComponent<TMPro.TextMeshProUGUI>().text = spawnWaves[currentWave].waveName + "!";
        // Animate the text
        waveText.GetComponent<Animator>().SetTrigger("StartPop");
        // Start coroutine to hide text
        StartCoroutine(HideWaveText());
    }

    // Hides the wave text
    IEnumerator HideWaveText()
    {
        // Wait for time and hide the wave text
        yield return new WaitForSeconds(2.5f);
        waveText.SetActive(false);
    }

    // Spawns all the items
    IEnumerator Spawn(Spawn spawn)
    {        
        // Loop through all of the Spawn
        for(int i = 0; i < spawn.spawnQuantity; i++)
        {
            // Check the game is still in playing state
            if (GameManager.Instance.currentState == GameManager.GameState.Playing)
            {
                // initialise Vector 3
                Vector3 spawnPosition = new Vector3(0, 0, 0);
                // Checks the spawn pattern of the spawn
                switch (spawn.spawnPattern)
                {
                    // If OnPlayer pattern use the players transform position
                    case (SpawnManager.SpawnPattern.OnPlayer):
                        spawnPosition = GameManager.Instance.player.transform.position;
                        break;
                    // If Random use a random screen position
                    case (SpawnManager.SpawnPattern.Random):
                        Vector2 spawnXY = RandomScreenPosition();
                        spawnPosition = new Vector3(spawnXY.x, spawnXY.y, 0f);
                        break;
                }

                // Checks if we are spawning an enemy
                if (spawn.spawnItem.tag == "Enemy")
                {
                    // If enemy then spawn facing a random direction
                    GameObject spawned = Instantiate(spawn.spawnItem, spawnPosition, Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 360))));
                }
                else
                {
                    // If any other item spawn in its normal rotation
                    GameObject spawned = Instantiate(spawn.spawnItem, spawnPosition, Quaternion.identity);
                }

                // Play sound
                PlaySpawnSound();
                // Waits for the spawnInterval time before repeating
                yield return new WaitForSeconds(spawn.spawnInterval);
            }
        }      
    }

    // Gets a random position on the screen
    private Vector2 RandomScreenPosition()
    {
        // Return a random X, Y position within the screen
        return Camera.main.ViewportToWorldPoint(new Vector2(UnityEngine.Random.value, UnityEngine.Random.value));
    }

    // Plays a spawn sound
    private void PlaySpawnSound()
    {
        // Get a random clip from audioClips array
        int randomClip = UnityEngine.Random.Range(0, audioClips.Length);
        // Set the audiosources clip and play the sound
        audioSource.clip = audioClips[randomClip];
        audioSource.Play();
    }
}
