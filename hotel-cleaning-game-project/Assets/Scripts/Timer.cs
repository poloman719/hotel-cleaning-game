using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("HourTimes")]
    public List<GameObject> hourTimes;
    [SerializeField] private float hourBreaks;
    public float hTracker;
    [SerializeField] private int hoursPassed = 0;

    [Header("MinuteTimes")]
    public List<GameObject> minuteTimes;
    [SerializeField] private float minuteBreaks;
    float minTracker;
    [SerializeField] private int minPassed = 0;

    // Max Time; Ideal is increment of 15 mins
    public float maxTime;
    // Current Time
    [SerializeField] private float timeLeft = 0;
    // Time Start
    private bool timerStart = true;

    // Timer (for seconds tracking or whatever you wanna call it)
    // private float timerSec = 0;

    // Loss
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
            //timerSec += Time.deltaTime;
            timeLeft -= Time.deltaTime;
        }

        /*if (timerSec > 1)
        {
            timerSec -= 1;
            Debug.Log(Math.Floor(timeLeft));
        }*/

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

            ///Set next hour in list
            hourTimes[hoursPassed - 1].SetActive(false);
            hourTimes[hoursPassed].SetActive(true);
        }

        if (minTracker >= minuteBreaks)
        {
            minTracker = 0f;

            ///Set next minute in list
            minPassed++;
            minuteTimes[minPassed - 1].SetActive(false);

            if (minPassed / 6 == 1)
                minPassed = 0;

            minuteTimes[minPassed].SetActive(true);
        }
    }


    [ContextMenu("Start Timer")]
    void StartTimer()
    {
        gameOver = false;
        timeLeft = maxTime;
        timerStart = true;
        Debug.Log("Timer Started");
    }
}
