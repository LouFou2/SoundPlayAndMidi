using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
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
    public bool introSceneFinished = false;
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
        if (keysPressed >= 3 && !textHasStarted && !introSceneFinished) 
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
        yield return new WaitForSeconds(2.0f); 
        textMeshPro.text = "initialising.";
        yield return new WaitForSeconds(0.3f); 
        textMeshPro.text = "initialising..";
        yield return new WaitForSeconds(0.3f); 
        textMeshPro.text = "initialising...";
        yield return new WaitForSeconds(0.3f); 
        textMeshPro.text = "initialising.";
        yield return new WaitForSeconds(0.3f); 
        textMeshPro.text = "initialising..";
        yield return new WaitForSeconds(0.3f); 
        textMeshPro.text = "initialising...";
        yield return new WaitForSeconds(0.3f); // Wait for 2 seconds
        
        textMeshPro.text = "refrequaliser";
        yield return new WaitForSeconds(4.0f);
        
        textMeshPro.text = "scan your body";
        yield return new WaitForSeconds(2.0f);
        textMeshPro.text = "for noise residue from city living";
        yield return new WaitForSeconds(4.0f);
        textMeshPro.text = "re-tune";
        yield return new WaitForSeconds(1.0f);
        textMeshPro.text = "re-balance";
        yield return new WaitForSeconds(1.0f);
        textMeshPro.text = "refrequalise";
        yield return new WaitForSeconds(3.0f);
        introSceneFinished = true;
        yield break;
    }
}
