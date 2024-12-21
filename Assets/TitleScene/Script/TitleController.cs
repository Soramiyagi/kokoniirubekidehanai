using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleController : MonoBehaviour
{
    [SerializeField] GameObject HowTo, Encyclopedia, LoadCliant_ToCharacterSelect;
    [SerializeField] MenuImageController MIC_Script;
    [SerializeField] HowToPageController HTPC_Script;
    [SerializeField] EncyclopediaPageController EPC_Script;
    
    public float interval_set;  //入力のインターバル
    public float L_StickDeadzone;
    private float interval;
    private Vector2 MenuSelectInput;
    private bool canInput_menu;

    private InputAction L_Stick;
    private InputAction D_Pad;

    //メニュー項目の番号
    //0遊び方　1・開始　2・図鑑
    private int menu = 1;

    //多重読み込み防止
    private bool loadStart = false;

    // Start is called before the first frame update
    void Start()
    {
        canInput_menu = true;
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

        if (canInput_menu == true && interval <= 0)
        {
            if (MenuSelectInput.x > L_StickDeadzone)
            {
                if (menu <= 1)
                {
                    menu++;
                    MIC_Script.ScreenUpdate(menu);
                    interval = interval_set;
                }
            }
            else if (MenuSelectInput.x < -L_StickDeadzone)
            {
                if (menu >= 1)
                {
                    menu--;
                    MIC_Script.ScreenUpdate(menu);
                    interval = interval_set;
                }

            }
        }
        else if(canInput_menu == false && interval <= 0)
        {
            if (menu == 0)
            {
                if (MenuSelectInput.x > L_StickDeadzone)
                {
                    HTPC_Script.NextPage();
                    interval = interval_set;
                }
                else if (MenuSelectInput.x < -L_StickDeadzone)
                {
                    HTPC_Script.BackPage();
                    interval = interval_set;
                }
            }
            else if (menu == 2)
            {
                if (MenuSelectInput.x > L_StickDeadzone)
                {
                    EPC_Script.NextPage();
                    interval = interval_set;
                }
                else if (MenuSelectInput.x < -L_StickDeadzone)
                {
                    EPC_Script.BackPage();
                    interval = interval_set;
                }
            }

        }
    }

    public void OnDecision(InputAction.CallbackContext Decision)
    {
        if (canInput_menu == true)
        {
            if (Decision.started)
            {
                canInput_menu = false;
            }
        }


        if (menu == 0)
        {
            HowTo.SetActive(true);
        }
        else if (menu == 1)
        {
            if (loadStart == false)
            {
                LoadCliant_ToCharacterSelect.GetComponent<LoadClient>().LoadStart();
                loadStart = true;
            }
        }
        else if (menu == 2)
        {
            Encyclopedia.SetActive(true);
        }
    }

    public void OnCancel(InputAction.CallbackContext Cancel)
    {
        if (canInput_menu == false)
        {
            if (Cancel.started)
            {
                canInput_menu = true;
                HowTo.SetActive(false);
                Encyclopedia.SetActive(false);
            }
        }
    }

    public void OnaStart(InputAction.CallbackContext Start)
    {
    }
}