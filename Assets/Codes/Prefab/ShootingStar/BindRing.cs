using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindRing : MonoBehaviour
{
    private float time;
    [SerializeField] private float speed = 1;
    [SerializeField] private float effectRange = 0;

    void OnEnable()
    {
        time = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time < effectRange)
        {
            time += effectRange * Time.deltaTime * speed;
        }
        else if(time >= effectRange)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.gameObject.SetActive(false);
        }

        this.transform.localScale = new Vector3(time + 1, 1, time + 1);
    }
}