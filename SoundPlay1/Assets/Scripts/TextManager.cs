using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField] private MidiInputManager midiInputManager;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private int midiKeyPreviousIndex = -1;
    private int midiKeyCurrentIndex = -1;

    private int keysPressed = -1;

    private bool textHasStarted = false;
    private bool textHasFinished = false;
    void Start()
    {
        keysPressed = -1;
        midiKeyPreviousIndex = -1;
        midiKeyCurrentIndex = -1;
    }

    void Update()
    {
        // Check if keys have been pressed 
        if (midiInputManager != null)
        {
            midiKeyCurrentIndex = midiInputManager.currentKeyIndex;
            Debug.Log("Current Key: " + midiKeyCurrentIndex + " , Previous Key: " + midiKeyPreviousIndex);
            if (midiKeyCurrentIndex != -1 && midiKeyCurrentIndex != midiKeyPreviousIndex) 
            {
                keysPressed += 1;
                Debug.Log("Keys Pressed " + (keysPressed+1) + " times");
            }
        }
        // When at least 3 keys have been pressed, start the text display coroutine
        if (keysPressed >= 3 && !textHasStarted && !textHasFinished) 
        {
            StartCoroutine(TextDisplayCoroutine());
            //Debug.Log("Scene can start");
        }
        midiKeyPreviousIndex = midiKeyCurrentIndex;
    }

    private IEnumerator TextDisplayCoroutine()
    {
        textHasStarted = true;
        // Here, you can define the text and delays as needed
        yield return new WaitForSeconds(2.0f); // Wait for 1 second
        textMeshPro.text = "Line 1 of text";

        yield return new WaitForSeconds(2.0f); // Wait for 2 seconds
        textMeshPro.text = "Line 2 of text";

        // Add more text changes and delays as needed

        yield return new WaitForSeconds(2.0f);
        textMeshPro.text = "last line of text";
        textHasFinished = true;
        yield break;
    }
}
