using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerText, Text1, Text2, Text3, Text4, WinText, BackText;

    public void ActiveTextImage()
    {
        PlayerText.SetActive(true);

        if (Winner_Save.winnerPlayer == -1 || Winner_Save.winnerPlayer == 0)
        {
            Text1.SetActive(true);
        }
        else if (Winner_Save.winnerPlayer == 1)
        {
            Text2.SetActive(true);
        }
        else if (Winner_Save.winnerPlayer == 2)
        {
            Text3.SetActive(true);
        }
        else if (Winner_Save.winnerPlayer == 3)
        {
            Text4.SetActive(true);
        }

        WinText.SetActive(true);
        BackText.SetActive(true);
    }
}