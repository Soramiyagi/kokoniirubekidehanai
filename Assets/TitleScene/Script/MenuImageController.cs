using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuImageController : MonoBehaviour
{
    [SerializeField] private GameObject[] Item = new GameObject[3];

    private Vector3 NormalScale = new Vector3(1f, 1f, 1f);
    private Vector3 ZoomScale = new Vector3(1.5f, 1.5f, 1.5f);

    void Start()
    {
        ScreenUpdate(1);
    }

    public void ScreenUpdate(int menu)
    {
        Item[0].transform.localScale = NormalScale;
        Item[1].transform.localScale = NormalScale;
        Item[2].transform.localScale = NormalScale;

        Item[menu].transform.localScale = ZoomScale;
    }
}
