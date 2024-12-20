using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Status : MonoBehaviour
{
    [SerializeField] private GameObject StatusFrame, Stock1, Stock2, Stock3;
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
            StatusFrame.transform.position = new Vector3(-310, 140, 0);
        }
        else if (playerNum == 2)
        {
            name.text = "Player2";
            StatusFrame.transform.position = new Vector3(310, 140, 0);
        }
        else if (playerNum == 3)
        {
            name.text = "Player3";
            StatusFrame.transform.position = new Vector3(-310, -180, 0);
        }
        else if (playerNum == 4)
        {
            name.text = "Player4";
            StatusFrame.transform.position = new Vector3(310, -180, 0);
        }
    }

    public void StockMinus(int stock)
    {
        if (stock == 0)
        {
            Stock1.SetActive(false);
            Stock2.SetActive(false);
            Stock3.SetActive(false);
        }
        else if (stock == 1)
        {
            Stock1.SetActive(true);
            Stock2.SetActive(false);
            Stock3.SetActive(false);
        }
        else if (stock == 2)
        {
            Stock1.SetActive(true);
            Stock2.SetActive(true);
            Stock3.SetActive(false);
        }
    }
}
