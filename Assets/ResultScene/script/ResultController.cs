using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultController : MonoBehaviour
{
    [SerializeField] GameObject LoadCliant_ToTitle;
    private bool canInput = false;

    //多重読み込み防止
    private bool loadStart = false;

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
                //押した時
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
                //押した時
                Debug.Log("Cancel");
            }
        }
    }

    public void OnaStart(InputAction.CallbackContext Start)
    {
        if (Start.started)
        {
            if (loadStart == false)
            {
                LoadCliant_ToTitle.GetComponent<LoadClient>().LoadStart();
                loadStart = true;
            }
        }
    }
}