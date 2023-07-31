using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{    
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float screenBorder;

    private GameControls gameControls;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 smoothedMovement;
    private Vector2 movementInputSmoothVelocity;
    private Camera screenCamera;
    
    void Awake()
    {
        // Initialise required variables
        gameControls = new GameControls();
        rb = GetComponent<Rigidbody2D>();
        screenCamera = Camera.main;
    }

    private void OnEnable()
    {
        // Enable the game controls
        gameControls.PlayerActionMap.Enable();
    }

    private void OnDisable()
    {
        // Disable the game controls
        gameControls.PlayerActionMap.Disable();
    }

    private void FixedUpdate()
    {
        // Check the game is in the playing state
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // Set the moment input to the control value
            movementInput = gameControls.PlayerActionMap.Movement.ReadValue<Vector2>();
            // Set the velocity and rotate
            SetVelocity();
            RotateWithInputDirection();
        }
    }

    // Sets the speed the player moves
    private void SetVelocity()
    {
        // Smooth the movement and move the player
        smoothedMovement = Vector2.SmoothDamp(smoothedMovement, movementInput, ref movementInputSmoothVelocity, 0.1f);
        rb.velocity = smoothedMovement * movementSpeed;
        // Check the player doesn't go off screen
        PreventPlayerOffScreen();
    }

    // Stops the player from going off the screen
    private void PreventPlayerOffScreen()
    {
        // Get the position of the camera
        Vector2 screenPosition = screenCamera.WorldToScreenPoint(transform.position);
        // Check if the player has gone off the sides of the screen
        if ((screenPosition.x < screenBorder && rb.velocity.x < 0) || (screenPosition.x > screenCamera.pixelWidth - screenBorder && rb.velocity.x > 0))
        {
            // Stop sideways movement
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Check if the player has gone off the top and bottom of the screen
        if ((screenPosition.y < screenBorder && rb.velocity.y < 0) || (screenPosition.y > screenCamera.pixelHeight - screenBorder && rb.velocity.y > 0))
        {
            // Stops vertical movement
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    // Rotate the player towards the direction input by the player controls
    private void RotateWithInputDirection()
    {
        // Check for movement input
        if(movementInput != Vector2.zero)
        {
            // Get the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovement);
            // Rotate towards the target rotation
            Quaternion rotateTo = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            // Rotate the players rigid body
            rb.SetRotation(rotateTo);
        }
    }

    // Called when the game ends
    public void GameOver()
    {
        // Stops the player movement
        rb.velocity = Vector3.zero;
    }
}
