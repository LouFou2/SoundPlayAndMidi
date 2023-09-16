using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnimTweaker : MonoBehaviour
{
    [SerializeField] private MidiInputManager midiInputManager; //assign in inspector
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;

    public float[] waveValue;
    [SerializeField] private float[] waveFrequency;
    [SerializeField] private float[] waveAmplitude;

    private int knobIndex = -1;

    [Header("Rhytmic means looping is not used")]
    [SerializeField] private bool rhythmic = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (rhythmic)
        {
            audioSource.loop = false;
        }
        else
        {
            audioSource.loop = true;
        }
        
        int knobCount = 7; // Assuming 7 knobs
        waveValue = new float[knobCount];
        waveFrequency = new float[knobCount];
        waveAmplitude = new float[knobCount];
    }

    void Update()
    {
        if(midiInputManager != null) 
        {
            for (int i = 0; i < waveValue.Length; i++)
            {
                waveValue[i] = GenerateSineWave(waveAmplitude[i], waveFrequency[i]);
                Debug.Log(waveValue[i]);
            }

            waveFrequency[0] = midiInputManager._midiKnob2Value; //frequency knobs
            waveFrequency[1] = midiInputManager._midiKnob3Value;
            waveFrequency[2] = midiInputManager._midiKnob4Value;
            waveFrequency[3] = midiInputManager._midiKnob5Value;
            waveFrequency[4] = midiInputManager._midiKnob6Value;
            waveFrequency[5] = midiInputManager._midiKnob7Value;
            waveFrequency[6] = midiInputManager._midiKnob8Value;

            waveAmplitude[0] = midiInputManager._midiKnob10Value; //amplitude knobs
            waveAmplitude[1] = midiInputManager._midiKnob11Value;
            waveAmplitude[2] = midiInputManager._midiKnob12Value;
            waveAmplitude[3] = midiInputManager._midiKnob13Value;
            waveAmplitude[4] = midiInputManager._midiKnob14Value;
            waveAmplitude[5] = midiInputManager._midiKnob15Value;
            waveAmplitude[6] = midiInputManager._midiKnob16Value;
        }
        

    }
    float GenerateSineWave(float amplitude, float frequency)
    {
        return amplitude * Mathf.Sin(frequency * Time.time);
    }
}
