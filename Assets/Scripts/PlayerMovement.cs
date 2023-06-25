using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    private GameControls gameControls;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    [SerializeField]
    private float movementSpeed;

    void Awake()
    {
        gameControls = new GameControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        gameControls.PlayerActionMap.Enable();
    }

    private void OnDisable()
    {
        gameControls.PlayerActionMap.Disable();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            movementInput = gameControls.PlayerActionMap.Movement.ReadValue<Vector2>();
            rb.velocity = movementInput * movementSpeed;
        }
    }

    public void GameOver()
    {
        rb.velocity = Vector2.zero;
    }
}
