using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterSelectInput : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite sprite0, sprite1, sprite2, sprite3, sprite4;
    [SerializeField] private GameObject selectedObj;

    public int maxNum = 0;  //総キャラクターの数
    public float interval_set;  //入力のインターバル
    public float lstickDeadzone;

    private int select; //キャラクターの番号を保存
    private bool canInput;
    private float interval;
    private Vector2 menuSelectInput;

    private InputAction L_Stick;
    private InputAction D_Pad;

    CharacterSelectInputManager csi;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySFX("decision_se");

        select = 0;    //-1はキャラ無し
        canInput = true;
        interval = interval_set;
        menuSelectInput = Vector2.zero;

        L_Stick = GetComponent<PlayerInput>().actions["MenuSelect_LStick"];
        D_Pad = GetComponent<PlayerInput>().actions["MenuSelect_DPad"];

        csi = GameObject.Find("PlayerInputManager").GetComponent<CharacterSelectInputManager>();

        NameSet(); 
        UpdateCharacterImage();
    }

    void Update()
    {
        if (interval > 0)
        {
            interval = interval - Time.deltaTime;
        }

        if (L_Stick.ReadValue<Vector2>() == Vector2.zero || D_Pad.ReadValue<Vector2>() == Vector2.zero)
        {
            menuSelectInput = Vector2.zero;
        }

        if (L_Stick.ReadValue<Vector2>() != Vector2.zero)
        {
            menuSelectInput = L_Stick.ReadValue<Vector2>();
        }
        else if (D_Pad.ReadValue<Vector2>() != Vector2.zero)
        {
            menuSelectInput = D_Pad.ReadValue<Vector2>();
        }

        if (canInput == true && interval <= 0)
        {
            if (menuSelectInput.x > lstickDeadzone)
            {
                if (select < maxNum - 1)
                {
                    select = select + 1;
                    interval = interval_set;
                    UpdateCharacterImage();

                    AudioManager.Instance.PlaySFX("page_se");
                }
            }
            else if (menuSelectInput.x < -lstickDeadzone)
            {
                if (select > 0)
                {
                    select = select - 1;
                    interval = interval_set;
                    UpdateCharacterImage();

                    AudioManager.Instance.PlaySFX("page_se");
                }
            }
        }
    }

    private void UpdateCharacterImage()
    {
        if(select == 0)
        {
            image.sprite = sprite0;
        }
        else if (select == 1)
        {
            image.sprite = sprite1;
        }
        else if (select == 2)
        {
            image.sprite = sprite2;
        }
        else if (select == 3)
        {
            image.sprite = sprite3;
        }
        else if (select == 4)
        {
            image.sprite = sprite4;
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
                selectedObj.SetActive(true);
                csi.Ready(this.name, select); 
                AudioManager.Instance.PlaySFX("selected_se");
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
                selectedObj.SetActive(false);
                csi.Unready();
                AudioManager.Instance.PlaySFX("cancel_se");
            }
        }
    }

    public void OnStart(InputAction.CallbackContext Start)
    {
        if (Start.started)
        {
            csi.GameStart();
        }
    }

    public void OnSelect(InputAction.CallbackContext Select)
    {
        if (Select.started)
        {
            csi.BackToTitle();
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
            this.name = "Player1";
            return;
        }

        if (exist[1] == false)
        {
            this.name = "Player2";
            return;
        }

        if (exist[2] == false)
        {
            this.name = "Player3";
            return;
        }

        if (exist[3] == false)
        {
            this.name = "Player4";
            return;
        }
    }
}