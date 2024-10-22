using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    bool fall;       //ブロックが落ちるフラグ
    float fallSpeed;    //ブロックの落ちる速さ

    bool countDown;     //ブロックが落ちるまでのカウントを開始するフラグ
    float time;         //ブロックが落ちるまでの時間

    public float timeSet = 0;

    Vector3 startPos;   //このブロックの初期位置
    Vector3 currentPos; //このブロックの現在位置

    //マテリアル関連
    [SerializeField] private Material safety, caution, danger;
    new Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.position;
        renderer = GetComponent<Renderer>();

        StateReset();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (countDown == true)
        {
            if (time >= 0)
            {
                time = time - Time.deltaTime;
            }
            else
            {
                fall = true;
                renderer.material = danger;
            }
        }
        

        //ブロック落下
        if (fall == true)
        {
            currentPos.y -= fallSpeed * Time.deltaTime;
            this.transform.position = currentPos;


            if (currentPos.y < -20)
            {
                StateReset();
            }
        }
    }

    void StateReset()
    {
        fall = false;
        fallSpeed = 3f;
        countDown = false;
        time = timeSet;
        currentPos = startPos;
        this.transform.position = currentPos;
        renderer.material = safety;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))//Playerが床に接触すると一定時間経過後に床が落ちる
        {
            if (fall == false)
            {
                countDown = true;
                renderer.material = caution;
            }
        }
        if (collision.gameObject.CompareTag("Skill"))//Skillのコリジョン接触が起きた瞬間に床が落ちる
        {
            fall = true;
            renderer.material = danger;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (fall == false)
            {
                countDown = true;
                renderer.material = caution;
            }
        }
    }
}
