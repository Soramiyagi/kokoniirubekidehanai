using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
    [SerializeField] private GameObject BackGround;

    //�Ώہi�v���C���[�L�����j
    private GameObject[] targetObj = new GameObject[4];

    //�S�Ă̑Ώۂ̒��S�_�̍��W
    private Vector3 centerPoint;

    private Camera camera;

    //���ƂȂ�ʒu
    public float pos_x, pos_y, pos_z;

    //�ʒu�̒���
    private float offset_x, offset_y, offset_z;

    //���ׂẴL�����̒[�̃|�C���g
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

    //���S�_�̎Z�o(�S�ẴL�����̍��W�̒��̈�ԏ㉺���E�̃|�C���g�̒��S�_)
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

    //offset�̐ݒ�
    //���S�_�ƒ��S�_����ł������L�����̍��W�̋�������J�����̋����Ɖ�]�����肷��
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