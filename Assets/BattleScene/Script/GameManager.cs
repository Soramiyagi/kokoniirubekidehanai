using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu, selectCursor1, selectCursor2;
    [SerializeField] private GameObject loadClientToResult, loadClientToTitle;
    [SerializeField] private GameObject timer, firstStopGrounds, countdownObj;
    [SerializeField] private GameObject timeUp, gameSet;
    [SerializeField] private CautionArea caScript;
    [SerializeField] private AudioClip mainBGM; // 再生したいBGMをインスペクターで指定
    private Timer timerScript;
    private FirstStopGrounds fsgScript;
    private CountDown countDownScript;
    private AudioManager audioManager;

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
        WinnerSave.winnerPlayer = -1;

        for (int i = 0; i < 4; i++)
        {
            restPlayer[i] = false;
        }

        timerScript = Instantiate(timer, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Timer>();
        timerScript.gameManager = this.GetComponent<GameManager>();
        timerScript.TimerSet(limitTime_set);
        fsgScript = Instantiate(firstStopGrounds, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<FirstStopGrounds>();
        countDownScript = Instantiate(countdownObj, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<CountDown>();
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
        fsgScript.FSG_Delete();
        AudioManager.Instance.PlayBGM(mainBGM);
    }

    public void Finish()
    {
        for (int i = 0; i < 4; i++)
        {
            if (restPlayer[i] == true)
            {
                WinnerSave.winnerPlayer = i;
            }
        }

        gameSet.SetActive(true);
        StartCoroutine(UnscaledCountdown(3));
    }

    public void TimeOver()
    {
        timeUp.SetActive(true);
        StartCoroutine(UnscaledCountdown(3));
    }

    public void MenuDisplay(bool state)
    {
        if (state == false)
        {
            AudioManager.Instance.PauseBGM();
            pauseControl = false;
            StartCoroutine(PauseControlWait());
            pauseMenu.SetActive(true);
        }
        else if (state == true)
        {
            AudioManager.Instance.ResumeBGM();
            pauseMenu.SetActive(false);
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
                selectCursor1.SetActive(true);
                selectCursor2.SetActive(false);
            }
        }
        else if (action == 1)
        {
            if (pauseControl == true)
            {
                pauseControl = false;
                StartCoroutine(PauseControlWait());
                selectCursor1.SetActive(false);
                selectCursor2.SetActive(true);
            }
        }
        else if (action == 2)
        {
            if (selectCursor1.activeSelf == true && selectCursor2.activeSelf == false)
            {
                return 1;
            }
            else if (selectCursor1.activeSelf == false && selectCursor2.activeSelf == true)
            {
                StartCoroutine(LoadSceneCoroutine());
            }
        }

        return 0;
    }

    public void FirstCautionAreaReady()
    {
        caScript.FirstCautionAreaReady();
    }

    public void SecondCautionAreaReady()
    {
        caScript.SecondCautionAreaReady();
    }

    public void FirstCautionAreaON()
    {
        caScript.FirstCautionAreaON();
    }

    public void SecondCautionAreaON()
    {
        caScript.SecondCautionAreaON();
    }

    private IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        loadClientToTitle.GetComponent<LoadClient>().LoadStart();
    }

    private IEnumerator PauseControlWait()
    { 
        yield return new WaitForSecondsRealtime(0.3f);
        pauseControl = true;
    }

    private IEnumerator UnscaledCountdown(float duration)
    {
        timerScript.TimerHide();
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(duration);
        
        //ゲーム終了の処理
        loadClientToResult.GetComponent<LoadClient>().LoadStart();
    }
}