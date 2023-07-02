using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{    
    private void Update()
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
        else if(Keyboard.current.anyKey.wasPressedThisFrame)
        {
            // Load the game
            Debug.Log("Load game");
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }
}
