using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaPageController : MonoBehaviour
{
    [SerializeField] private GameObject[] pageObj = new GameObject[0];
    [SerializeField] private GameObject leftObj, rightObj;

    private int minPage = 0;
    private int maxPage = 0;
    private int page = 0;

    private 

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
        CursolCheck();
    }

    public void NextPage()
    {
        if (page < maxPage - 1)
        {
            page++;
            ScreenUpdate(page);
            AudioManager.Instance.PlaySFX("page_se");
        }
        CursolCheck();
    }

    public void BackPage()
    {
        if (page > minPage)
        {
            page--;
            ScreenUpdate(page);
            AudioManager.Instance.PlaySFX("page_se");
        }
        CursolCheck();
    }

    void ScreenUpdate(int page)
    {
        for (int i = 0; i < maxPage; i++)
        {
            pageObj[i].SetActive(false);
        }

        pageObj[page].SetActive(true);
    }

    void CursolCheck()
    {
        if (page == minPage)
        {
            leftObj.SetActive(false);
            rightObj.SetActive(true);
        }
        else if (page == maxPage - 1)
        {
            leftObj.SetActive(true);
            rightObj.SetActive(false);
        }
        else
        {
            leftObj.SetActive(true);
            rightObj.SetActive(true);
        }
    }
}
