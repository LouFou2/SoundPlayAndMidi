using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private MidiInputManager midiInput;

    // the camera will switch between lookat targets, depending on the current key index
    [SerializeField] private GameObject lookAtTarget;
    [SerializeField] private GameObject followTarget;
    [SerializeField] private GameObject[] cameraAim;

    private Vector3 lookTargetPosition = Vector3.zero;
    private Vector3 followTargetPosition = Vector3.zero;

    private int _previousKeyIndex = -1;
    private int _currentKeyIndex = -1;
    // Start is called before the first frame update
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
                        lookTargetPosition = cameraAim[0].transform.position;
                        break;
                    case 1:
                        lookTargetPosition = cameraAim[1].transform.position;
                        break;
                    case 2:
                        lookTargetPosition = cameraAim[2].transform.position;
                        break;
                    case 3:
                        lookTargetPosition = cameraAim[3].transform.position;
                        break;
                    case 4:
                        lookTargetPosition = cameraAim[4].transform.position;
                        break;
                    case 5:
                        lookTargetPosition = cameraAim[5].transform.position;
                        break;
                    case 6:
                        lookTargetPosition = cameraAim[6].transform.position;
                        break;
                    case 7:
                        lookTargetPosition = cameraAim[7].transform.position;
                        break;
                    case 8:
                        lookTargetPosition = cameraAim[8].transform.position;
                        break;
                    case 9:
                        lookTargetPosition = cameraAim[9].transform.position;
                        break;
                    case 10:
                        lookTargetPosition = cameraAim[10].transform.position;
                        break;
                    case 11:
                        lookTargetPosition = cameraAim[11].transform.position;
                        break;
                    case 12:
                        lookTargetPosition = cameraAim[12].transform.position;
                        break;
                    case 13:
                        lookTargetPosition = cameraAim[13].transform.position;
                        break;
                    case 14:
                        lookTargetPosition = cameraAim[14].transform.position;
                        break;
                }
            }
            
        }
        if (_currentKeyIndex != -1 && _currentKeyIndex != _previousKeyIndex) 
        {
            StartCoroutine(SwitchCameraTarget());
        }
        _previousKeyIndex = _currentKeyIndex;
    }

    private IEnumerator SwitchCameraTarget() 
    {
        if (lookAtTarget == null || followTarget == null) 
        { yield break; }
        else 
        {
            //move the lookat target to the current index lookat position, using an eased lerp
            while (lookAtTarget.transform.position != lookTargetPosition) 
            {
                float currentLookAtX = lookAtTarget.transform.position.x;
                float aimLookatX = lookTargetPosition.x;
                float lerpingLookatX = Mathf.Lerp(currentLookAtX, aimLookatX, Time.deltaTime * 10f);
                lookAtTarget.transform.position = new Vector3(lerpingLookatX, lookAtTarget.transform.position.y, lookAtTarget.transform.position.z);
            }
            
            //move camera position to current index camera position, using eased lerp
        }
    }

}
