using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStopGrounds : MonoBehaviour
{
    [SerializeField] private GameObject firstGround1, firstGround2, firstGround3, firstGround4;
    [SerializeField] private GameObject systemStopBreak1, systemStopBreak2, systemStopBreak3, systemStopBreak4;

    // Start is called before the first frame update
    void Start()
    {
        firstGround1.SetActive(true);
        firstGround2.SetActive(true);
        firstGround3.SetActive(true);
        firstGround4.SetActive(true);

        firstGround1.transform.position = new Vector3(1, 1.5f, 19);
        firstGround2.transform.position = new Vector3(19, 1.5f, 19);
        firstGround3.transform.position = new Vector3(1, 1.5f, 1);
        firstGround4.transform.position = new Vector3(19, 1.5f, 1);

        systemStopBreak1.transform.position = new Vector3(1, 2.5f, 19);
        systemStopBreak2.transform.position = new Vector3(19, 2.5f, 19);
        systemStopBreak3.transform.position = new Vector3(1, 2.5f, 1);
        systemStopBreak4.transform.position = new Vector3(19, 2.5f, 1);
    }

    public void FSG_Delete()
    {
        systemStopBreak1.SetActive(true);
        systemStopBreak2.SetActive(true);
        systemStopBreak3.SetActive(true);
        systemStopBreak4.SetActive(true);

        firstGround1.SetActive(false);
        firstGround2.SetActive(false);
        firstGround3.SetActive(false);
        firstGround4.SetActive(false);

        StartCoroutine(WaitAndDelete());
    }

    private IEnumerator WaitAndDelete()
    { 
        yield return new WaitForSeconds(0.1f);
        SSB_Delete();
    }

        private void SSB_Delete()
    {
        systemStopBreak1.SetActive(false);
        systemStopBreak2.SetActive(false);
        systemStopBreak3.SetActive(false);
        systemStopBreak4.SetActive(false);
    }
}
