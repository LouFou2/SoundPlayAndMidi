using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZoneValues", menuName = "Create Zone Values")]
public class ZoneValues : ScriptableObject
{
    public float[] waveValue;
    public float[] waveFrequency;
    public float[] waveAmplitude;
}
