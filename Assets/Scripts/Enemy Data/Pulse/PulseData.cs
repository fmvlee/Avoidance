using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pulse Data", menuName = "Pulse Data")]
public class PulseData : ScriptableObject
{
    public bool isPulsing = true;
    public float pulseFactor = 1.25f;
    public float pulseSpeed = 1.25f;
}
