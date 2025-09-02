using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float hourBreaks;
    private float minuteBreaks;
    public int hoursPassed = 0;
    public int minPassed = 0;
    float hTracker;
    float minTracker;

    //Ideal is increment of 15 mins
    public float maxTime;

    private float timeLeft = 0;
    private bool timerStart = true;
    public bool gameOver = false;

    void Start()
    {
        timeLeft = maxTime;

        ///9-5 job, so 8 hours
        hourBreaks = maxTime / 8f;

        ///alarm clock increments by 10 mins
        minuteBreaks = maxTime / 48f;
    }

    void Update()
    {
        hTracker += Time.deltaTime;
        minTracker += Time.deltaTime;

        CalculateDisplay();

        if (timerStart)
        {
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft <= 0)
        {
            timerStart = false;
            gameOver = true;
            Debug.Log("You lost lmao");
        }
    }

    void CalculateDisplay()
    {
        if (hTracker >= hourBreaks)
        {
            hTracker = 0f;
            hoursPassed++;
        }

        if (minTracker >= minuteBreaks)
        {
            minTracker = 0f;
            minPassed++;
        }
    }

    /*
    [ContextMenu("Start Timer")]
    void StartTimer()
    {
        gameOver = false;
        timeLeft = maxTime;
        timerStart = true;
        Debug.Log("Timer Started");
    }
    */
}
