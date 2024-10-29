using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Test : MonoBehaviour
{
    public float time_set;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = time_set;
    }

    void FixedUpdate()
    {
        if (time > 0)
        {
            time = time - Time.deltaTime;
        }
        else if(time <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
