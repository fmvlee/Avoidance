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
        gameControls = new GameControls();
        rb = GetComponent<Rigidbody2D>();
        screenCamera = Camera.main;
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
            SetVelocity();
            RotateWithInputDirection();
        }
    }

    private void SetVelocity()
    {
        smoothedMovement = Vector2.SmoothDamp(smoothedMovement, movementInput, ref movementInputSmoothVelocity, 0.1f);
        rb.velocity = smoothedMovement * movementSpeed;
        PreventPlayerOffScreen();
    }

    private void PreventPlayerOffScreen()
    {
        Vector2 screenPosition = screenCamera.WorldToScreenPoint(transform.position);
        if ((screenPosition.x < screenBorder && rb.velocity.x < 0) || (screenPosition.x > screenCamera.pixelWidth - screenBorder && rb.velocity.x > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if ((screenPosition.y < screenBorder && rb.velocity.y < 0) || (screenPosition.y > screenCamera.pixelHeight - screenBorder && rb.velocity.y > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
    private void RotateWithInputDirection()
    {
        if(movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovement);
            Quaternion rotateTo = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb.SetRotation(rotateTo);
        }
    }
    public void GameOver()
    {
        rb.velocity = Vector3.zero;
    }
}
