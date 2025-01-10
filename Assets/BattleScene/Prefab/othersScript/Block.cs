using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    private float time;         //ブロックが消えるまでの時間
    private float scale;

    public float timeSet = 0;
    private BoxCollider boxCol;

    private bool revival = true;

    //マテリアル関連
    [SerializeField] private Material safety, caution, danger;
    new Renderer renderer;

    private Coroutine countdownCoroutine; // コルーチンの参照

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        boxCol = this.GetComponent<BoxCollider>();
        StateReset();
    }

    void StateReset()
    {
        time = timeSet;
        scale = 1;
        this.transform.localScale = new Vector3(scale, scale, scale);
        boxCol.isTrigger = false;
        renderer.material = safety;

        // コルーチンが実行中であれば停止
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null; // コルーチンの参照をクリア
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Playerが床に接触すると一定時間経過後に床が落ちる
        {
            if (countdownCoroutine == null) // コルーチンが実行中でない場合のみ開始
            {
                countdownCoroutine = StartCoroutine(Countdown()); // コルーチンを開始
                renderer.material = caution;
            }
        }
        else if (collision.gameObject.CompareTag("Break")) // Breakのコリジョン接触が起きた瞬間に床が落ちる
        {
            countdownCoroutine = StartCoroutine(Countdown()); // コルーチンを開始
            time = 0;
            renderer.material = danger;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Playerが床に接触すると一定時間経過後に床が落ちる
        {
            if (countdownCoroutine == null) // コルーチンが実行中でない場合のみ開始
            {
                countdownCoroutine = StartCoroutine(Countdown()); // コルーチンを開始
                renderer.material = caution;
            }
        }
        else if (collision.gameObject.CompareTag("Break")) // Breakのコリジョン接触が起きた瞬間に床が落ちる
        {
            countdownCoroutine = StartCoroutine(Countdown()); // コルーチンを開始
            time = 0;
            renderer.material = danger;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("CountStart"))
        {
            if (countdownCoroutine == null) // コルーチンが実行中でない場合のみ開始
            {
                countdownCoroutine = StartCoroutine(Countdown()); // コルーチンを開始
                renderer.material = caution;
            }
        }
        else if (other.gameObject.CompareTag("Break"))
        {
            countdownCoroutine = StartCoroutine(Countdown()); // コルーチンを開始
            time = 0;
            renderer.material = danger;
        }
        else if (other.gameObject.CompareTag("Fix"))
        {
            if (revival == true)
            {
                StateReset();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CautionArea"))
        {
            if (revival == true)
            {
                revival = false;

                countdownCoroutine = StartCoroutine(Countdown()); // コルーチンを開始
                time = 0;
                renderer.material = danger;
            }
        }
    }

    private IEnumerator Countdown()
    {
        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        renderer.material = danger;

        while (scale > 0)
        {
            scale -= Time.deltaTime * 0.75f;

            if (scale <= 0)
            {
                scale = 0;
                boxCol.isTrigger = true;
            }

            this.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        // コルーチン終了時に参照をクリア
        countdownCoroutine = null;
    }
}
