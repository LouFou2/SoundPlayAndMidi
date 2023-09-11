using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimateSound : MonoBehaviour
{
    // Audiosource to animate
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private SoundSequencer _soundSequencer;
    [SerializeField] private Animator _animator;

    //AudioClip Trigger
    //private bool _isStillPlaying = false; // Keeps track if the audio is still playing

    //Animation Trigger
    private bool _hasTriggeredAnimation = false;
    private int _previousBeat = -1;

    //Audiosource values to use as input
    [Header("Select One Input")]
    [SerializeField] private bool _useVolumeAsInput = false;
    [SerializeField] private bool _usePitchAsInput = false;
    [SerializeField] private bool _usePanAsInput = false;
    private float _inputVolume;
    private float _inputPitch;
    private float _inputPan;
    private float _volumeValue;
    private float _pitchValue;
    private float _panValue;

    void Start()
    {
        _animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>(); // Find the AudioSource component
        _soundSequencer = GetComponent<SoundSequencer>(); // Find the SoundSequencer script component attached to the same GameObject

        //Get AudioSource variables
        _inputVolume = m_AudioSource.volume;
        _inputPitch = m_AudioSource.pitch;
        _inputPan = m_AudioSource.panStereo;
    }

    // Update is called once per frame
    void Update()
    {
        _inputVolume = m_AudioSource.volume;
        _inputPitch = m_AudioSource.pitch;
        _inputPan = m_AudioSource.panStereo;

        // Update _animTrigger based on desired conditions
        if (_soundSequencer != null)
        {
            int currentBeat = _soundSequencer.clockTimer.beatCount;

            if (currentBeat != _previousBeat)
            {
                _hasTriggeredAnimation = false;
                _previousBeat = currentBeat;
            }

            if (_soundSequencer.playOnBeats[currentBeat])
            {
                if (!_hasTriggeredAnimation)
                {
                    _animator.SetTrigger("AudioPlayTrigger");
                    _hasTriggeredAnimation = true;
                }
            }
        }
        /* 
        if (_isStillPlaying && !m_AudioSource.isPlaying) // Should use the Sound Sequencer instead
        {
            _isStillPlaying = false;
            _animator.ResetTrigger("AudioPlayTrigger"); // Reset the "AudioPlayTrigger" trigger
        }

        if (m_AudioSource.isPlaying && !_isStillPlaying)
        {
            _animator.SetTrigger("AudioPlayTrigger"); // Set the "AudioPlayTrigger" trigger
            _isStillPlaying = true;
        }
        */
        if (_useVolumeAsInput)
        {
            Debug.Log("Volume is Input");
            //map volume input to usable range (0-1)
            _volumeValue = _inputVolume; 
            Debug.Log(_volumeValue);
        }
        if (_usePitchAsInput)
        {
            Debug.Log("Pitch is Input");
            //map pitch input to usable range (0-1)
            _pitchValue = _inputPitch/3;
            Debug.Log(_pitchValue);
        }
        if (_usePanAsInput)
        {
            Debug.Log("Pan is Input");
            //map pan input to usable range (0-1)
            _panValue = _inputPan; //_inputPan is -1 to 1, so needs to be normalised
            Debug.Log(_panValue);
        }

        // set the animater parameter to the value of the input (with mapped range calculations)
        _animator.SetFloat("VolumeVariable", _volumeValue);
        _animator.SetFloat("PitchVariable", _pitchValue);
        _animator.SetFloat("PanVariable", _panValue);
    }
}
