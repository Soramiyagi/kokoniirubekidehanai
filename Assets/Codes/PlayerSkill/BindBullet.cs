using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindBullet : MonoBehaviour
{
    private float time;
    public float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        time = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;
        }
        else if (time <= 0)
        {
            Destroy(this.gameObject);
        }

        this.transform.position += transform.forward * speed * Time.deltaTime;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
