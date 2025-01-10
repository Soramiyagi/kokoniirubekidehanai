using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautionArea : MonoBehaviour
{
    [SerializeField] private GameObject FirstArea, SecondArea;
    [SerializeField] private GameObject F_N, F_W, F_E, F_S, S_N, S_W, S_E, S_S;

    public void FirstCautionAreaReady()
    {
        FirstArea.SetActive(true);
    }

    public void SecondCautionAreaReady()
    {
        SecondArea.SetActive(true);
    }

    public void FirstCautionAreaON()
    {
        F_N.GetComponent<BoxCollider>().enabled = true;
        F_W.GetComponent<BoxCollider>().enabled = true;
        F_E.GetComponent<BoxCollider>().enabled = true;
        F_S.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(FirstCautionAreaOFF());
    }

    public void SecondCautionAreaON()
    {
        S_N.GetComponent<BoxCollider>().enabled = true;
        S_W.GetComponent<BoxCollider>().enabled = true;
        S_E.GetComponent<BoxCollider>().enabled = true;
        S_S.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(SecondCautionAreaOFF());
    }

    private IEnumerator FirstCautionAreaOFF()
    {
        yield return new WaitForSeconds(0.1f);
        FirstArea.SetActive(false);
    }

    private IEnumerator SecondCautionAreaOFF()
    {
        yield return new WaitForSeconds(0.1f);
        SecondArea.SetActive(false);
    }
}
