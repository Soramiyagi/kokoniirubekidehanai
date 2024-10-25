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

    // ’n–Ê‚ÉÚG‚µ‚½‚Æ‚«‚ÉŒÄ‚Î‚ê‚é
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.contacts‚É•Û‘¶‚³‚ê‚Ä‚¢‚éÕ“Ëî•ñ‚ğ’²‚×‚é
            foreach (ContactPoint hitPoint in collision.contacts)
            {
                hitPos = hitPoint.point;   //Õ“ËêŠ‚ğæ“¾
            }

            Instantiate(FixSphere, hitPos, Quaternion.identity);
        }
    }
}