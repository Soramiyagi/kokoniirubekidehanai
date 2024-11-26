using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject LoadClient_ToCharacterSelect;
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

    // Start is called before the first frame update
    void Start()
    {
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
        //ゲーム終了の処理
        LoadClient_ToCharacterSelect.GetComponent<LoadClient>().LoadStart();
    }
}
