using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Source to load movement data
    [SerializeField]
    private EnemyMovementData enemyData;
    // Speed of the Enemy
    [SerializeField]
    private float speed= 3;
    [SerializeField]
    private bool variedSpeed = false;
    [SerializeField]
    private float speedMin = 3f;
    [SerializeField]
    private float speedMax = 4.2f;

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
    public enum MovementType { Stationary, TargetPlayer, Random, AroundPoint, SineWave }


    private void Awake()
    {
        SetupEnemyData();
        // Get the rigidbody of this enemy
        rb = GetComponent<Rigidbody2D>();
        // Set an initial direction
        targetDirection = transform.up;
        // Get the main camera
        screenCamera = Camera.main;
        // Setup direction cooldown speed
        directionCooldown = Random.Range(directionMinCooldown, directionMaxCooldown);
        // Check if the enemy has a varied speed and choose the speed
        if (variedSpeed)
        {
            speed = Random.Range(speedMin, speedMax);
        }
    }

    private void SetupEnemyData()
    {
        variedSpeed = enemyData.variedSpeed;
        speed = enemyData.speed;
        speedMin = enemyData.minSpeed;
        speedMax = enemyData.maxSpeed;
        rotationSpeed = enemyData.rotationSpeed;
        directionMinCooldown = enemyData.directionMinCooldown;
        directionMaxCooldown = enemyData.directionMaxCooldown;
        directionMinChange = enemyData.directionMinChangeAngle;
        directionMaxChange = enemyData.directionMaxChangeAngle;
        screenBorder = enemyData.screenBorder;
        currentMovementType = enemyData.movementType;
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
        // Reduce direction cooldown by the time passed
        directionCooldown -= Time.deltaTime;

        switch (currentMovementType)
        { 
            case MovementType.TargetPlayer:
                Debug.Log("target player");
                if (directionCooldown <= 0)
                {
                    if (!target.GetComponent<EnemyCollision>().isImmune)
                    {
                        Vector2 enemyToPlayeer = target.transform.position - transform.position;
                        targetDirection = enemyToPlayeer.normalized;
                        RotateToTarget();
                    } else
                    {
                        ChangeDirection();
                    }
                }
                break;
            case MovementType.Random:
                if (directionCooldown <= 0)
                {
                    ChangeDirection();
                }
                Debug.Log("Random Movement");
                break;
            case MovementType.Stationary:
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                Debug.Log("Stationary");
                break;
        }

        // Check enemy not off screen
        PreventEnemyOffScreen();
    }

    // Change the enemy direction to the target direction
    private void ChangeDirection()
    {
        float angleChange = Random.Range(directionMinChange, directionMaxChange);
        Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
        targetDirection = rotation * targetDirection;
        directionCooldown = Random.Range(directionMinCooldown, directionMaxCooldown);
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
