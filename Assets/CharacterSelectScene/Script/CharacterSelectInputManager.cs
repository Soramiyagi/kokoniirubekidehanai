using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelectInputManager : MonoBehaviour
{
    [SerializeField] private GameObject loadClientToBattleScene, loadClientToTitleScene, readyObj;

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
            CharacterSelectSave.characterIndex[i] = -1;
        } 
    }

    public void JoinPlayer()
    {
        playerNum++;

        if (playerNum > readyNum)
        {
            readyObj.SetActive(false);
            ready = false;
        }
    }

    public void Ready(string player, int select)
    {
        //èÄîıäÆóπèÛë‘Ç÷ÇÃà⁄çs
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
            readyObj.SetActive(true);
            ready= true;
        }
    }

    public void Unready()
    {
        //èÄîıäÆóπèÛë‘Ç©ÇÁñ¢äÆóπèÛë‘Ç÷ÇÃà⁄çs
        readyNum--;

        if (playerNum > readyNum)
        {
            readyObj.SetActive(false);
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

    public void BackToTitle()
    {
        loadClientToTitleScene.GetComponent<LoadClient>().LoadStart();
    }

    private void SceneChange()
    {
        for (int i = 0; i < 4; i++)
        {
            CharacterSelectSave.characterIndex[i] = playerChoice[i];
        }

        loadClientToBattleScene.GetComponent<LoadClient>().LoadStart();
    }
}