using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    [SerializeField] private GameObject BackGround;

    //対象（プレイヤーキャラ）
    private GameObject[] targetObj = new GameObject[4];

    //全ての対象の中心点の座標
    private Vector3 centerPoint;

    private Camera camera;

    //元となる位置
    public float pos_x, pos_y, pos_z;

    //位置の調整
    private float offset_x, offset_y, offset_z;

    //すべてのキャラの端のポイント
    float lx = 0;
    float rx = 0;
    float uz = 0;
    float dz = 0;

    void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            targetObj[i] = null;
        }
        centerPoint = Vector3.zero;
        offset_x = 0;
        offset_y = 0;
        offset_z = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = this.GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        centerPoint = CenterPoint_Calculation(targetObj);
        Offset_Set();

        this.transform.position = new Vector3(pos_x + offset_x, pos_y + offset_y, pos_z + offset_z) + centerPoint;
    }

    public void FirstSet(GameObject[] players)
    {
        targetObj = players;
        centerPoint = CenterPoint_Calculation(targetObj);
        Offset_Set(); 
        this.transform.position = new Vector3(pos_x + offset_x, pos_y + offset_y, pos_z + offset_z) + centerPoint;
        BackGround.SetActive(false);
    }

    //中心点の算出(全てのキャラの座標の中の一番上下左右のポイントの中心点)
    private Vector3 CenterPoint_Calculation(GameObject[] targetObj)
    {
        Vector3[] targetPos = new Vector3[4];
        Vector3 centerPoint = Vector3.zero;

        bool first = true;

        for (int i = 0; i < 4; i++)
        {

            if (targetObj[i] != null)
            {
                if (first == true)
                {
                    lx = targetObj[i].transform.position.x;
                    rx = targetObj[i].transform.position.x;
                    uz = targetObj[i].transform.position.z;
                    dz = targetObj[i].transform.position.z;
                    first = false;
                }
                else
                {
                    if (targetObj[i].transform.position.x < lx)
                    {
                        lx = targetObj[i].transform.position.x;
                    }
                    else if (targetObj[i].transform.position.x > rx)
                    {
                        rx = targetObj[i].transform.position.x;
                    }

                    if (targetObj[i].transform.position.z > uz)
                    {
                        uz = targetObj[i].transform.position.z;
                    }
                    if (targetObj[i].transform.position.z < dz)
                    {
                        dz = targetObj[i].transform.position.z;
                    }
                }
            }
        }
        centerPoint = new Vector3((lx + rx) / 2, 0, (uz + dz) / 2);
        return centerPoint;
    }

    //offsetの設定
    //中心点と中心点から最も遠いキャラの座標の距離からカメラの距離と回転を決定する
    private void Offset_Set()
    {
        float farDistance = 0;

        for (int i = 0; i < 4; i++)
        {
            if (targetObj[i] != null)
            {
                float distance = Vector3.Distance(centerPoint, targetObj[i].transform.position);
                if(farDistance < distance)
                {
                    farDistance = distance;
                }
            }
        }

        offset_y = farDistance / 2;
        offset_z = -(farDistance / 2 + (centerPoint.z - dz) / 3);
    }
}