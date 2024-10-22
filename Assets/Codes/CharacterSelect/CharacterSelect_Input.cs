using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterSelect_Input : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI num_text, c_text;

    public int maxNum = 0;  //総キャラクターの数
    public float interval_set;  //入力のインターバル
    public float L_StickDeadzone;

    private int select; //キャラクターの番号を保存
    private bool canInput;
    private float interval;
    private Vector2 MenuSelectInput;

    private InputAction L_Stick;
    private InputAction D_Pad;

    CharacterSelect_InputManager CSI;

    // Start is called before the first frame update
    void Start()
    {
        select = 0;    //-1はキャラ無し
        canInput = true;
        interval = interval_set;
        MenuSelectInput = Vector2.zero;

        c_text.text = "0";

        L_Stick = GetComponent<PlayerInput>().actions["MenuSelect_LStick"];
        D_Pad = GetComponent<PlayerInput>().actions["MenuSelect_DPad"];

        CSI = GameObject.Find("PlayerInputManager").GetComponent<CharacterSelect_InputManager>();

        NameSet();
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
                if (select < maxNum - 1)
                {
                    select = select + 1;
                    interval = interval_set;
                    c_text.SetText(select.ToString());
                }
            }
            else if (MenuSelectInput.x < -L_StickDeadzone)
            {
                if (select > 0)
                {
                    select = select - 1;
                    interval = interval_set;
                    c_text.SetText(select.ToString());
                }
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
                canInput = false;
                CSI.Ready();
            }
        }
    }

    public void OnCancel(InputAction.CallbackContext Cancel)
    {
        if (canInput == false)
        {
            if (Cancel.started)
            {
                //押した時
                canInput = true;
                CSI.Unready();
            }
        }
    }

    void NameSet()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        // 名前が一致するオブジェクトが存在するかチェック

        bool[] exist = new bool[4];

        exist[0] = false;
        exist[1] = false;
        exist[2] = false;
        exist[3] = false;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Player1")
            {
                exist[0] = true;
            }
            else if (obj.name == "Player2")
            {
                exist[1] = true;
            }
            else if (obj.name == "Player3")
            {
                exist[2] = true;
            }
            else if (obj.name == "Player4")
            {
                exist[3] = true;
            }
        }

        if(exist[0] == false)
        {
            num_text.SetText("P1");
            this.name = "Player1";
            CSI.c_text[0] = c_text;
            return;
        }

        if (exist[1] == false)
        {
            num_text.SetText("P2");
            this.name = "Player2";
            CSI.c_text[1] = c_text;
            return;
        }

        if (exist[2] == false)
        {
            num_text.SetText("P3");
            this.name = "Player3";
            CSI.c_text[2] = c_text;
            return;
        }

        if (exist[3] == false)
        {
            num_text.SetText("P4");
            this.name = "Player4";
            CSI.c_text[3] = c_text;
            return;
        }
    }

    public void DeviceLost()
    {
        // 接続が切れた時の処理

        if(canInput == true)
        {
            CSI.LostEvent_A();
        }
        else if (canInput == false)
        {
            CSI.LostEvent_B();
        }

        select = - 1;
        canInput = true;
        c_text.SetText(select.ToString());
    }
}