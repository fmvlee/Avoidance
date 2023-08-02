using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Handles the controls#when the game reaches a GameOver state
public class InputControls : MonoBehaviour
{
    [SerializeField]
    private float waitTime = 3f;

    // True when input allowed
    private bool allowInput;

    // Get Gameover objects to update
    private GameObject continueText;
    
    private void Awake()
    {
        // Initialise
        allowInput = false;
        // Get continue text and hide
        continueText = GameObject.FindWithTag("GameOverContinue");
        continueText.SetActive(false);
    }

    void Update()
    {
        // If GameOver
        if(GameManager.Instance.currentState == GameManager.GameState.GameOver && allowInput)
        {
            // check if any key pressed
            if(Keyboard.current.anyKey.wasPressedThisFrame){
                // Destroy the GameManager and reload the scene
                Destroy(GameManager.Instance.gameObject);
                SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
            }
        }
    }

    // Called on Game Over
    public void GameOver()
    {
        // Start coroutine
        StartCoroutine(AllowInput());
    }

    // Delays the user input on game over
    IEnumerator AllowInput()
    {
        // Wait and then allow input
        yield return new WaitForSeconds(waitTime);
        allowInput = true;
        continueText.SetActive(true);
    }


}
