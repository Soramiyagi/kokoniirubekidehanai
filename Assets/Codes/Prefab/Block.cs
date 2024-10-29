using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private bool countDown;     //ブロックが落ちるまでのカウントを開始するフラグ
    private float time;         //ブロックが消えるまでの時間
    private float scale;

    public float timeSet = 0;
    private BoxCollider boxCol;

    //マテリアル関連
    [SerializeField] private Material safety, caution, danger;
    new Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        boxCol = this.GetComponent<BoxCollider>();
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
                renderer.material = danger;

                if (scale > 0)
                {
                    scale = scale - Time.deltaTime * 0.5f;
                    if (scale <= 0)
                    {
                        scale = 0;
                        countDown = false;
                        boxCol.isTrigger = true;
                    }
                }

                this.transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }

    void StateReset()
    {
        countDown = false;
        time = timeSet;
        scale = 1;
        this.transform.localScale = new Vector3(scale, scale, scale);
        boxCol.isTrigger = false;
        renderer.material = safety;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))//Playerが床に接触すると一定時間経過後に床が落ちる
        {
            if (countDown == false)
            {
                countDown = true;
                renderer.material = caution;
            }
        }
        else if (collision.gameObject.CompareTag("Break"))//Breakのコリジョン接触が起きた瞬間に床が落ちる
        {
            countDown = true;
            time = 0;
            renderer.material = danger;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (countDown == false)
            {
                countDown = true;
                renderer.material = caution;
            }
        }
        else if (other.gameObject.CompareTag("Fix"))
        {
            StateReset();
        }
    }
}
