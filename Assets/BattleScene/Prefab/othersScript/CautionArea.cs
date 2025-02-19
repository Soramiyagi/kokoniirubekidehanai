using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautionArea : MonoBehaviour
{
    [SerializeField] private GameObject firstArea, secondArea;
    [SerializeField] private GameObject firstNorth, firstWest, firstEast, firstSouth, secondNorth, secondWest, secondEast, secondSouth;

    public void FirstCautionAreaReady()
    {
        firstArea.SetActive(true);
    }

    public void SecondCautionAreaReady()
    {
        secondArea.SetActive(true);
    }

    public void FirstCautionAreaON()
    {
        firstNorth.GetComponent<BoxCollider>().enabled = true;
        firstWest.GetComponent<BoxCollider>().enabled = true;
        firstEast.GetComponent<BoxCollider>().enabled = true;
        firstSouth.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(FirstCautionAreaOFF());
    }

    public void SecondCautionAreaON()
    {
        secondNorth.GetComponent<BoxCollider>().enabled = true;
        secondWest.GetComponent<BoxCollider>().enabled = true;
        secondEast.GetComponent<BoxCollider>().enabled = true;
        secondSouth.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(SecondCautionAreaOFF());
    }

    private IEnumerator FirstCautionAreaOFF()
    {
        yield return new WaitForSeconds(0.1f);
        firstArea.SetActive(false);
    }

    private IEnumerator SecondCautionAreaOFF()
    {
        yield return new WaitForSeconds(0.1f);
        secondArea.SetActive(false);
    }
}
