using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerScript : MonoBehaviour
{
    [SerializeField] private MidiInputManager midiInputManager; // assign in inspector
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource; // assign in inspector

    private int previousKeyIndex = -1;
    private int currentKeyIndex = -1; // Initialize to an invalid value
    void Start()
    {
        animator = GetComponent<Animator>();
        previousKeyIndex = -1;
        currentKeyIndex = -1;
    }
    void Update()
    {
        if (midiInputManager._keyC3_Value > 0.05) { currentKeyIndex = 0; }
        else if (midiInputManager._keyD3_Value > 0.05) { currentKeyIndex = 1; }
        else if (midiInputManager._keyE3_Value > 0.05) { currentKeyIndex = 2; }
        else if (midiInputManager._keyF3_Value > 0.05) { currentKeyIndex = 3; }
        else if (midiInputManager._keyG3_Value > 0.05) { currentKeyIndex = 4; }
        else if (midiInputManager._keyA3_Value > 0.05) { currentKeyIndex = 5; }
        else if (midiInputManager._keyB3_Value > 0.05) { currentKeyIndex = 6; }
        else if (midiInputManager._keyC4_Value > 0.05) { currentKeyIndex = 7; }
        else if (midiInputManager._keyD4_Value > 0.05) { currentKeyIndex = 8; }
        else if (midiInputManager._keyE4_Value > 0.05) { currentKeyIndex = 9; }
        else if (midiInputManager._keyF4_Value > 0.05) { currentKeyIndex = 10; }
        else if (midiInputManager._keyG4_Value > 0.05) { currentKeyIndex = 11; }
        else if (midiInputManager._keyA4_Value > 0.05) { currentKeyIndex = 12; }
        else if (midiInputManager._keyB4_Value > 0.05) { currentKeyIndex = 13; }
        else if (midiInputManager._keyC5_Value > 0.05) { currentKeyIndex = 14; }

        if (currentKeyIndex != previousKeyIndex && currentKeyIndex != -1) 
        { 
            AudioPlay();
        }

        // Handle setting "FadeIn" and "FadeOut" triggers
        for (int i = 0; i < 15; i++) // Assuming you have 15 keys
        {
            // Set "FadeIn" trigger for the current key
            if (i == currentKeyIndex)
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
            // Set "FadeOut" trigger for keys other than the current key // *** MAY OR MAY NOT USE THIS
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
            previousKeyIndex = currentKeyIndex;
        }
    }

    void AudioPlay() 
    {
        if (audioSource != null) { audioSource.Play(); }
    }
}
