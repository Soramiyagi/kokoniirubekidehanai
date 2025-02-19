using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarSkillManager : MonoBehaviour
{
    [SerializeField] private GameObject star1, star2, star3, star4, bindRing;

    public void UseSkill1()
    {
        star1.SetActive(true);
        star2.SetActive(true);
        star3.SetActive(true);
        star4.SetActive(true);
    }

    public void UseSkill2()
    {
        bindRing.SetActive(true);
    }
}