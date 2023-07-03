using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputControls : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Listen for any key to be pressed - if ESC then exit
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Exit Game");
            //Exit if in the editor - else close application
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        if(GameManager.Instance.currentState == GameManager.GameState.GameOver)
        {
            if(Keyboard.current.anyKey.wasPressedThisFrame){
                Destroy(GameManager.Instance.gameObject);
                SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
            }
        }
    }
}
