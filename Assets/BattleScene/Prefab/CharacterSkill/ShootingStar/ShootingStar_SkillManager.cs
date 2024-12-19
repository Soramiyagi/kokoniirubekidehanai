using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar_SkillManager : MonoBehaviour
{
    [SerializeField] private GameObject Star1, Star2, Star3, Star4, BindRing;

    public void UseSkill1()
    {
        Star1.SetActive(true);
        Star2.SetActive(true);
        Star3.SetActive(true);
        Star4.SetActive(true);
    }

    public void UseSkill2()
    {
        BindRing.SetActive(true);
    }
}