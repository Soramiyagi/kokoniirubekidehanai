using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : Player
{
    public string characterName = "DefaultCharacter";

    // �v���n�u�𐶐����邽�߂̕ϐ�
    public GameObject prefab; // �v���n�u�̎Q�Ƃ��C���X�y�N�^�Őݒ�
    private GameObject spawnedPrefab; // �������ꂽ�v���n�u�̎Q��

    private ShootingStar_SkillManager SSS;
    private Animator animator;//�A�j���[�V������GetComponent����ϐ�
    private float movementThreshold = 0.001f;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SSS = this.GetComponent<ShootingStar_SkillManager>();
        animator = GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        float distanceMoved = Vector3.Distance(transform.position, previousPosition);

        // �ړ�������臒l�𒴂�����walking��true�ɂ���
        if (distanceMoved > movementThreshold)
        {
            animator.SetBool("walking", true);
           
        }
        else
        {
            animator.SetBool("walking", false);
           
        }

        previousPosition = transform.position;

    }
    protected override void jumping()
    {

        animator.SetTrigger("jumping");
    }

    protected override void Landing()
    {

        animator.SetTrigger("Landing");
    }
    // �X�L��1�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill1Push()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */
        animator.SetTrigger("skill1");
        SSS.UseSkill1();

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima());
    }

    // �X�L��2�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill2Push()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */
        animator.SetTrigger("skill2");
        SSS.UseSkill2();

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());
    }
}