using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelect_InputManager : MonoBehaviour
{
    [SerializeField] private GameObject LoadClient, ReadyObj;

    private int playerNum;
    private int readyNum;

    private bool ready;

    int[] playerChoice = new int[4];

    // Start is called before the first frame update
    void Start()
    {
        playerNum = 0;
        readyNum = 0;
        ready = false;

        for (int i = 0; i < 4; i++)
        {
            playerChoice[i] = -1;
            CharacterSelect_Save.characterIndex[i] = -1;
        } 
    }

    public void JoinPlayer()
    {
        playerNum++;

        if (playerNum > readyNum)
        {
            ReadyObj.SetActive(false);
            ready = false;
        }
    }

    public void Ready(string player, int select)
    {
        //����������Ԃւ̈ڍs
        readyNum++;

        if (player == "Player1")
        {
            playerChoice[0] = select;
        }
        else if (player == "Player2")
        {
            playerChoice[1] = select;
        }
        else if (player == "Player3")
        {
            playerChoice[2] = select;
        }
        else if (player == "Player4")
        {
            playerChoice[3] = select;
        }

        if (playerNum <= readyNum)
        {
            ReadyObj.SetActive(true);
            ready= true;
        }
    }

    public void Unready()
    {
        //����������Ԃ��疢������Ԃւ̈ڍs
        readyNum--;

        if (playerNum > readyNum)
        {
            ReadyObj.SetActive(false);
            ready = false;
        }
    }

    public void GameStart()
    {
        if (ready == true)
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
        for (int i = 0; i < 4; i++)
        {
            CharacterSelect_Save.characterIndex[i] = playerChoice[i];
        }

        LoadClient.GetComponent<LoadClient>().LoadStart();
    }
}