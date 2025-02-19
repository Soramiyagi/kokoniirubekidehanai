using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeUp : MonoBehaviour
{
    [SerializeField] private GameObject leftText, rightText, centerText;

    // Update is called once per frame
    void Update()
    {
        if (leftText.transform.localPosition.x < 0)
        {
            leftText.transform.position = leftText.transform.position + new Vector3(25, 0, 0);
            rightText.transform.position = rightText.transform.position + new Vector3(-25, 0, 0);
        }
        else
        {
            leftText.SetActive(false);
            rightText.SetActive(false);
            centerText.SetActive(true);
        }
    }
}
