using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStopGrounds : MonoBehaviour
{
    [SerializeField] private GameObject FirstGround1, FirstGround2, FirstGround3, FirstGround4;

    // Start is called before the first frame update
    void Start()
    {
        FirstGround1.SetActive(true);
        FirstGround2.SetActive(true);
        FirstGround3.SetActive(true);
        FirstGround4.SetActive(true);

        FirstGround1.transform.position = new Vector3(1, 1.5f, 19);
        FirstGround2.transform.position = new Vector3(19, 1.5f, 19);
        FirstGround3.transform.position = new Vector3(1, 1.5f, 1);
        FirstGround4.transform.position = new Vector3(19, 1.5f, 1);
    }

    public void FSG_Delete()
    {
        FirstGround1.SetActive(false);
        FirstGround2.SetActive(false);
        FirstGround3.SetActive(false);
        FirstGround4.SetActive(false);
    }
}
