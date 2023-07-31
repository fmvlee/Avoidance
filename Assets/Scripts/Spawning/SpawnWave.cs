using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a menu option
[CreateAssetMenu(fileName = "Spawn Wave", menuName = "Spawn Wave")]

// Stores the wave information
public class SpawnWave : ScriptableObject
{
    [SerializeField]
    public string waveName = "Wave";
    [SerializeField]
    public float waveTime = 0f;
    // Spawnables can include any gameobject e.g. enemy or pickup
    [SerializeField]
    public List<Spawn> spawnables;
}
