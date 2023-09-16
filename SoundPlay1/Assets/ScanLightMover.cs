using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanLightMover : MonoBehaviour
{
    [SerializeField] private MidiInputManager midiInput;

    // the camera will switch between lookat targets, depending on the current key index
    [SerializeField] private GameObject scanLight;
    [SerializeField] private GameObject[] scanLightAim;
    [SerializeField] private AudioSource audioSource; // assign in inspector

    private Vector3 newAimTarget = Vector3.zero;

    private int _previousKeyIndex = -1;
    private int _currentKeyIndex = -1;
    private bool newKeyTriggered = false;

    private float time = 0f;

    void Start()
    {
        _previousKeyIndex = -1;
        _currentKeyIndex = -1;

        //== Get all the children "CameraAim" gameobjects and populate the indexes of cameraAim ==//
    }

    // Update is called once per frame
    void Update()
    {
        _currentKeyIndex = midiInput.currentKeyIndex;

        for (int i = 0; i < 15; i++) // Assuming you have 15 keys
        {
            // Set "Camera Aim" position for the current key
            if (i == _currentKeyIndex)
            {
                switch (i)
                {
                    case 0:
                        newAimTarget = scanLightAim[0].transform.position;
                        break;
                    case 1:
                        newAimTarget = scanLightAim[1].transform.position;
                        break;
                    case 2:
                        newAimTarget = scanLightAim[2].transform.position;
                        break;
                    case 3:
                        newAimTarget = scanLightAim[3].transform.position;
                        break;
                    case 4:
                        newAimTarget = scanLightAim[4].transform.position;
                        break;
                    case 5:
                        newAimTarget = scanLightAim[5].transform.position;
                        break;
                    case 6:
                        newAimTarget = scanLightAim[6].transform.position;
                        break;
                    case 7:
                        newAimTarget = scanLightAim[7].transform.position;
                        break;
                    case 8:
                        newAimTarget = scanLightAim[8].transform.position;
                        break;
                    case 9:
                        newAimTarget = scanLightAim[9].transform.position;
                        break;
                    case 10:
                        newAimTarget = scanLightAim[10].transform.position;
                        break;
                    case 11:
                        newAimTarget = scanLightAim[11].transform.position;
                        break;
                    case 12:
                        newAimTarget = scanLightAim[12].transform.position;
                        break;
                    case 13:
                        newAimTarget = scanLightAim[13].transform.position;
                        break;
                    case 14:
                        newAimTarget = scanLightAim[14].transform.position;
                        break;
                }
            }

        }
        if (_currentKeyIndex != -1 && _currentKeyIndex != _previousKeyIndex)
        {
            newKeyTriggered = true;
            AudioPlay();
        }

        if (newKeyTriggered) 
        {
            SwitchScanLightTarget();
        }
        _previousKeyIndex = _currentKeyIndex;
    }

    void SwitchScanLightTarget()
    {
        Vector3 initialTargetPosition = scanLight.transform.position;
        Vector3 distanceToTarget = newAimTarget - initialTargetPosition;

        if (scanLight == null) 
        { 
            return; 
        }
        else if ( scanLight.transform.position != newAimTarget )
        { 
            scanLight.transform.position = Vector3.Lerp(initialTargetPosition, newAimTarget, time);
        }
        else if (distanceToTarget.magnitude <= 0.02f) 
        {
            scanLight.transform.position = newAimTarget;
            newKeyTriggered = false;
            time = 0f;
        }
        time += Time.deltaTime;
    }
    
    void AudioPlay()
    {
        if (audioSource != null) { audioSource.Play(); }
    }
}
