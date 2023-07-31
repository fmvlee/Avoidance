using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a menu option
[CreateAssetMenu(fileName = "Enemy Movement Data", menuName = "Enemy Movement Data")]

// This scriptable object stores all the movement data
public class EnemyMovementData : ScriptableObject
{
    public bool variedSpeed;
    public float speed;
    public float minSpeed;
    public float maxSpeed;
    public float rotationSpeed;
    public float directionMinCooldown;
    public float directionMaxCooldown;
    public float directionMinChangeAngle;
    public float directionMaxChangeAngle;
    public float screenBorder;
    public EnemyMovement.MovementType movementType;
}
