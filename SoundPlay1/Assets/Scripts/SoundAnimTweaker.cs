using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SoundAnimTweaker : MonoBehaviour
{
    [SerializeField] private MidiInputManager midiInputManager; //assign in inspector
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ZoneValues zoneValues;
    
    [Header("Rhythmic means looping is not used")]
    [SerializeField] private bool rhythmic = false;
    private bool triggerBufferOn = false;

    //== Will use triggers for each Scanlight Zone (unique zones of sound and animation) ==//
    [SerializeField] private TriggerController zoneTrigger; // Have to assign in inspector
    [SerializeField] private bool zoneActive = false;

    [SerializeField] private float defaultVolume = 1f; // Set these values in inspector for what works best
    [SerializeField] private float volumeMin = 0.3f; 
    [SerializeField] private float volumeMax = 1f;
    [SerializeField] private float defaultPitch = 1f;
    [SerializeField] private float pitchMin = 0.5f;
    [SerializeField] private float pitchMax = 1.5f;

    [SerializeField] private float[] waveValue;
    [SerializeField] private float[] waveFrequency;
    [SerializeField] private float[] waveAmplitude;
    private float[] previousWaveFrequency;
    private float[] previousWaveAmplitude;
    private bool[] frequencyHasChanged;
    private bool[] amplitudeHasChanged;

    [SerializeField][Range(1f, 30f)] private float knobMultiplier2 = 1f; // set ranges that each knob (0-1 value) will actually use
    [SerializeField][Range(1f, 30f)] private float knobMultiplier3 = 1f;
    [SerializeField][Range(1f, 30f)] private float knobMultiplier4 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier10 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier11 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier12 = 1f;

    // private int knobIndex = -1; // might need this later
    private int _currentKeyIndex =-1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = defaultVolume;
        audioSource.pitch = defaultPitch;

        if (rhythmic)
        {
            audioSource.loop = false;
        }
        else
        {
            audioSource.loop = true;
        }

        int knobsUsed = 3;
        waveValue = new float[knobsUsed];
        waveFrequency = new float[knobsUsed];
        waveAmplitude = new float[knobsUsed];

        previousWaveFrequency = new float[knobsUsed];
        previousWaveAmplitude = new float[knobsUsed];

        frequencyHasChanged = new bool[waveValue.Length];
        amplitudeHasChanged = new bool[waveValue.Length];

        if (midiInputManager != null)
        {
            for (int i = 0; i < waveValue.Length; i++)
            {
                // Assign MIDI knob values directly from the MidiInputManager
                previousWaveFrequency[i] = GetKnobFrequency(i);
                Debug.Log("Initial Frequency " + i + ": " + previousWaveFrequency[i]);
                previousWaveAmplitude[i] = GetKnobAmplitude(i);
                Debug.Log("Initial Amplitude " + i + ": " + previousWaveAmplitude[i]);
            }
            
            _currentKeyIndex = -1;
        }
}

    void Update()
    {
        _currentKeyIndex = GetKeyIndex();
        Debug.Log(_currentKeyIndex);

        if (zoneTrigger != null && zoneTrigger.isTriggered) 
        {
            zoneActive = true; 
        }
        else if (zoneTrigger != null && !zoneTrigger.isTriggered) { zoneActive = false; }

        if (midiInputManager != null) 
        {
            
            for (int i = 0; i < waveValue.Length; i++)
            {
                
                // Check if the zone is active before modifying knob-related values
                if (zoneActive)
                {
                    // Get the current knob values
                    float currentFrequency = GetKnobFrequency(i);
                    float currentAmplitude = GetKnobAmplitude(i);

                    if (currentFrequency != previousWaveFrequency[i] && !frequencyHasChanged[i]) 
                    {
                        frequencyHasChanged[i] = true;
                    }
                    if (currentAmplitude != previousWaveAmplitude[i] && !amplitudeHasChanged[i])
                    {
                        amplitudeHasChanged[i] = true;
                    }
                    // Check if the knob values have changed
                    if (frequencyHasChanged[i])
                    {
                        // Knob value has changed, use the current value
                        waveFrequency[i] = currentFrequency;
                    }
                    else
                    {
                        // Knob value has not changed, use a default value
                        waveFrequency[i] = 1.0f; // Adjust the default value as needed
                    }

                    if (amplitudeHasChanged[i])
                    {
                        // Knob value has changed, use the current value
                        waveAmplitude[i] = currentAmplitude;
                    }
                    else
                    {
                        // Knob value has not changed, use a default value
                        waveAmplitude[i] = 1.0f; // Adjust the default value as needed
                    }
                    zoneValues.waveFrequency[i] = waveFrequency[i];
                    zoneValues.waveAmplitude[i] = waveAmplitude[i];
                }

                zoneValues.waveValue[i] = GenerateSineWave(zoneValues.waveAmplitude[i], zoneValues.waveFrequency[i]); // *** The issue here is: other objects with the same script will send 0 values
                
                Debug.Log("waveValue" + i + ": " + waveValue[i]); // *** So even if I do nice calculations here and use knobs, it will just be 0
            }
            
            if (rhythmic) 
            {
                DoRhythmicSound(); 
            }
            else if (!rhythmic && !audioSource.isPlaying) 
            {
                audioSource.Play();
            }
            ModulateSound();
            PassAnimValues();
        }
    }
    private int GetKeyIndex() 
    {
        return midiInputManager.currentKeyIndex;
    }
    // Method to get knob frequency value by index
    private float GetKnobFrequency(int knobIndex)
    {
        switch (knobIndex)
        {
            case 0:
                return midiInputManager._midiKnob2Value * knobMultiplier2;
            case 1:
                return midiInputManager._midiKnob3Value * knobMultiplier3;
            case 2:
                return midiInputManager._midiKnob4Value * knobMultiplier4;
            default:
                return 1f; // Default value for unknown index
        }
    }

    // Method to get knob amplitude value by index
    private float GetKnobAmplitude(int knobIndex)
    {
        switch (knobIndex)
        {
            case 0:
                return midiInputManager._midiKnob10Value * knobMultiplier10;
            case 1:
                return midiInputManager._midiKnob11Value * knobMultiplier11;
            case 2:
                return midiInputManager._midiKnob12Value * knobMultiplier12;
            default:
                return 1f; // Default value for unknown index
        }
    }

    float GenerateSineWave(float amplitude, float frequency)
    {
        //Debug.Log("Amp: " + amplitude + "Freq: " +  frequency);
        return amplitude * Mathf.Sin(frequency * Time.time);
    }

    void DoRhythmicSound() 
    {
        
        float triggerThreshold = zoneValues.waveAmplitude[0] * 0.98f; // Multiply Amplitude with appropriate factor to get a buffer threshold
        
        // Check if waveValue[0] crosses the trigger threshold
        if ((zoneValues.waveValue[0] <= -triggerThreshold || zoneValues.waveValue[0] >= triggerThreshold) && !triggerBufferOn) 
        {
            triggerBufferOn = true; // ensure that audio doesnt get triggered every next update
            AudioPlay();
        }
        // Check if waveValue[0] returns to within the threshold range
        if (zoneValues.waveValue[0] > -triggerThreshold && zoneValues.waveValue[0] < triggerThreshold) 
        {
            triggerBufferOn = false;
        }
    }
    void ModulateSound() 
    {
        float volumeRange = volumeMax - volumeMin;
        float moddedVolumeRange = volumeRange * Mathf.Abs(zoneValues.waveValue[1]); // had to keep it between 0 and 1
        audioSource.volume = moddedVolumeRange;

        float pitchRange = pitchMax - pitchMin;
        float moddedPitchRange = pitchRange * Mathf.Abs(zoneValues.waveValue[2]); // same
        audioSource.pitch = moddedPitchRange;

    }
    void AudioPlay() 
    {
        //audioSource.Stop();
        audioSource.Play();
    }

    void PassAnimValues()
    {
        if (_currentKeyIndex == -1) { return; }
        Debug.Log("Passing Anim Values...");
        if (_currentKeyIndex == 0) 
        {
            Debug.Log("Passing Value" + zoneValues.waveValue[0]);
            animator.SetFloat("1WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("1WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("1WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 1) {
            animator.SetFloat("2WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("2WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("2WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 2) {
            animator.SetFloat("3WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("3WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("3WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 3) {
            animator.SetFloat("4WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("4WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("4WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 4) {
            animator.SetFloat("5WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("5WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("5WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 5) {
            animator.SetFloat("6WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("6WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("6WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 6)
        {
            animator.SetFloat("7WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("7WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("7WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 7) {
            animator.SetFloat("8WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("8WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("8WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 8) {
            animator.SetFloat("9WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("9WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("9WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 9) {
            animator.SetFloat("10WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("10WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("10WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 10) {
            animator.SetFloat("11WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("11WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("11WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 11) {
            animator.SetFloat("12WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("12WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("12WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 12) {
            animator.SetFloat("13WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("13WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("13WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 13) {
            animator.SetFloat("14WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("14WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("14WaveValue2", zoneValues.waveValue[2]);
        }
        if (_currentKeyIndex == 14) {
            animator.SetFloat("15WaveValue0", zoneValues.waveValue[0]);
            animator.SetFloat("15WaveValue1", zoneValues.waveValue[1]);
            animator.SetFloat("15WaveValue2", zoneValues.waveValue[2]);
        }
    }
}
