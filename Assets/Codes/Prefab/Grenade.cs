using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
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
            transform.localScale = new Vector3(3f, 3f, 3f);
            StartCoroutine(DestroyPrefabAfterDelay(0.1f));
        }
    }

    // 1秒後にプレハブを削除するためのコルーチン
    private IEnumerator DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }
}
