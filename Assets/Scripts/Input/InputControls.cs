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
        if(GameManager.Instance.currentState == GameManager.GameState.GameOver)
        {
            if(Keyboard.current.anyKey.wasPressedThisFrame){
                Destroy(GameManager.Instance.gameObject);
                SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
            }
        }
    }
}
