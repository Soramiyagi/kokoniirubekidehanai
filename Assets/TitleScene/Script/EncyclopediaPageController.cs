using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaPageController : MonoBehaviour
{
    [SerializeField] private GameObject[] pageObj = new GameObject[0];

    private int minPage = 0;
    private int maxPage = 0;
    private int page = 0;

    void Start()
    {
        minPage = 0;
        maxPage = pageObj.Length;
        page = 0;
    }

    void OnEnable()
    {
        page = 0;
        ScreenUpdate(0);
    }

    public void NextPage()
    {
        if (page < maxPage - 1)
        {
            page++;
            ScreenUpdate(page);
        }
    }

    public void BackPage()
    {
        if (page > minPage)
        {
            page--;
            ScreenUpdate(page);
        }
    }

    void ScreenUpdate(int page)
    {
        for (int i = 0; i < maxPage; i++)
        {
            pageObj[i].SetActive(false);
        }

        pageObj[page].SetActive(true);
    }
}
