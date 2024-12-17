using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindBullets : MonoBehaviour
{
    [SerializeField] private GameObject Bullet1, Bullet2;
    public float speed = 5f;
    public float frequency = 3f;  // ���g���𒲐�
    public float magnitude = 2f;  // �U��

    private Vector3 startPosBullet1;
    private Vector3 startPosBullet2;
    private float time;
    private Vector3 direction;

    void Start()
    {
        startPosBullet1 = Bullet1.transform.position;
        startPosBullet2 = Bullet2.transform.position;
        time = 0f;
        direction = transform.forward; // ����������ݒ�
    }

    void FixedUpdate()
    {
        if (time < 10)
        {
            time += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // time ���X�V���Ĉړ��𐧌�
        float currentTime = time * speed;

        // �e�̌������铮�����v�Z
        float sinValue = Mathf.Sin(currentTime * frequency) * magnitude;

        // �i�s�����ɑ΂��Đ����Ȉړ����v�Z
        Vector3 crossMovement = Vector3.Cross(direction, Vector3.up) * sinValue;

        // �e�̈ʒu���X�V
        Bullet1.transform.position = startPosBullet1 + direction * currentTime + crossMovement;
        Bullet2.transform.position = startPosBullet2 + direction * currentTime - crossMovement;

        // Move the entire object forward
        this.transform.position += direction * speed * Time.deltaTime;
    }
}
