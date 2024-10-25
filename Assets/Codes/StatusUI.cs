using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI P1_Stock, P2_Stock, P3_Stock, P4_Stock;

    public void StockMinus(int playerNum, int stock)
    {
        if(playerNum == 1)
        {
            P1_Stock.SetText(stock.ToString());
        }
        else if (playerNum == 2)
        {
            P2_Stock.SetText(stock.ToString());
        }
        else if (playerNum == 3)
        {
            P3_Stock.SetText(stock.ToString());
        }
        else if (playerNum == 4)
        {
            P4_Stock.SetText(stock.ToString());
        }
    }
}