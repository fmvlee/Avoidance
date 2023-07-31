using System;
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

    private GameObject waveText;

    private int currentWave;
    private float spawnTime;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audioClips;

    private void Awake()
    {
        currentWave = 0;
        spawnTime = 0f;
        waveText = GameObject.FindWithTag("WaveText");
        audioSource = GetComponent<AudioSource>();
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
                UpdateWaveGUIText();
                Debug.Log("Spawn Wave: " + spawnWaves[currentWave].waveName + " at a time of " + spawnTime);
                Debug.Log("Spawnables in wave: " + (spawnWaves[currentWave].spawnables.Count));
                // Spawn the wave
                for (int i = 0; i < spawnWaves[currentWave].spawnables.Count; i++)
                {
                    Debug.Log("Start coroutine for: " + spawnWaves[currentWave].spawnables[i].name);
                    StartCoroutine(Spawn(spawnWaves[currentWave].spawnables[i]));
                }
                currentWave++;
            }
        }
    }

    private void UpdateWaveGUIText()
    {
        //NEED TO ADD ANIMATION IN HERE
        waveText.SetActive(true);
        waveText.GetComponent<TMPro.TextMeshProUGUI>().text = spawnWaves[currentWave].waveName + "!";
        waveText.GetComponent<Animator>().SetTrigger("StartPop");
        StartCoroutine(HideWaveText());
    }

    IEnumerator HideWaveText()
    {
        yield return new WaitForSeconds(2.5f);
        waveText.SetActive(false);
    }
    IEnumerator Spawn(Spawn spawn)
    {        
        for(int i = 0; i < spawn.spawnQuantity; i++)
        {
            //Instantiate
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            switch (spawn.spawnPattern)
            {
                case (SpawnManager.SpawnPattern.OnPlayer):
                    spawnPosition = GameManager.Instance.player.transform.position;
                    break;
                case (SpawnManager.SpawnPattern.Random):
                    Vector2 spawnXY = RandomScreenPosition();
                    spawnPosition = new Vector3(spawnXY.x, spawnXY.y, 0f);
                    break;
            }

            if (spawn.spawnItem.tag == "Enemy")
            {
                GameObject spawned = Instantiate(spawn.spawnItem, spawnPosition, Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(0, 360))));
            } else
            {
                GameObject spawned = Instantiate(spawn.spawnItem, spawnPosition, Quaternion.identity);
            }
            PlaySpawnSound();
            yield return new WaitForSeconds(spawn.spawnInterval);
        }      
    }

    private Vector2 RandomScreenPosition()
    {
        return Camera.main.ViewportToWorldPoint(new Vector2(UnityEngine.Random.value, UnityEngine.Random.value));
    }

    private void PlaySpawnSound()
    {
        int randomClip = UnityEngine.Random.Range(0, audioClips.Length);
        audioSource.clip = audioClips[randomClip];
        audioSource.Play();
    }
}
