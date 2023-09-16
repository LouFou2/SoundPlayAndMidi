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

    [SerializeField] private float volumeMin = 0.3f; // Set these values in inspector for what works best
    [SerializeField] private float volumeMax = 1f;
    [SerializeField] private float pitchMin = 0.5f;
    [SerializeField] private float pitchMax = 1.5f;

    public float[] waveValue;
    [SerializeField] private float[] waveFrequency;
    [SerializeField] private float[] waveAmplitude;
    private float[] initialWaveFrequency;
    private float[] initialWaveAmplitude;

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
    private int _currentKeyIndex;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
        audioSource.pitch = 1;

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

        initialWaveFrequency = new float[knobCount];
        initialWaveAmplitude = new float[knobCount];


        if (midiInputManager != null)
        {
            for (int i = 0; i < waveValue.Length; i++)
            {
                // Assign MIDI knob values directly from the MidiInputManager
                initialWaveFrequency[i] = GetKnobFrequency(i);
                Debug.Log(initialWaveFrequency[i]);
                initialWaveAmplitude[i] = GetKnobAmplitude(i);
                Debug.Log(initialWaveAmplitude[i]);
            }
            
            _currentKeyIndex = -1;
        }
}

    void Update()
    {
        GetKeyIndex();

        if (zoneTrigger != null && zoneTrigger.isTriggered) { zoneActive = true; }
        if (midiInputManager != null) 
        {
            
            for (int i = 0; i < waveValue.Length; i++)
            {
                // Check if the zone is active before modifying knob-related values
                if (zoneActive)
                {
                    // Assign MIDI knob values directly from the MidiInputManager
                    waveFrequency[i] = GetKnobFrequency(i);
                    waveAmplitude[i] = GetKnobAmplitude(i);
                }

                waveValue[i] = GenerateSineWave(waveAmplitude[i], waveFrequency[i]);
            }
            
            if (rhythmic) 
            {
                DoRhythmicSound(); 
            }
            ModulateSound();
            PassAnimValues();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ActiveScanPoint"))
        {
            // An object on the "ActiveScanPoint" layer has entered the trigger.
            // You can add your logic here.
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
                return 0.5f; // Default value for unknown index
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
                return 0.5f; // Default value for unknown index
        }
    }
    float GenerateSineWave(float amplitude, float frequency)
    {
        return amplitude * Mathf.Sin(frequency * Time.time);
    }

    void DoRhythmicSound() 
    {
        
        float triggerThreshold = waveAmplitude[0] * 0.98f;
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
        if (waveFrequency[1] != initialWaveFrequency[1] && waveAmplitude[1] != initialWaveAmplitude[1])
        {
            float volumeRange = volumeMax - volumeMin;
            float moddedVolumeRange = volumeRange * Mathf.Abs(waveValue[1]); // had to keep it between 0 and 1
            audioSource.volume = moddedVolumeRange; 
        }
        if (waveFrequency[2] != initialWaveFrequency[2] && waveAmplitude[2] != initialWaveAmplitude[2])
        {
            float pitchRange = pitchMax - pitchMin;
            float moddedPitchRange = pitchRange * Mathf.Abs(waveValue[2]); // same
            audioSource.pitch = moddedPitchRange; 
        }
    }
    void AudioPlay() 
    {
        //audioSource.Stop();
        audioSource.Play();
    }

    void PassAnimValues() 
    {
        animator.SetFloat("WaveValue0", waveValue[0]);
        animator.SetFloat("WaveValue1", waveValue[1]);
        animator.SetFloat("WaveValue2", waveValue[2]);
        animator.SetFloat("WaveValue3", waveValue[3]);
        animator.SetFloat("WaveValue4", waveValue[4]);
        animator.SetFloat("WaveValue5", waveValue[5]);
        animator.SetFloat("WaveValue6", waveValue[6]);
    }

}
