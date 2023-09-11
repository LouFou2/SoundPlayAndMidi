using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTimer : MonoBehaviour
{
    private float time;
    public float clockSpeed = 10f;
    public int beatCount = 0;
    public int beatsInBar = 4;
    public int barCount = 0;
    public int barsInLine = 4;
    public int lineCount = 0;
    public int linesInLoop = 4;

    void Update()
    {
        time += Time.deltaTime * clockSpeed;

        if (time >= 1) 
        {
            beatCount += 1;
            time = 0f; // Reset time
        }

        if (beatCount == beatsInBar)
        {
            beatCount = 0;
            barCount += 1;
        }

        if (barCount == barsInLine)
        {
            barCount = 0;
            lineCount += 1;
        }


        if (lineCount == linesInLoop)
        {
            lineCount = 0;
        }

        Debug.Log("Beat: " + beatCount + ", Bar: " + barCount + ", Line: " + lineCount);
    }
}
