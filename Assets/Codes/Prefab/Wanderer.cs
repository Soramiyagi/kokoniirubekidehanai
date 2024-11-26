using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : MonoBehaviour
{
    public float speed_x, speed_y = 0;
    public float time_set = 0;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        time = time_set;

        speed_x = Random.Range(-4f, 4f);
        speed_y = Random.Range(-4f, 4f);

        if(speed_x <= 0)
        {
            speed_x = -4.0f;
        }
        else if (speed_x > 0)
        {
            speed_x = 4.0f;
        }

        if (speed_y <= 0)
        {
            speed_y = -4.0f;
        }
        else if (speed_y > 0)
        {
            speed_y = 4.0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }

        if(this.transform.position.x >= 20f || this.transform.position.x <= 0f)
        {
            speed_x = -speed_x;
        }

        if (this.transform.position.z >= 20f || this.transform.position.z <= 0f)
        {
            speed_y = -speed_y;
        }

        this.transform.position += new Vector3(speed_x * Time.deltaTime, 0, speed_y * Time.deltaTime);
        this.transform.Rotate(new Vector3(0, 80f * Time.deltaTime, 0));
    }
}
