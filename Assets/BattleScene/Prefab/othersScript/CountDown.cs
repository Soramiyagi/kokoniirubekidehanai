using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private RectTransform canvasRectTransform;
    public GameManager gameManager;

    private float time;
    private float ex_time;
    private int preTime; // 前フレームの整数時間を記録

    // Start is called before the first frame update
    void Start()
    {
        time = 3.55f;
        ex_time = 1f;
        preTime = Mathf.CeilToInt(time); // 初期化時の整数時間

        if (canvasRectTransform != null)
        {
            canvasRectTransform.anchoredPosition = new Vector2(0, 0);
            canvasRectTransform.sizeDelta = new Vector2(800, 600);
            canvasRectTransform.pivot = new Vector2(0.5f, 0.5f);
        }
        countDownText.enabled = false;
    }

    void FixedUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;

            int currentTime = Mathf.CeilToInt(time); // 現在の整数時間

            // タイムが変わった場合にSFXを再生
            if (currentTime != preTime && currentTime > 0)
            {
                AudioManager.Instance.PlaySFX("count_down_se");
                preTime = currentTime; // 前フレームの時間を更新
            }

            if (time > 1)
            {
                countDownText.text = time.ToString("F0");

                if (time >= 3.5f)
                {
                    countDownText.enabled = false;
                }
                else
                {
                    countDownText.enabled = true;
                }
            }
        }
        else if (time <= 0)
        {
            gameManager.GameStart();
            countDownText.fontSize = 200f;
            countDownText.text = "GO!!";
            AudioManager.Instance.PlaySFX("game_start2_se");


            if (ex_time > 0)
            {
                ex_time -= Time.deltaTime;
            }
            else if (ex_time <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}

