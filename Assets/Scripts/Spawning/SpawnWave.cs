using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Wave", menuName = "Spawn Wave")]
public class SpawnWave : ScriptableObject
{
    [SerializeField]
    public string waveName = "Wave";
    [SerializeField]
    public float waveTime = 0f;
    [SerializeField]
    public List<Spawn> spawnables;
}
