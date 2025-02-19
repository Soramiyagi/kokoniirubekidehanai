using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Status : MonoBehaviour
{
    [SerializeField] private GameObject statusFrame, stock1, stock2, stock3;
    [SerializeField] private GameObject icon1P, icon2P, icon3P, icon4P;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private RectTransform canvasRectTransform;

    private int playerNum;

    public void FirstSet(int playerNum)
    {
        if (canvasRectTransform != null)
        {
            canvasRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            canvasRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            canvasRectTransform.pivot = new Vector2(0.5f, 0.5f);
            canvasRectTransform.sizeDelta = new Vector2(800, 600);
            canvasRectTransform.anchoredPosition = new Vector2(0, 0);
            canvasRectTransform.localScale = new Vector3(1f, 1f);
        }
            

        if (playerNum == 1)
        {
            name.text = "Player1";
            icon1P.SetActive(true);
            statusFrame.transform.position = new Vector3(-310, 140, 0);
        }
        else if (playerNum == 2)
        {
            name.text = "Player2";
            icon2P.SetActive(true);
            statusFrame.transform.position = new Vector3(310, 140, 0);
        }
        else if (playerNum == 3)
        {
            name.text = "Player3";
            icon3P.SetActive(true);
            statusFrame.transform.position = new Vector3(-310, -180, 0);
        }
        else if (playerNum == 4)
        {
            name.text = "Player4";
            icon4P.SetActive(true);
            statusFrame.transform.position = new Vector3(310, -180, 0);
        }
    }

    public void StockMinus(int stock)
    {
        if (stock == 0)
        {
            stock1.SetActive(false);
            stock2.SetActive(false);
            stock3.SetActive(false);
        }
        else if (stock == 1)
        {
            stock1.SetActive(true);
            stock2.SetActive(false);
            stock3.SetActive(false);
        }
        else if (stock == 2)
        {
            stock1.SetActive(true);
            stock2.SetActive(true);
            stock3.SetActive(false);
        }
    }
}
