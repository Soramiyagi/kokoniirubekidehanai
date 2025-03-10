using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindBullets : MonoBehaviour
{
    [SerializeField] private GameObject Bullet1, Bullet2;
    [SerializeField] private TrailRenderer Bullet1trail, Bullet2trail;
    public float speed = 5f;
    public float frequency = 3f;  // 周波数を調整
    public float magnitude = 2f;  // 振幅

    private Vector3 startPosBullet1;
    private Vector3 startPosBullet2;
    private float time;
    private Vector3 direction;

    void OnEnable()
    {   
        Bullet1.transform.position = this.transform.position;
        Bullet2.transform.position = this.transform.position;
        startPosBullet1 = Bullet1.transform.position;
        startPosBullet2 = Bullet2.transform.position;
        time = 0f;
        direction = transform.forward; // 初期方向を設定
        
        Bullet1trail.Clear();
        Bullet2trail.Clear();
        Bullet1trail.enabled = true;
        Bullet2trail.enabled = true;
    }

    void OnDisable()
    {
        Bullet1trail.enabled = false;
        Bullet2trail.enabled = false;
        Bullet1trail.Clear();
        Bullet2trail.Clear();
    }

    void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(direction);
    }

    void FixedUpdate()
    {
        if (time < 5)
        {
            time += Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        // time を更新して移動を制御
        float currentTime = time * speed;

        // 弾の交差する動きを計算
        float sinValue = Mathf.Sin(currentTime * frequency) * magnitude;

        // 進行方向に対して垂直な移動を計算
        Vector3 crossMovement = Vector3.Cross(direction, Vector3.up) * sinValue;

        // 弾の位置を更新
        Bullet1.transform.position = startPosBullet1 + direction * currentTime + crossMovement;
        Bullet2.transform.position = startPosBullet2 + direction * currentTime - crossMovement;

        // Move the entire object forward
        this.transform.position += direction * speed * Time.deltaTime;
    }
}
