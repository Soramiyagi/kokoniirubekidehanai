using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleController : MonoBehaviour
{
    public float interval_set;  //入力のインターバル
    public float L_StickDeadzone;
    private float interval;
    private Vector2 MenuSelectInput;
    private bool canInput;

    private InputAction L_Stick;
    private InputAction D_Pad;

    // Start is called before the first frame update
    void Start()
    {
        canInput = true;
        interval = interval_set;
        MenuSelectInput = Vector2.zero;

        L_Stick = GetComponent<PlayerInput>().actions["MenuSelect_LStick"];
        D_Pad = GetComponent<PlayerInput>().actions["MenuSelect_DPad"];
    }

    void Update()
    {
        if (interval > 0)
        {
            interval = interval - Time.deltaTime;
        }

        if (L_Stick.ReadValue<Vector2>() == Vector2.zero || D_Pad.ReadValue<Vector2>() == Vector2.zero)
        {
            MenuSelectInput = Vector2.zero;
        }

        if (L_Stick.ReadValue<Vector2>() != Vector2.zero)
        {
            MenuSelectInput = L_Stick.ReadValue<Vector2>();
        }
        else if (D_Pad.ReadValue<Vector2>() != Vector2.zero)
        {
            MenuSelectInput = D_Pad.ReadValue<Vector2>();
        }

        if (canInput == true && interval <= 0)
        {
            if (MenuSelectInput.x > L_StickDeadzone)
            {
                interval = interval_set;
            }
            else if (MenuSelectInput.x < -L_StickDeadzone)
            {
                interval = interval_set;
            }

        }
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
            //押した時
            Debug.Log("Start");
        }
    }
}