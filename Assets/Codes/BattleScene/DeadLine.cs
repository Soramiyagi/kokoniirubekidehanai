using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    [SerializeField] private GameObject FixSphere;

    Vector3 hitPos;

    private float interval_set = 0.05f;
    private float interval;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void FixedUpdate()
    {
        if (interval > 0)
        {
            interval -= Time.deltaTime;
        }
    }

    // 地面に接触したときに呼ばれる
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (interval <= 0)
            {
                //collision.contactsに保存されている衝突情報を調べる
                foreach (ContactPoint hitPoint in collision.contacts)
                {
                    hitPos = hitPoint.point;   //衝突場所を取得
                    interval = interval_set;
                }

                Instantiate(FixSphere, hitPos, Quaternion.identity);
            }
        }
    }
}