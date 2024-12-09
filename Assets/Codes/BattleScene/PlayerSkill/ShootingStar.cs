using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : Player
{
    [SerializeField] private GameObject Shaft;

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
        Shaft.transform.rotation = Quaternion.Euler(0.0f, 90 - R_angle, 0.0f);
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
        StartCoroutine(Skill1DuringAnima(true));
    }

    // �X�L��1�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void Skill1Release()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima(false));
        */
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
        StartCoroutine(Skill2DuringAnima(true));
    }

    // �X�L��2�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void Skill2Release()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima(false));
        */
    }

    // 1�b��Ƀv���n�u���폜���邽�߂̃R���[�`��
    private IEnumerator DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab); // �v���n�u���폜

        }
    }
}