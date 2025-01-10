using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMESET : MonoBehaviour
{
    [SerializeField] private GameObject textObj;

    void OnEnable()
    {
        textObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (textObj.transform.localScale.x < 1f)
        {
            textObj.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
            textObj.transform.Rotate(Vector3.forward, 30, Space.Self);
        }
        else
        {
            textObj.transform.localEulerAngles = new Vector3(0, 0, 15);
        }
    }
}