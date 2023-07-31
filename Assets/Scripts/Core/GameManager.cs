using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Make the GameManager a Singleton
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    // Game States to suppot game loop
    public enum GameState { NotStarted, Playing, GameOver };
    public GameState currentState;

    // Player
    public GameObject player;

    // Other managers to support game
    public LifeManager lifeManager;
    public ScoreManager scoreManager;

    // Game start events
    public GameEvent gameStartEvent;
    public GameEvent gameStartCountdownEvent;

    // Audio for Game Manage
    private AudioSource audioSource;

    void Awake()
    {
        // Check if an instance exists
        if (_instance != null && _instance != this)
        {
            // Destroy this if an instance already exists
            Destroy(this.gameObject);
        }
        else
        {
            // Set the instance to this if an instance does not exist
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Initialise variables
        currentState = GameState.NotStarted;
        player = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // On Start trigger the start coroutine
    private void Start()
    {
        StartCoroutine(TriggerStart());
    }

    // Sets the games state on Game Over
    public void GameOver()
    {
        currentState = GameState.GameOver;
    }

    // Triggers the start of the game
    private IEnumerator TriggerStart()
    {
        // Play intro sound
        audioSource.Play();
        // Waits for as long as the audio
        yield return new WaitForSeconds(1.4f);
        // Start the games countdown. 3, 2, 1, Go
        gameStartCountdownEvent.TriggerEvent();
    }

    // Start the Game
    public void StartGame()
    {
        // Update the game state
        currentState = GameState.Playing;
        // Trigger the start of the game
        gameStartEvent.TriggerEvent();
        // Activate player
        player.SetActive(true);
    }
}
