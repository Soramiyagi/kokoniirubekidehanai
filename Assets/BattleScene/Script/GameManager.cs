using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu, SelectCursor1, SelectCursor2;
    [SerializeField] private GameObject LoadClient_ToCharacterSelect, LoadClient_ToResultScaene;
    [SerializeField] private GameObject Timer, FirstStopGrounds, CountdownObj;
    private Timer timerScript;
    private FirstStopGrounds FSGsScript;
    private CountDown countDownScript;

    //ゲームに参加している人数
    private int joinPlayerNum = 0;

    //残り人数
    private bool[] restPlayer = new bool[4];

    //時間制限
    public float limitTime_set;

    //Pause画面をいじれるかどうかのフラグ
    private bool pauseControl = false;

    // Start is called before the first frame update
    void Start()
    {
        Winner_Save.winnerPlayer = -1;

        for (int i = 0; i < 4; i++)
        {
            restPlayer[i] = false;
        }

        timerScript = Instantiate(Timer, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Timer>();
        timerScript.gameManager = this.GetComponent<GameManager>();
        timerScript.TimerSet(limitTime_set);
        FSGsScript = Instantiate(FirstStopGrounds, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<FirstStopGrounds>();
        countDownScript = Instantiate(CountdownObj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<CountDown>();
        countDownScript.gameManager = this.GetComponent<GameManager>();
    }

    public void Join()
    {
        if (joinPlayerNum < 4)
        {
            restPlayer[joinPlayerNum] = true;
            joinPlayerNum = joinPlayerNum + 1;
        }
    }

    public void Retire(int playerNum)
    {
        int finishCount = 0;

        restPlayer[playerNum] = false;

        for (int i = 0; i < 4; i++)
        {
            if (restPlayer[i] == true)
            {
                finishCount++;
            }
        }

        if (finishCount == 0 || finishCount == 1)
        {
            Finish();
        }
    }

    public void GameStart()
    {
        timerScript.TimerStart();
        FSGsScript.FSG_Delete();
    }

    public void Finish()
    {
        for (int i = 0; i < 4; i++)
        {
            if (restPlayer[i] == true)
            {
                Winner_Save.winnerPlayer = i;
            }
        }

        //ゲーム終了の処理
        LoadClient_ToResultScaene.GetComponent<LoadClient>().LoadStart();
    }

    public void TimeOver()
    {
        LoadClient_ToCharacterSelect.GetComponent<LoadClient>().LoadStart();
    }

    public void MenuDisplay(bool state)
    {
        if (state == false)
        {
            pauseControl = false;
            StartCoroutine(PauseControlWait());
            PauseMenu.SetActive(true);
        }
        else if (state == true)
        {
            PauseMenu.SetActive(false);
        }
    }

    public int MenuControl(int action)
    {
        if (action == 0)
        {
            if (pauseControl == true)
            {
                pauseControl = false;
                StartCoroutine(PauseControlWait());
                SelectCursor1.SetActive(true);
                SelectCursor2.SetActive(false);
            }
        }
        else if (action == 1)
        {
            if (pauseControl == true)
            {
                pauseControl = false;
                StartCoroutine(PauseControlWait());
                SelectCursor1.SetActive(false);
                SelectCursor2.SetActive(true);
            }
        }
        else if (action == 2)
        {
            if (SelectCursor1.activeSelf == true && SelectCursor2.activeSelf == false)
            {
                return 1;
            }
            else if (SelectCursor1.activeSelf == false && SelectCursor2.activeSelf == true)
            {
                StartCoroutine(LoadSceneCoroutine());
            }
        }

        return 0;
    }

    private IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        LoadClient_ToCharacterSelect.GetComponent<LoadClient>().LoadStart();
    }

    private IEnumerator PauseControlWait()
    { 
        yield return new WaitForSecondsRealtime(0.3f);
        pauseControl = true;
    }
}
