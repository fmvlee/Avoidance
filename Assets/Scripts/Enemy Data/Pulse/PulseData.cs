using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a menu option
[CreateAssetMenu(fileName = "Pulse Data", menuName = "Pulse Data")]

// This stores all the data for the pulsing effect
public class PulseData : ScriptableObject
{
    public bool isPulsing = true;
    public float pulseFactor = 1.25f;
    public float pulseSpeed = 1.25f;
}
