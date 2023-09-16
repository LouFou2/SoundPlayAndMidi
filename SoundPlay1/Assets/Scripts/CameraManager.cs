using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private MidiInputManager midiInput;

    // the camera will switch between lookat targets, depending on the current key index
    [SerializeField] private GameObject lookAtTarget;
    //[SerializeField] private GameObject followTarget; // not using this for this project, but might in other projects
    [SerializeField] private GameObject[] cameraAim;

    private Vector3 newLookTarget = Vector3.zero;
    //private Vector3 newFollowTarget = Vector3.zero;

    private int _previousKeyIndex = -1;
    private int _currentKeyIndex = -1;
    private bool newKeyTriggered = false;
    
    private float time = 0f;

    void Start()
    {
        _previousKeyIndex = -1;
        _currentKeyIndex = -1;
    }

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
                        newLookTarget = cameraAim[0].transform.position;
                        break;
                    case 1:
                        newLookTarget = cameraAim[1].transform.position;
                        break;
                    case 2:
                        newLookTarget = cameraAim[2].transform.position;
                        break;
                    case 3:
                        newLookTarget = cameraAim[3].transform.position;
                        break;
                    case 4:
                        newLookTarget = cameraAim[4].transform.position;
                        break;
                    case 5:
                        newLookTarget = cameraAim[5].transform.position;
                        break;
                    case 6:
                        newLookTarget = cameraAim[6].transform.position;
                        break;
                    case 7:
                        newLookTarget = cameraAim[7].transform.position;
                        break;
                    case 8:
                        newLookTarget = cameraAim[8].transform.position;
                        break;
                    case 9:
                        newLookTarget = cameraAim[9].transform.position;
                        break;
                    case 10:
                        newLookTarget = cameraAim[10].transform.position;
                        break;
                    case 11:
                        newLookTarget = cameraAim[11].transform.position;
                        break;
                    case 12:
                        newLookTarget = cameraAim[12].transform.position;
                        break;
                    case 13:
                        newLookTarget = cameraAim[13].transform.position;
                        break;
                    case 14:
                        newLookTarget = cameraAim[14].transform.position;
                        break;
                }
            }
            
        }
        if (_currentKeyIndex != -1 && _currentKeyIndex != _previousKeyIndex) 
        {
            newKeyTriggered = true;
        }

        if (newKeyTriggered) 
        {
            SwitchCameraTarget();
        }
        _previousKeyIndex = _currentKeyIndex;
    }
    void SwitchCameraTarget()
    {
        Vector3 initialTargetPosition = lookAtTarget.transform.position;
        Vector3 distanceToTarget = newLookTarget - initialTargetPosition;

        if (lookAtTarget == null)
        {
            return;
        }
        else if (lookAtTarget.transform.position != newLookTarget)
        {
            lookAtTarget.transform.position = Vector3.Lerp(initialTargetPosition, newLookTarget, time);
        }
        else if (distanceToTarget.magnitude <= 0.02f)
        {
            lookAtTarget.transform.position = newLookTarget;
            newKeyTriggered = false;
            time = 0f;
        }
        time += Time.deltaTime;
    }

}
