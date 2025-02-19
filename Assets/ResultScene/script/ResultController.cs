using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultController : MonoBehaviour
{
    [SerializeField] GameObject loadCliantToTitle;
    private bool canInput = false;

    //ëΩèdì«Ç›çûÇ›ñhé~
    private bool loadStart = false;

    public void InputEnable()
    {
        canInput = true;
    }

    public void OnStart(InputAction.CallbackContext Start)
    {
        if (Start.started)
        {
            if (loadStart == false)
            {
                loadCliantToTitle.GetComponent<LoadClient>().LoadStart();
                loadStart = true;
            }
        }
    }
}