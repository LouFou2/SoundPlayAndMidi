using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Palmmedia.ReportGenerator.Core.CodeAnalysis;

public class MainScene_UIManager : MonoBehaviour
{
    public TextMeshProUGUI rhythmValue;
    public TextMeshProUGUI volFreqValue;
    public TextMeshProUGUI volAmpValue;
    public TextMeshProUGUI pitchFreqValue;
    public TextMeshProUGUI pitchAmpValue;
    public Image button2Image;

    public Image[] key;
    public Color[] alphaColor;

    void Start()
    {
        rhythmValue.text = "0";
        volFreqValue.text = "0";
        volAmpValue.text = "0";
        pitchFreqValue.text = "0";
        pitchAmpValue.text = "0";

        for(int i = 0; i < key.Length; i++) 
        {
            alphaColor[i] = new Color(1f, 1f, 1f, 0.14f);
        }
    }

    private void Update()
    {
        for (int i = 0; i < key.Length; i++)
        {
            key[i].color = alphaColor[i];
        }
    }

}
