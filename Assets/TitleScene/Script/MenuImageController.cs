using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuImageController : MonoBehaviour
{
    [SerializeField] private GameObject[] item = new GameObject[3];

    private Vector3 normalScale = new Vector3(1f, 1f, 1f);
    private Vector3 zoomScale = new Vector3(1.5f, 1.5f, 1.5f);

    void Start()
    {
        ScreenUpdate(1);
    }

    public void ScreenUpdate(int menu)
    {
        item[0].transform.localScale = normalScale;
        item[1].transform.localScale = normalScale;
        item[2].transform.localScale = normalScale;

        item[menu].transform.localScale = zoomScale;
    }
}
