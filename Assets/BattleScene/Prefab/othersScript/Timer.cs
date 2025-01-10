using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject TimerObj;
    [SerializeField] private TextMeshProUGUI TimeText;
    [SerializeField] private float firstCautionReadyTime, secondCautionReadyTime, firstCautionOnTime, secondCautionOnTime;
    public GameManager gameManager;

    private bool timeStop = true;
    private float time;

    private bool firstCautionReady = true;
    private bool secondCautionReady = true;
    private bool firstCautionOn = true;
    private bool secondCautionOn = true;

    void FixedUpdate()
    {
        if (timeStop == false)
        {
            if(TimerObj.activeSelf == false);
            {
                TimerObj.SetActive(true);
            }
            
            if (time > 0)
            {
                time -= Time.deltaTime;
                TimeText.text = time.ToString("F0");

                if(time < firstCautionReadyTime && firstCautionReady == true)
                {
                    gameManager.FirstCautionAreaReady();
                    firstCautionReady = false;
                }

                if (time < firstCautionOnTime && firstCautionOn == true)
                {
                    gameManager.FirstCautionAreaON();
                    firstCautionOn = false;
                }

                if (time < secondCautionReadyTime && secondCautionReady == true)
                {
                    gameManager.SecondCautionAreaReady();
                    secondCautionReady = false;
                }

                if (time < secondCautionOnTime && secondCautionOn == true)
                {
                    gameManager.SecondCautionAreaON();
                    secondCautionOn = false;
                }
            }
            else if (time <= 0)
            {
                TimeText.text = "0";
                TimeLimit();
            }
        }
        else
        {
            TimerObj.SetActive(false);
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

    public void TimerHide()
    {
        TimerObj.SetActive(false);
    }

    void TimeLimit()
    {
        gameManager.TimeOver();
    }
}