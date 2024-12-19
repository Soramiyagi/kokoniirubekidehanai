using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashClone : MonoBehaviour
{
    [SerializeField] private Dash dash;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dash.walkingFlag)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        if (dash.skill1Flag)
        {
            animator.SetTrigger("skill1");
        }

        if (dash.skill2Flag)
        {
            animator.SetTrigger("skill2");
        }

    }
}
