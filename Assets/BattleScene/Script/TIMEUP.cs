using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TIMEUP : MonoBehaviour
{
    [SerializeField] private GameObject LeftText, RightText, CenterText;

    // Update is called once per frame
    void Update()
    {
        if (LeftText.transform.localPosition.x < 0)
        {
            LeftText.transform.position = LeftText.transform.position + new Vector3(25, 0, 0);
            RightText.transform.position = RightText.transform.position + new Vector3(-25, 0, 0);
        }
        else
        {
            LeftText.SetActive(false);
            RightText.SetActive(false);
            CenterText.SetActive(true);
        }
    }
}
