using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public enum GameState { NotStarted, Playing, GameOver };
    public GameState currentState;

    public GameObject player;
    public LifeManager lifeManager;

    public GameEvent gameStartEvent;

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
        currentState = GameState.NotStarted;
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
    }
    public void StartGame()
    {
        currentState = GameState.Playing;
        gameStartEvent.TriggerEvent();
        player.SetActive(true);
    }   
}
