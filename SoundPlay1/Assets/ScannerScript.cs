using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerScript : MonoBehaviour
{
    [SerializeField] private MidiInputManager midiInputManager; // assign in inspector
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject scanLightC3;
    [SerializeField] private GameObject scanLightD3;
    [SerializeField] private GameObject scanLightE3;
    [SerializeField] private GameObject scanLightF3;
    [SerializeField] private GameObject scanLightG3;
    [SerializeField] private GameObject scanLightA3;
    [SerializeField] private GameObject scanLightB3;
    [SerializeField] private GameObject scanLightC4;
    [SerializeField] private GameObject scanLightD4;
    [SerializeField] private GameObject scanLightE4;
    [SerializeField] private GameObject scanLightF4;
    [SerializeField] private GameObject scanLightG4;
    [SerializeField] private GameObject scanLightA4;
    [SerializeField] private GameObject scanLightB4;
    [SerializeField] private GameObject scanLightC5;

    private int newKeyIndex = -1; // Initialize to an invalid value
    void Start()
    {
        animator = GetComponent<Animator>();
        //== Find all the gameObjects (they are children of the object with the script on it) ==//
        scanLightC3 = transform.Find("ScanlightC3").gameObject;
        scanLightD3 = transform.Find("ScanlightD3").gameObject;
        scanLightE3 = transform.Find("ScanlightE3").gameObject;
        scanLightF3 = transform.Find("ScanlightF3").gameObject;
        scanLightG3 = transform.Find("ScanlightG3").gameObject;
        scanLightA3 = transform.Find("ScanlightA3").gameObject;
        scanLightB3 = transform.Find("ScanlightB3").gameObject;
        scanLightC4 = transform.Find("ScanlightC4").gameObject;
        scanLightD4 = transform.Find("ScanlightD4").gameObject;
        scanLightE4 = transform.Find("ScanlightE4").gameObject;
        scanLightF4 = transform.Find("ScanlightF4").gameObject;
        scanLightG4 = transform.Find("ScanlightG4").gameObject;
        scanLightA4 = transform.Find("ScanlightA4").gameObject;
        scanLightB4 = transform.Find("ScanlightB4").gameObject;
        scanLightC5 = transform.Find("ScanlightC5").gameObject;

        //== Set all Scanlight bars x scale to 0
        Vector3 zeroScale = new Vector3(0f, scanLightC3.transform.localScale.y, scanLightC3.transform.localScale.z);
        scanLightC3.transform.localScale = zeroScale;
        scanLightD3.transform.localScale = zeroScale;
        scanLightE3.transform.localScale = zeroScale;
        scanLightF3.transform.localScale = zeroScale;
        scanLightG3.transform.localScale = zeroScale;
        scanLightA3.transform.localScale = zeroScale;
        scanLightB3.transform.localScale = zeroScale;
        scanLightC4.transform.localScale = zeroScale;
        scanLightD4.transform.localScale = zeroScale;
        scanLightE4.transform.localScale = zeroScale;
        scanLightF4.transform.localScale = zeroScale;
        scanLightG4.transform.localScale = zeroScale;
        scanLightA4.transform.localScale = zeroScale;
        scanLightB4.transform.localScale = zeroScale;
        scanLightC5.transform.localScale = zeroScale;
    }
    void Update()
    {
        // Determine the current key index based on MIDI input
        //int newKeyIndex = -1; // Initialize to an invalid value

        if (midiInputManager._keyC3_Value > 0.05) { newKeyIndex = 0; }
        else if (midiInputManager._keyD3_Value > 0.05) { newKeyIndex = 1; }
        else if (midiInputManager._keyE3_Value > 0.05) { newKeyIndex = 2; }
        else if (midiInputManager._keyF3_Value > 0.05) { newKeyIndex = 3; }
        else if (midiInputManager._keyG3_Value > 0.05) { newKeyIndex = 4; }
        else if (midiInputManager._keyA3_Value > 0.05) { newKeyIndex = 5; }
        else if (midiInputManager._keyB3_Value > 0.05) { newKeyIndex = 6; }
        else if (midiInputManager._keyC4_Value > 0.05) { newKeyIndex = 7; }
        else if (midiInputManager._keyD4_Value > 0.05) { newKeyIndex = 8; }
        else if (midiInputManager._keyE4_Value > 0.05) { newKeyIndex = 9; }
        else if (midiInputManager._keyF4_Value > 0.05) { newKeyIndex = 10; }
        else if (midiInputManager._keyG4_Value > 0.05) { newKeyIndex = 11; }
        else if (midiInputManager._keyA4_Value > 0.05) { newKeyIndex = 12; }
        else if (midiInputManager._keyB4_Value > 0.05) { newKeyIndex = 13; }
        else if (midiInputManager._keyC5_Value > 0.05) { newKeyIndex = 14; }

        // Handle setting "FadeIn" and "FadeOut" triggers
        for (int i = 0; i < 15; i++) // Assuming you have 15 keys
        {
            // Set "FadeIn" trigger for the current key
            if (i == newKeyIndex)
            {
                switch (i)
                {
                    case 0:
                        animator.SetTrigger("FadeInC3");
                        break;
                    case 1:
                        animator.SetTrigger("FadeInD3");
                        break;
                    case 2:
                        animator.SetTrigger("FadeInE3");
                        break;
                    case 3:
                        animator.SetTrigger("FadeInF3");
                        break;
                    case 4:
                        animator.SetTrigger("FadeInG3");
                        break;
                    case 5:
                        animator.SetTrigger("FadeInA3");
                        break;
                    case 6:
                        animator.SetTrigger("FadeInB3");
                        break;
                    case 7:
                        animator.SetTrigger("FadeInC4");
                        break;
                    case 8:
                        animator.SetTrigger("FadeInD4");
                        break;
                    case 9:
                        animator.SetTrigger("FadeInE4");
                        break;
                    case 10:
                        animator.SetTrigger("FadeInF4");
                        break;
                    case 11:
                        animator.SetTrigger("FadeInG4");
                        break;
                    case 12:
                        animator.SetTrigger("FadeInA4");
                        break;
                    case 13:
                        animator.SetTrigger("FadeInB4");
                        break;
                    case 14:
                        animator.SetTrigger("FadeInC5");
                        break;
                }
            }
            // Set "FadeOut" trigger for keys other than the current key
            /*else
            {
                switch (i)
                {
                    case 0:
                        animator.SetTrigger("FadeOutC3");
                        break;
                    case 1:
                        animator.SetTrigger("FadeOutD3");
                        break;
                    case 2:
                        animator.SetTrigger("FadeOutE3");
                        break;
                    case 3:
                        animator.SetTrigger("FadeOutF3");
                        break;
                    case 4:
                        animator.SetTrigger("FadeOutG3");
                        break;
                    case 5:
                        animator.SetTrigger("FadeOutA3");
                        break;
                    case 6:
                        animator.SetTrigger("FadeOutB3");
                        break;
                    case 7:
                        animator.SetTrigger("FadeOutC4");
                        break;
                    case 8:
                        animator.SetTrigger("FadeOutD4");
                        break;
                    case 9:
                        animator.SetTrigger("FadeOutE4");
                        break;
                    case 10:
                        animator.SetTrigger("FadeOutF4");
                        break;
                    case 11:
                        animator.SetTrigger("FadeOutG4");
                        break;
                    case 12:
                        animator.SetTrigger("FadeOutA4");
                        break;
                    case 13:
                        animator.SetTrigger("FadeOutB4");
                        break;
                    case 14:
                        animator.SetTrigger("FadeOutC5");
                        break;
                }
            }*/
        }

    }


    /*void Update()
    {
        //float scanLightAimScaleX = 0.09f;
        if (midiInputManager._keyC3_Value > 0.05 && currentKeyIndex != 0) { currentKeyIndex = 0; }
        else if (midiInputManager._keyD3_Value > 0.05 && currentKeyIndex != 1) { currentKeyIndex = 1; }
        else if (midiInputManager._keyE3_Value > 0.05 && currentKeyIndex != 2) { currentKeyIndex = 2; }
        else if (midiInputManager._keyF3_Value > 0.05 && currentKeyIndex != 3) { currentKeyIndex = 3; }
        else if (midiInputManager._keyG3_Value > 0.05 && currentKeyIndex != 4) { currentKeyIndex = 4; }
        else if (midiInputManager._keyA3_Value > 0.05 && currentKeyIndex != 5) { currentKeyIndex = 5; }
        else if (midiInputManager._keyB3_Value > 0.05 && currentKeyIndex != 6) { currentKeyIndex = 6; }
        else if (midiInputManager._keyC4_Value > 0.05 && currentKeyIndex != 7) { currentKeyIndex = 7; }
        else if (midiInputManager._keyD4_Value > 0.05 && currentKeyIndex != 8) { currentKeyIndex = 8; }
        else if (midiInputManager._keyE4_Value > 0.05 && currentKeyIndex != 9) { currentKeyIndex = 9; }
        else if (midiInputManager._keyF4_Value > 0.05 && currentKeyIndex != 10) { currentKeyIndex = 10; }
        else if (midiInputManager._keyG4_Value > 0.05 && currentKeyIndex != 11) { currentKeyIndex = 11; }
        else if (midiInputManager._keyA4_Value > 0.05 && currentKeyIndex != 12) { currentKeyIndex = 12; }
        else if (midiInputManager._keyB4_Value > 0.05 && currentKeyIndex != 13) { currentKeyIndex = 13; }
        else if (midiInputManager._keyC5_Value > 0.05 && currentKeyIndex != 14) { currentKeyIndex = 14; }
        
        if(currentKeyIndex == 0) { animator.SetTrigger("FadeInC3"); }
        else if (currentKeyIndex != 0) { animator.SetTrigger("FadeOutC3"); }
        if(currentKeyIndex == 1) { animator.SetTrigger("FadeInD3"); }
        else if (currentKeyIndex != 1) { animator.SetTrigger("FadeOutD3"); }
        

    }
    private GameObject GetScanLightGameObject(int index)
    {
        switch (index)
        {
            case 0: return scanLightC3;
            case 1: return scanLightD3;
            case 2: return scanLightE3;
            case 3: return scanLightF3;
            case 4: return scanLightG3;
            case 5: return scanLightA3;
            case 6: return scanLightB3;
            case 7: return scanLightC4;
            case 8: return scanLightD4;
            case 9: return scanLightE4;
            case 10: return scanLightF4;
            case 11: return scanLightG4;
            case 12: return scanLightA4;
            case 13: return scanLightB4;
            case 14: return scanLightC5;
            default: return null;
        }
    }*/

}
