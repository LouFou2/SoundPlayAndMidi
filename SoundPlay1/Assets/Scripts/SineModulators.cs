using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineModulators : MonoBehaviour
{
    // Audiosource variables to be modulated
    private AudioSource m_AudioSource;

    // Sine wave variables

    [SerializeField]
    [Range(0f, 1f)] private float _volumeMin;
    [SerializeField]
    [Range(0f, 1f)] private float _volumeMax; // Volume amplitude = (VolumeMax - Volume Min)/2. Sine offset by +volume amplitude
    
    [SerializeField]
    [Range(0.1f, 20f)] private float _volumeFrequency;  // Volume Sine Frequency

    [SerializeField]
    [Range(0f, 3f)] private float _pitchMin;
    [SerializeField]
    [Range(0f, 3f)] private float _pitchMax;  //(pitch amplitude = same calculation as volume. Pitch has to be clamped between zero and Pitch Max)
    
    [SerializeField]
    [Range(0f, 20f)] private float _pitchFrequency;   // Pitch Sine Frequency



    void Start()
    {
        // Get the audiosource component
        m_AudioSource = GetComponent<AudioSource>();
    }

    float GenerateSineWave(float amplitude, float frequency, float offset)
    {
        return amplitude * Mathf.Sin(2 * Mathf.PI * frequency * Time.time) + offset;
    }


    void Update()
    {
        // Generate Volume Sine Wave, return volume value
        float volumeAmplitude = (_volumeMax - _volumeMin) / 2;
        float volAmpOffset = _volumeMax - volumeAmplitude;
        float newVolume = GenerateSineWave(volumeAmplitude, _volumeFrequency, volAmpOffset);

        // Generate Pitch Sine Wave, return pitch value
        float pitchAmplitude = (_pitchMax - _pitchMin) / 2;
        float pitchAmpOffset = _pitchMax - pitchAmplitude;
        float newPitch = GenerateSineWave(pitchAmplitude, _pitchFrequency, pitchAmpOffset);

        // Send volume value to audiosource volume
        m_AudioSource.volume = newVolume;

        // Send pitch value to audiosource pitch
        m_AudioSource.pitch = Mathf.Clamp(newPitch, 0, _pitchMax);
    }

}
