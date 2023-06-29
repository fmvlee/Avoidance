using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Speed of the Enemy
    [SerializeField]
    private float speed= 3;
    // Speed the Enemy can turn
    [SerializeField]
    private float rotationSpeed = 5;
    // Target for the enemy
    [SerializeField]
    private GameObject target;
    // Time between changes in direction
    private float directionCooldown;
    // Minimum time between changes in direction
    [SerializeField]
    private float directionMinCooldown = 1f;
    // Maximum time between changes in direction
    [SerializeField]
    private float directionMaxCooldown = 5f;
    [SerializeField]
    private float directionMinChange = -90f;
    // Maximum time between changes in direction
    [SerializeField]
    private float directionMaxChange = 90f;
    // Distance the enemy can go off the screen
    [SerializeField]
    private float screenBorder;
    // Direction to the target (Player)
    private Vector2 targetDirection;
    // Enemy rigidbody for movement
    private Rigidbody2D rb;
    // Reference to the main camera
    private Camera screenCamera;
    // The type of movement the Enemy will follow
    [SerializeField]
    private MovementType currentMovementType;
    // Selectable MovementTypes
    private enum MovementType { TargetPlayer, Random, AroundPoint, SineWave }

    private void Awake()
    {
        // Get the rigidbody of this enemy
        rb = GetComponent<Rigidbody2D>();
        // Set an initial direction
        targetDirection = transform.up;
        // Get the main camera
        screenCamera = Camera.main;
    }

    private void FixedUpdate()
    {     
        // Check the game is playing
        if (GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            // Call methods that support movement
            UpdateTargetDirection();
            RotateToTarget();
            SetVelocity();
        }
    }

    private void UpdateTargetDirection()
    {
        directionCooldown -= Time.deltaTime;    
        if(directionCooldown <= 0)
        {
            if (!target.GetComponent<EnemyCollision>().isImmune && currentMovementType == MovementType.TargetPlayer)
            {
                Vector2 enemyToPlayeer = target.transform.position - transform.position;
                targetDirection = enemyToPlayeer.normalized;
            }

            float angleChange = Random.Range(directionMinChange, directionMaxChange);
            Quaternion rotaion = Quaternion.AngleAxis(angleChange   , transform.forward);
            targetDirection = rotaion * targetDirection;
            directionCooldown = Random.Range(directionMinCooldown, directionMaxCooldown);
        }
        PreventEnemyOffScreen();
    }

    private void UpdateRandomDirection()
    {
        
    }

    // Check if the enemy has gone off the side of the screen and sends it towards the play area
    private void PreventEnemyOffScreen()
    {
        // Get the screen position of the enemy
        Vector2 screenPosition = screenCamera.WorldToScreenPoint(transform.position);
        // Check if the enemy has gone off the sides of the screen
        if((screenPosition.x < screenBorder && targetDirection.x < 0) || (screenPosition.x > screenCamera.pixelWidth - screenBorder && targetDirection.x >0))
        {
            // Change direction back onto screen
            targetDirection = new Vector2(-targetDirection.x, targetDirection.y);
        }
        // Check if the enemy has gone off the top or bottom of the screen
        if ((screenPosition.y < screenBorder && targetDirection.y  < 0) || (screenPosition.y > screenCamera.pixelHeight - screenBorder && targetDirection.y > 0))
        {
            // Change direction back onto screen
            targetDirection = new Vector2(targetDirection.x, -targetDirection.y);
        }
    }

    // Turn the enemy towards a target
    private void RotateToTarget()
    {
        // Get the rotation to the target
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        // Rotate to the target rotation with speed multiplier
        Quaternion rotateTo = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        // Rotate the enemy rigid body
        rb.SetRotation(rotateTo);
    }

    // Sets the velocity of the enemy
    private void SetVelocity()
    {
        // Set the velocity of the rigid body with speed multiplier
        rb.velocity = transform.up * speed;
    }

    // Called when the Gameover Event is called
    public void GameOver()  
    {
        // Stop the enemy moving
        rb.velocity = Vector3.zero;
    }
}
