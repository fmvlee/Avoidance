using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn", menuName = "Spawn Data")]
public class Spawn : ScriptableObject
{
    [SerializeField]
    public GameObject spawnItem;
    [SerializeField]
    public int spawnQuantity = 1;
    [SerializeField]
    public float spawnInterval = 0.5f;
    [SerializeField]
    public SpawnManager.SpawnPattern spawnPattern = SpawnManager.SpawnPattern.Random;
}
