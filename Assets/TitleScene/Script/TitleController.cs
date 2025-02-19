using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleController : MonoBehaviour
{
    [SerializeField] GameObject HowTo, encyclopedia, loadCliantToCharacterSelect;
    [SerializeField] MenuImageController micScript;
    [SerializeField] HowToPageController htpcScript;
    [SerializeField] EncyclopediaPageController epcScript;
    
    public float intervalSet;  //���͂̃C���^�[�o��
    public float lstickDeadzone;
    private float interval;
    private Vector2 menuSelectInput;
    private bool canInputMenu;

    private InputAction L_Stick;
    private InputAction D_Pad;

    //���j���[���ڂ̔ԍ�
    //0�V�ѕ��@1�E�J�n�@2�E�}��
    private int menu = 1;

    //���d�ǂݍ��ݖh�~
    private bool loadStart = false;

    // Start is called before the first frame update
    void Start()
    {
        canInputMenu = true;
        interval = intervalSet;
        menuSelectInput = Vector2.zero;

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

        if (canInputMenu == true && interval <= 0)
        {
            if (menuSelectInput.x > lstickDeadzone)
            {
                if (menu <= 1)
                {
                    menu++;
                    micScript.ScreenUpdate(menu);
                    interval = intervalSet;
                    AudioManager.Instance.PlaySFX("select_se");
                }
            }
            else if (menuSelectInput.x < -lstickDeadzone)
            {
                if (menu >= 1)
                {
                    menu--;
                    micScript.ScreenUpdate(menu);
                    interval = intervalSet;
                    AudioManager.Instance.PlaySFX("select_se");
                }

            }
        }
        else if(canInputMenu == false && interval <= 0)
        {
            if (menu == 0)
            {
                if (menuSelectInput.x > lstickDeadzone)
                {
                    htpcScript.NextPage();
                    interval = intervalSet;
                }
                else if (menuSelectInput.x < -lstickDeadzone)
                {
                    htpcScript.BackPage();
                    interval = intervalSet;
                }
            }
            else if (menu == 2)
            {
                if (menuSelectInput.x > lstickDeadzone)
                {
                    epcScript.NextPage();
                    interval = intervalSet;
                }
                else if (menuSelectInput.x < -lstickDeadzone)
                {
                    epcScript.BackPage();
                    interval = intervalSet;
                }
            }

        }
    }

    public void OnDecision(InputAction.CallbackContext Decision)
    {
        if (canInputMenu == true)
        {
            if (Decision.started)
            {
                canInputMenu = false;
            }
        }


        if (menu == 0)
        {
            HowTo.SetActive(true);
            AudioManager.Instance.PlaySFX("decision_se");
        }
        else if (menu == 1)
        {
            if (loadStart == false)
            {
                StartCoroutine(Delay(1.25f));
                AudioManager.Instance.PlaySFX("decision_se");
            }
        }
        else if (menu == 2)
        {
            encyclopedia.SetActive(true);
            AudioManager.Instance.PlaySFX("decision_se");
        }
    }

    public void OnCancel(InputAction.CallbackContext Cancel)
    {
        if (canInputMenu == false)
        {
            if (Cancel.started)
            {
                canInputMenu = true;
                HowTo.SetActive(false);
                encyclopedia.SetActive(false);
                AudioManager.Instance.PlaySFX("cancel_se");
            }
        }
    }

    IEnumerator Delay(float time)
    {
        // �w�肵���b������������҂��܂��B(�����ł�1.0�b)
        yield return new WaitForSeconds(time);

        loadCliantToCharacterSelect.GetComponent<LoadClient>().LoadStart();
        loadStart = true;
    }
}