using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Timer, FirstStopGrounds, CountdownObj;
    private Timer timerScript;
    private FirstStopGrounds FSGsScript;
    private CountDown countDownScript;

    //�Q�[���ɎQ�����Ă���l��
    private int joinPlayerNum = 0;

    //�c��l��
    private bool[] restPlayer = new bool[4];

    //���Ԑ���
    public float limitTime_set;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            restPlayer[i] = true;
        }

        timerScript = Instantiate(Timer, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Timer>();
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

        if(finishCount == 1)
        {
            Finish();
        }
    }

    public void GameStart()
    {
        timerScript.TimerStart();
        FSGsScript.FSG_Delete();
    }

    private void Finish()
    {
        //�Q�[���I���̏���
        Debug.Log("�Q�[���Z�b�g");
    }
}
