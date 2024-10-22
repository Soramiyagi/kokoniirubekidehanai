using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelect_InputManager : MonoBehaviour
{
    public TextMeshProUGUI[] c_text = new TextMeshProUGUI[4];

    private int playerNum;
    private int readyNum;

    // Start is called before the first frame update
    void Start()
    {
        playerNum = 0;
        readyNum = 0;

        CharacterSelect_Save.P1_C = -1;
        CharacterSelect_Save.P2_C = -1;
        CharacterSelect_Save.P3_C = -1;
        CharacterSelect_Save.P4_C = -1;
    }

    public void JoinPlayer()
    {
        playerNum++;
    }

    public void Ready()
    {
        //����������Ԃւ̈ڍs
        readyNum++;

        if (playerNum <= readyNum)
        {
            SceneChange();
        }
    }

    public void Unready()
    {
        //����������Ԃ��疢������Ԃւ̈ڍs
        readyNum--;

        if (playerNum == readyNum)
        {
            SceneChange();
        }
    }

    public void LostEvent_A()
    {
        //������������Ԃ̃f�o�C�X����������
        playerNum--;

        if (playerNum <= readyNum)
        {
            SceneChange();
        }
    }

    public void LostEvent_B()
    {
        //����������Ԃ̃f�o�C�X����������
        playerNum--;
        readyNum--;

        if (playerNum <= readyNum)
        {
            SceneChange();
        }
    }

    private void SceneChange()
    {
        int[] playerChoice = new int[4];

        for (int i = 0; i < 4; i++)
        {
            if (c_text[i] != null)
            {
                if (int.TryParse(c_text[i].text, out int result))
                {
                    playerChoice[i] = result;
                }
                else
                {
                    playerChoice[i] = -1;
                } 
            }
            else
            {
                playerChoice[i] = -1;
            }
        }

        CharacterSelect_Save.P1_C = playerChoice[0];
        CharacterSelect_Save.P2_C = playerChoice[1];
        CharacterSelect_Save.P3_C = playerChoice[2];
        CharacterSelect_Save.P4_C = playerChoice[3];
        
        SceneManager.LoadScene("SampleScene");
    }
}