using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimToSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _gameObject;

    // Audio Source variables

    [Header("Volume")]
    [SerializeField] private bool _modulateVolume = false;

    [Header("Choose 1 Transform to Modulate Volume")]
    [SerializeField] private bool _positionXtoVolume = false;
    [SerializeField] private bool _positionYtoVolume = false;
    [SerializeField] private bool _positionZtoVolume = false;
    [SerializeField]
    [Range(0f, 1f)] private float _volumeMin;
    [SerializeField]
    [Range(0f, 1f)] private float _volumeMax;
    
    private float _modVolumeValue;

    [Header("Pitch")]
    [SerializeField] private bool _modulatePitch = false;

    [Header("Choose 1 Transform to Modulate Pitch")]
    [SerializeField] private bool _positionXtoPitch = false;
    [SerializeField] private bool _positionYtoPitch = false;
    [SerializeField] private bool _positionZtoPitch = false;
    [SerializeField]
    [Range(0f, 3f)] private float _pitchMin;
    [SerializeField]
    [Range(0f, 3f)] private float _pitchMax;
        
    private float _modPitchValue;

    // GameObject Variables
    private Vector3 _initialPosition = Vector3.zero;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //_initialPosition = _gameObject.transform.position;
    }

    // Update is called once per frame
    void Update() 
    {
        //get relevant transforms values
        float xAbsPosition = Mathf.Abs(_gameObject.transform.localPosition.x - _initialPosition.x); //*** Absolute value, keep it positive
        float yAbsPosition = Mathf.Abs(_gameObject.transform.localPosition.y - _initialPosition.y);
        float zAbsPosition = Mathf.Abs(_gameObject.transform.localPosition.z - _initialPosition.z);
        

        //normalise the xPosition, yPosition, zPosition to usable ranges (0-1)
        //how?
        float xNormPosition = NormalizeValue(xAbsPosition, _initialPosition.x, 10f);
        float yNormPosition = NormalizeValue(yAbsPosition, _initialPosition.y, 10f);
        float zNormPosition = NormalizeValue(zAbsPosition, _initialPosition.z, 10f);

        //float xPosition = Mathf.Clamp01(xNormPosition); //** I have a feeling these lines are redundant
        //float yPosition = Mathf.Clamp01(yNormPosition); //** because of the NormalizeValue function below
        //float zPosition = Mathf.Clamp01(zNormPosition); //** (it already does Mathf.Clamp01)
        float xPosition = xNormPosition; //** I have a feeling these lines are redundant
        float yPosition = yNormPosition; //** because of the NormalizeValue function below
        float zPosition = zNormPosition; //** (it already does Mathf.Clamp01)

        if (_positionXtoVolume) { _modVolumeValue = xPosition; }
        if (_positionYtoVolume) { _modVolumeValue = yPosition; }
        if (_positionZtoVolume) { _modVolumeValue = zPosition; }

        if (_positionXtoPitch) { _modPitchValue = xPosition; }
        if (_positionYtoPitch) { _modPitchValue = yPosition; }
        if (_positionZtoPitch) { _modPitchValue = zPosition; }


        //map the value(s) that was selected to modulate the sound variable(s)
        if (_modulateVolume)
        {
            float volume = Mathf.Lerp(_volumeMin, _volumeMax, _modVolumeValue);
            _audioSource.volume = volume;
        }

        if (_modulatePitch)
        {
            float pitch = Mathf.Lerp(_pitchMin, _pitchMax, _modPitchValue);
            _audioSource.pitch = pitch;
        }
    }
    float NormalizeValue(float value, float minValue, float maxValue)
    {
        return Mathf.Clamp01((value - minValue) / (maxValue - minValue));
    }
}   //*** For some reason, the Volume/Pitch goes past the range of _volumeMax and _pitchMax
    // Still have to find where the calculations are wrong, it has to be calculated within that range
