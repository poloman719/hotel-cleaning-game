using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public List<GameObject> hourTimes;
    public List<GameObject> minuteTimes;
    private TimeManager timeManager;

    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    void Update()
    {
        hourChange();
        minChange();
    }

    void hourChange()
    {
        if (timeManager.hoursPassed == 0)
        {
            hourTimes[timeManager.hoursPassed].SetActive(true);
            for (int i = 1; i < hourTimes.Count; i++)
            {
                hourTimes[i].SetActive(false);
            }
            return;
        }

        hourTimes[timeManager.hoursPassed - 1].SetActive(false);

        if (timeManager.hoursPassed / hourTimes.Count == 1)
            timeManager.hoursPassed = 0;

        hourTimes[timeManager.hoursPassed].SetActive(true);
    }

    void minChange()
    {
        if (timeManager.minPassed == 0)
        {
            minuteTimes[timeManager.minPassed].SetActive(true);
            for (int i = 1; i < minuteTimes.Count; i++)
            {
                minuteTimes[i].SetActive(false);
            }
            return;
        }

        minuteTimes[timeManager.minPassed - 1].SetActive(false);

        if (timeManager.minPassed / minuteTimes.Count == 1)
            timeManager.minPassed = 0;

        minuteTimes[timeManager.minPassed].SetActive(true);
    }
}
