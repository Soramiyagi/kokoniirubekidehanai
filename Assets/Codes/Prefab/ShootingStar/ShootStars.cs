using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStars : MonoBehaviour
{
    [SerializeField] private GameObject EndPoint, MiddlePoint;
    
    public float speed = 0; 
    private float t;
    private Vector3 activatePos, movePos;
    private Vector3 endPos, middlePos;

    // Start is called before the first frame update
    void Start()
    {
        t = 0.0f;

        activatePos = this.transform.position;
        endPos = EndPoint.transform.position;
        middlePos = MiddlePoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        t += Time.deltaTime * speed;

        if (t < 1.0)
        {
            movePos = CalculateBezierPoint(t, activatePos, middlePos, endPos);
        }
        else if (t >= 1.0f)
        {
            // tを1.0にクランプしない

            if (t >= 1.15f)
            {
                Destroy(this.gameObject);
            }
            else
            {
                // tが1を超えた場合の位置を計算
                movePos = CalculateBezierPoint(1.0f, activatePos, middlePos, endPos) + (t - 1.0f) * (endPos - middlePos);
            }
        }

        movePos = CalculateBezierPoint(t, activatePos, middlePos, endPos);
        this.transform.position = movePos;
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1-t)^2 * p0
        p += 2 * u * t * p1; // 2 * (1-t) * t * p1
        p += tt * p2; // t^2 * p2

        return p;
    }
}
