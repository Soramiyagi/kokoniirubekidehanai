using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultAction : MonoBehaviour
{
    private Animator animator;//�A�j���[�V������GetComponent����ϐ�

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAnima()
    {
        animator.SetTrigger("Result");
    }
}
