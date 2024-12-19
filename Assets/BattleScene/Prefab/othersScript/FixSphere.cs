using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixSphere : MonoBehaviour
{
    public float effectRange = 0;

    private bool effect;
    private float scale;

    // Start is called before the first frame update
    void Start()
    {
        effect = true;
        scale = 0;
        this.transform.localScale = new Vector3(scale, scale, scale);
    }

    void FixedUpdate()
    {
        if (effect == true)
        {
            if (scale < effectRange)
            {
                scale = scale + Time.deltaTime * 15f;
            }
            else if (scale >= effectRange)
            {
                scale = 0;
                Destroy(this.gameObject);
            }

            this.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}