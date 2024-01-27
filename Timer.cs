using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float roundLength = 60f;
    public float targetTime = 0f;
    public bool timerRunning = false;
    public GameObject timerUI;
    public float totalScore = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            targetTime -= Time.deltaTime;

            if (targetTime <= 0.0f)
            {
                stopTimer();
                // We prob should have a way of triggering the end round screen 
            }

            timerUI.GetComponent<TextMeshProUGUI>().text = targetTime.ToString("#0.00");
        }

    }

    public void startTimer()
    {
        targetTime = roundLength;
        timerRunning = true;
    }

    public void pauseTimer()
    {
        // I dont think we need this if we do unity controlling but a way of pausing the timer would be nice.
    }

    public void stopTimer()
    {
        timerRunning = false;
        float taken = timeTaken();
        Debug.Log("Time Taken:" + taken + " Total Score: " + totalScore);
        totalScore += taken;
    }

    internal float timeTaken()
    {
        // returns the diffrent between time left and round length
        return roundLength - targetTime;
    }

    public float playerScore() { 
        return totalScore; 
    }
}
