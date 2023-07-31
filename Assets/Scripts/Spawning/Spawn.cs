using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add menu option
[CreateAssetMenu(fileName = "Spawn", menuName = "Spawn Data")]
// This scriptable object contains the information required for its spawn
public class Spawn : ScriptableObject
{
    // Prefab to spawn, can be any item e.g. enemy or pickup item
    [SerializeField]
    public GameObject spawnItem;
    // The aount to spawn
    [SerializeField]
    public int spawnQuantity = 1;
    // The time between each spawn
    [SerializeField]
    public float spawnInterval = 0.5f;
    // The spawn pattern to use e.g. random spawns anywhere on screen
    [SerializeField]
    public SpawnManager.SpawnPattern spawnPattern = SpawnManager.SpawnPattern.Random;
}
