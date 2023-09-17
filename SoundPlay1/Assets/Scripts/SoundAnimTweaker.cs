using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SoundAnimTweaker : MonoBehaviour
{
    [SerializeField] private MidiInputManager midiInputManager; //assign in inspector
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    
    [Header("Rhythmic means looping is not used")]
    [SerializeField] private bool rhythmic = false;
    private bool triggerBufferOn = false;

    [SerializeField] private float defaultVolume = 1f; // Set these values in inspector for what works best
    [SerializeField] private float volumeMin = 0.3f; 
    [SerializeField] private float volumeMax = 1f;
    [SerializeField] private float defaultPitch = 1f;
    [SerializeField] private float pitchMin = 0.5f;
    [SerializeField] private float pitchMax = 1.5f;

    public float[] waveValue;
    [SerializeField] private float[] waveFrequency;
    [SerializeField] private float[] waveAmplitude;
    private float[] previousWaveFrequency;
    private float[] previousWaveAmplitude;

    [SerializeField][Range(1f, 30f)] private float knobMultiplier2 = 1f; // set ranges that each knob (0-1 value) will actually use
    [SerializeField][Range(1f, 30f)] private float knobMultiplier3 = 1f;
    [SerializeField][Range(1f, 30f)] private float knobMultiplier4 = 1f;
    [SerializeField][Range(1f, 30f)] private float knobMultiplier5 = 1f;
    [SerializeField][Range(1f, 30f)] private float knobMultiplier6 = 1f;
    [SerializeField][Range(1f, 30f)] private float knobMultiplier7 = 1f;
    [SerializeField][Range(1f, 30f)] private float knobMultiplier8 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier10 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier11 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier12 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier13 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier14 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier15 = 1f;
    [SerializeField][Range(0.01f, 3f)] private float knobMultiplier16 = 1f;

    //== Will use triggers for each Scanlight Zone (unique zones of sound and animation) ==//
    [SerializeField] private TriggerController zoneTrigger; // Have to assign in inspector
    [SerializeField] private bool zoneActive = false;

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
        
        int knobCount = 7; // Assuming 7 knobs for each (freq/amp)
        waveValue = new float[knobCount];
        waveFrequency = new float[knobCount];
        waveAmplitude = new float[knobCount];

        previousWaveFrequency = new float[knobCount];
        previousWaveAmplitude = new float[knobCount];


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
        GetKeyIndex();

        if (zoneTrigger != null && zoneTrigger.isTriggered) { zoneActive = true; }
        else if (zoneTrigger != null && !zoneTrigger.isTriggered) { zoneActive = false; }

        if (midiInputManager != null) 
        {
            
            for (int i = 0; i < waveValue.Length; i++)
            {
                // Check if the zone is active before modifying knob-related values
                if (zoneActive)
                {
                    /*waveFrequency[i] = GetKnobFrequency(i);
                    waveAmplitude[i] = GetKnobAmplitude(i);*/

                    // Get the current knob values
                    float currentFrequency = GetKnobFrequency(i);
                    float currentAmplitude = GetKnobAmplitude(i);

                    // Check if the knob values have changed
                    if (currentFrequency != previousWaveFrequency[i])
                    {
                        // Knob value has changed, use the current value
                        waveFrequency[i] = currentFrequency;
                    }
                    else
                    {
                        // Knob value has not changed, use a default value
                        waveFrequency[i] = 1.0f; // Adjust the default value as needed
                    }

                    if (currentAmplitude != previousWaveAmplitude[i])
                    {
                        // Knob value has changed, use the current value
                        waveAmplitude[i] = currentAmplitude;
                    }
                    else
                    {
                        // Knob value has not changed, use a default value
                        waveAmplitude[i] = 1.0f; // Adjust the default value as needed
                    }

                }
                Debug.Log("Frequency knob" + i + ":" + waveFrequency[i]);
                Debug.Log("Amplitude knob" + i + ":" + waveAmplitude[i]);

                waveValue[i] = GenerateSineWave(waveAmplitude[i], waveFrequency[i]);
                //Debug.Log("waveValue" + i + ": " + waveValue[i]);
            }
            
            if (rhythmic) 
            {
                DoRhythmicSound(); 
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
            case 3:
                return midiInputManager._midiKnob5Value * knobMultiplier5;
            case 4:
                return midiInputManager._midiKnob6Value * knobMultiplier6;
            case 5:
                return midiInputManager._midiKnob7Value * knobMultiplier7;
            case 6:
                return midiInputManager._midiKnob8Value * knobMultiplier8;
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
            case 3:
                return midiInputManager._midiKnob13Value * knobMultiplier13;
            case 4:
                return midiInputManager._midiKnob14Value * knobMultiplier14;
            case 5:
                return midiInputManager._midiKnob15Value * knobMultiplier15;
            case 6:
                return midiInputManager._midiKnob16Value * knobMultiplier16;
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
        
        float triggerThreshold = waveAmplitude[0] * 0.98f; // Multiply Amplitude with appropriate factor to get a buffer threshold
        if(midiInputManager._midiKnob2Value == 0) { triggerBufferOn = true; } // just a hacky way I stopped it from playing once at the start
        // Check if waveValue[0] crosses the trigger threshold
        if ((waveValue[0] <= -triggerThreshold || waveValue[0] >= triggerThreshold) && !triggerBufferOn) 
        {
            triggerBufferOn = true; // ensure that audio doesnt get triggered every next update
            AudioPlay();
        }
        // Check if waveValue[0] returns to within the threshold range
        if (waveValue[0] > -triggerThreshold && waveValue[0] < triggerThreshold) 
        {
            triggerBufferOn = false;
        }
    }
    void ModulateSound() 
    {
        float volumeRange = volumeMax - volumeMin;
        float moddedVolumeRange = volumeRange * Mathf.Abs(waveValue[1]); // had to keep it between 0 and 1
        audioSource.volume = moddedVolumeRange;

        float pitchRange = pitchMax - pitchMin;
        float moddedPitchRange = pitchRange * Mathf.Abs(waveValue[2]); // same
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
        if (_currentKeyIndex == 0) 
        {
            animator.SetFloat("1WaveValue0", waveValue[0]);
            animator.SetFloat("1WaveValue1", waveValue[1]);
            animator.SetFloat("1WaveValue2", waveValue[2]);
            animator.SetFloat("1WaveValue3", waveValue[3]);
            animator.SetFloat("1WaveValue4", waveValue[4]);
            animator.SetFloat("1WaveValue5", waveValue[5]);
            animator.SetFloat("1WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 1) {
            animator.SetFloat("2WaveValue0", waveValue[0]);
            animator.SetFloat("2WaveValue1", waveValue[1]);
            animator.SetFloat("2WaveValue2", waveValue[2]);
            animator.SetFloat("2WaveValue3", waveValue[3]);
            animator.SetFloat("2WaveValue4", waveValue[4]);
            animator.SetFloat("2WaveValue5", waveValue[5]);
            animator.SetFloat("2WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 2) {
            animator.SetFloat("3WaveValue0", waveValue[0]);
            animator.SetFloat("3WaveValue1", waveValue[1]);
            animator.SetFloat("3WaveValue2", waveValue[2]);
            animator.SetFloat("3WaveValue3", waveValue[3]);
            animator.SetFloat("3WaveValue4", waveValue[4]);
            animator.SetFloat("3WaveValue5", waveValue[5]);
            animator.SetFloat("3WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 3) {
            animator.SetFloat("4WaveValue0", waveValue[0]);
            animator.SetFloat("4WaveValue1", waveValue[1]);
            animator.SetFloat("4WaveValue2", waveValue[2]);
            animator.SetFloat("4WaveValue3", waveValue[3]);
            animator.SetFloat("4WaveValue4", waveValue[4]);
            animator.SetFloat("4WaveValue5", waveValue[5]);
            animator.SetFloat("4WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 4) {
            animator.SetFloat("5WaveValue0", waveValue[0]);
            animator.SetFloat("5WaveValue1", waveValue[1]);
            animator.SetFloat("5WaveValue2", waveValue[2]);
            animator.SetFloat("5WaveValue3", waveValue[3]);
            animator.SetFloat("5WaveValue4", waveValue[4]);
            animator.SetFloat("5WaveValue5", waveValue[5]);
            animator.SetFloat("5WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 5) {
            animator.SetFloat("6WaveValue0", waveValue[0]);
            animator.SetFloat("6WaveValue1", waveValue[1]);
            animator.SetFloat("6WaveValue2", waveValue[2]);
            animator.SetFloat("6WaveValue3", waveValue[3]);
            animator.SetFloat("6WaveValue4", waveValue[4]);
            animator.SetFloat("6WaveValue5", waveValue[5]);
            animator.SetFloat("6WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 6)
        {
            animator.SetFloat("7WaveValue0", waveValue[0]);
            animator.SetFloat("7WaveValue1", waveValue[1]);
            animator.SetFloat("7WaveValue2", waveValue[2]);
            animator.SetFloat("7WaveValue3", waveValue[3]);
            animator.SetFloat("7WaveValue4", waveValue[4]);
            animator.SetFloat("7WaveValue5", waveValue[5]);
            animator.SetFloat("7WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 7) {
            animator.SetFloat("8WaveValue0", waveValue[0]);
            animator.SetFloat("8WaveValue1", waveValue[1]);
            animator.SetFloat("8WaveValue2", waveValue[2]);
            animator.SetFloat("8WaveValue3", waveValue[3]);
            animator.SetFloat("8WaveValue4", waveValue[4]);
            animator.SetFloat("8WaveValue5", waveValue[5]);
            animator.SetFloat("8WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 8) {
            animator.SetFloat("9WaveValue0", waveValue[0]);
            animator.SetFloat("9WaveValue1", waveValue[1]);
            animator.SetFloat("9WaveValue2", waveValue[2]);
            animator.SetFloat("9WaveValue3", waveValue[3]);
            animator.SetFloat("9WaveValue4", waveValue[4]);
            animator.SetFloat("9WaveValue5", waveValue[5]);
            animator.SetFloat("9WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 9) {
            animator.SetFloat("10WaveValue0", waveValue[0]);
            animator.SetFloat("10WaveValue1", waveValue[1]);
            animator.SetFloat("10WaveValue2", waveValue[2]);
            animator.SetFloat("10WaveValue3", waveValue[3]);
            animator.SetFloat("10WaveValue4", waveValue[4]);
            animator.SetFloat("10WaveValue5", waveValue[5]);
            animator.SetFloat("10WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 10) {
            animator.SetFloat("11WaveValue0", waveValue[0]);
            animator.SetFloat("11WaveValue1", waveValue[1]);
            animator.SetFloat("11WaveValue2", waveValue[2]);
            animator.SetFloat("11WaveValue3", waveValue[3]);
            animator.SetFloat("11WaveValue4", waveValue[4]);
            animator.SetFloat("11WaveValue5", waveValue[5]);
            animator.SetFloat("11WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 11) {
            animator.SetFloat("12WaveValue0", waveValue[0]);
            animator.SetFloat("12WaveValue1", waveValue[1]);
            animator.SetFloat("12WaveValue2", waveValue[2]);
            animator.SetFloat("12WaveValue3", waveValue[3]);
            animator.SetFloat("12WaveValue4", waveValue[4]);
            animator.SetFloat("12WaveValue5", waveValue[5]);
            animator.SetFloat("12WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 12) {
            animator.SetFloat("13WaveValue0", waveValue[0]);
            animator.SetFloat("13WaveValue1", waveValue[1]);
            animator.SetFloat("13WaveValue2", waveValue[2]);
            animator.SetFloat("13WaveValue3", waveValue[3]);
            animator.SetFloat("13WaveValue4", waveValue[4]);
            animator.SetFloat("13WaveValue5", waveValue[5]);
            animator.SetFloat("13WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 13) {
            animator.SetFloat("14WaveValue0", waveValue[0]);
            animator.SetFloat("14WaveValue1", waveValue[1]);
            animator.SetFloat("14WaveValue2", waveValue[2]);
            animator.SetFloat("14WaveValue3", waveValue[3]);
            animator.SetFloat("14WaveValue4", waveValue[4]);
            animator.SetFloat("14WaveValue5", waveValue[5]);
            animator.SetFloat("14WaveValue6", waveValue[6]);
        }
        if (_currentKeyIndex == 14) {
            animator.SetFloat("15WaveValue0", waveValue[0]);
            animator.SetFloat("15WaveValue1", waveValue[1]);
            animator.SetFloat("15WaveValue2", waveValue[2]);
            animator.SetFloat("15WaveValue3", waveValue[3]);
            animator.SetFloat("15WaveValue4", waveValue[4]);
            animator.SetFloat("15WaveValue5", waveValue[5]);
            animator.SetFloat("15WaveValue6", waveValue[6]);
        }
    }
}
