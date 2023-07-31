using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Handles the controls when the game reaches a GameOver state
public class InputControls : MonoBehaviour
{
    void Update()
    {
        // If GameOver
        if(GameManager.Instance.currentState == GameManager.GameState.GameOver)
        {
            // check if any key pressed
            if(Keyboard.current.anyKey.wasPressedThisFrame){
                // Destroy the GameManager and reload the scene
                Destroy(GameManager.Instance.gameObject);
                SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
            }
        }
    }
}
