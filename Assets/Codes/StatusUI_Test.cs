using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusUI_Test : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI P1_Stock, P2_Stock, P3_Stock, P4_Stock;
    [SerializeField] private TextMeshProUGUI P1_HP, P2_HP, P3_HP, P4_HP;

    public void StockMinus(int playerNum, int stock, int HP)
    {
        if(playerNum == 1)
        {
            P1_Stock.SetText(stock.ToString());
            P1_HP.SetText(HP.ToString());
        }
        else if (playerNum == 2)
        {
            P2_Stock.SetText(stock.ToString());
            P2_HP.SetText(HP.ToString());
        }
        else if (playerNum == 3)
        {
            P3_Stock.SetText(stock.ToString());
            P3_HP.SetText(HP.ToString());
        }
        else if (playerNum == 4)
        {
            P4_Stock.SetText(stock.ToString());
            P4_HP.SetText(HP.ToString());
        }
    }

    public void HPMinus(int playerNum, int HP)
    {
        if (playerNum == 1)
        {
            P1_HP.SetText(HP.ToString());
        }
        else if (playerNum == 2)
        {
            P2_HP.SetText(HP.ToString());
        }
        else if (playerNum == 3)
        {
            P3_HP.SetText(HP.ToString());
        }
        else if (playerNum == 4)
        {
            P4_HP.SetText(HP.ToString());
        }
    }
}