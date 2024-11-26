using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimeText;
    public GameManager gameManager;

    private bool timeStop = true;
    private float time;

    void FixedUpdate()
    {
        if (timeStop == false)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                TimeText.text = time.ToString("F0");
            }
            else if (time <= 0)
            {
                TimeText.text = "0";
                TimeLimit();
            }
        }
    }

    public void TimerStart()
    {
        timeStop = false;
    }

    public void TimerSet(float limitTime_set)
    {
        time = limitTime_set;
    }

    void TimeLimit()
    {
        gameManager.Finish();
    }
}