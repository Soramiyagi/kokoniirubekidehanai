using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultController : MonoBehaviour
{
    private bool canInput = false;

    public void InputEnable()
    {
        canInput = true;
    }

    public void OnDecision(InputAction.CallbackContext Decision)
    {
        if (canInput == true)
        {
            if (Decision.started)
            {
                //��������
                Debug.Log("Decision");
            }
        }
    }

    public void OnCancel(InputAction.CallbackContext Cancel)
    {
        if (canInput == true)
        {
            if (Cancel.started)
            {
                //��������
                Debug.Log("Cancel");
            }
        }
    }

    public void OnaStart(InputAction.CallbackContext Start)
    {
        if (Start.started)
        {
            //��������
            Debug.Log("Start");
        }
    }
}