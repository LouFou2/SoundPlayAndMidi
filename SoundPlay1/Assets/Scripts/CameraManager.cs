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

    private Vector3 newLookTarget = Vector3.zero;
    private Vector3 newFollowTarget = Vector3.zero;

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
            StartCoroutine(SwitchCameraTarget());
        }
        _previousKeyIndex = _currentKeyIndex;
    }

    private IEnumerator SwitchCameraTarget() 
    {
        if (lookAtTarget == null) 
        { yield break; }

        float duration = 0.5f; // Adjust the duration as needed
        float elapsedTime = 0f;
        Vector3 initialLookAtPosition = lookAtTarget.transform.position;

        while (elapsedTime < duration)
        {
            // Interpolate the lookAtTarget position smoothly
            float t = elapsedTime / duration;
            lookAtTarget.transform.position = Vector3.Lerp(initialLookAtPosition, newLookTarget, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position is exactly the target position
        lookAtTarget.transform.position = newLookTarget;
    }

}
