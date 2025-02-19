using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private GameObject skill1Gauge, skill2Gauge;
    private float skill1GaugeMaxValue, skill2GaugeMaxValue;
    private Image gauge1, gauge2;
    private float skill1GaugeValue, skill2GaugeValue;

    void Awake()
    {
        gauge1 = skill1Gauge.GetComponent<Image>();
        gauge2 = skill2Gauge.GetComponent<Image>();
    }

    public void FirstSet(float skill1GaugeMaxValue_set, float skill2GaugeMaxValue_set)
    {
        skill1GaugeValue = skill1GaugeMaxValue_set;
        skill2GaugeValue = skill2GaugeMaxValue_set;
        skill1GaugeMaxValue = skill1GaugeMaxValue_set;
        skill2GaugeMaxValue = skill2GaugeMaxValue_set;
    }

    void FixedUpdate()
    {
        if (skill1GaugeValue < skill1GaugeMaxValue)
        {
            skill1GaugeValue += Time.deltaTime;
        }
        else if (skill1GaugeValue >= skill1GaugeMaxValue)
        {
            skill1GaugeValue = skill1GaugeMaxValue;
        }

        if (skill2GaugeValue < skill2GaugeMaxValue)
        {
            skill2GaugeValue += Time.deltaTime;
        }
        else if (skill2GaugeValue >= skill2GaugeMaxValue)
        {
            skill2GaugeValue = skill2GaugeMaxValue;
        }

        GaugeViewUpdate();
    }

    public void SkillUse(int skillNum)
    {
        if(skillNum == 1)
        {
            skill1GaugeValue = 0;
        }
        else if (skillNum == 2)
        {
            skill2GaugeValue = 0;
        }
    }

    public void SkillGaugeReset()
    {
        skill1GaugeValue = skill1GaugeMaxValue;
        skill2GaugeValue = skill2GaugeMaxValue;
    }

    void GaugeViewUpdate()
    {
        gauge1.fillAmount = skill1GaugeValue / skill1GaugeMaxValue;
        gauge2.fillAmount = skill2GaugeValue / skill2GaugeMaxValue;
    }
}
