using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    [SerializeField] private GameObject FixSphere;

    Vector3 hitPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // �n�ʂɐڐG�����Ƃ��ɌĂ΂��
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.contacts�ɕۑ�����Ă���Փˏ��𒲂ׂ�
            foreach (ContactPoint hitPoint in collision.contacts)
            {
                hitPos = hitPoint.point;   //�Փˏꏊ���擾
            }

            Instantiate(FixSphere, hitPos, Quaternion.identity);
        }
    }
}