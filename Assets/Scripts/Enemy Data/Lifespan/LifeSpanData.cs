using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a menu option
[CreateAssetMenu(fileName = "Life Data", menuName = "LifeSpan Data")]

// Stroe the date required for the lifespan
public class LifeSpanData : ScriptableObject
{
    public bool hasLifespan = false;
    public float lifespan = 20f;
    public bool randomiseLifespan = false;
    public float minLifespan = 5f;
    public float maxLifespan = 20f;
}
