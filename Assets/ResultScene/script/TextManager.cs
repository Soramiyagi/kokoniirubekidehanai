using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] private GameObject playerText, text1, text2, text3, text4, winText, backText, noWinnerText;

    public void ActiveTextImage()
    {
        if (WinnerSave.winnerPlayer == -1)
        {
            noWinnerText.SetActive(true);

            AudioManager.Instance.PlaySFX("don_se");
        }
        else
        {
            playerText.SetActive(true);

            if (WinnerSave.winnerPlayer == 0)
            {
                text1.SetActive(true);
            }
            else if (WinnerSave.winnerPlayer == 1)
            {
                text2.SetActive(true);
            }
            else if (WinnerSave.winnerPlayer == 2)
            {
                text3.SetActive(true);
            }
            else if (WinnerSave.winnerPlayer == 3)
            {
                text4.SetActive(true);
            }

            winText.SetActive(true);
            AudioManager.Instance.PlaySFX("winner_se");
        }

        backText.SetActive(true);
    }
}