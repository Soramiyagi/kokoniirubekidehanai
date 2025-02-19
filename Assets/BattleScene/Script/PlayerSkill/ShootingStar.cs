using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : Player
{
    public string characterName = "DefaultCharacter";

    // �v���n�u�𐶐����邽�߂̕ϐ�
    public GameObject prefab; // �v���n�u�̎Q�Ƃ��C���X�y�N�^�Őݒ�
    private GameObject spawnedPrefab; // �������ꂽ�v���n�u�̎Q��

    private ShootingStarSkillManager sss;
    private Animator animator;//�A�j���[�V������GetComponent����ϐ�
    private float movementThreshold = 0.001f;
    private Vector3 previousPosition;
    public ParticleSystem bindParticleSystem;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sss = this.GetComponent<ShootingStarSkillManager>();
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
    protected override void Jumping()
    {

        animator.SetTrigger("jumping");
    }

    protected override void Landing()
    {

        animator.SetTrigger("Landing");
    }
    
    protected override void Binding(bool super)
    {
        if (super == true)
        {
            if (bindParticleSystem != null)
            {
                bindParticleSystem.startColor = Color.red;
                bindParticleSystem.Play();
            }

            StartCoroutine(BindParticleDelay(2.5f));
        }
        else
        {
            if (bindParticleSystem != null)
            {
                bindParticleSystem.startColor = Color.blue;
                bindParticleSystem.Play();
            }

            StartCoroutine(BindParticleDelay(1.5f));
        }
    }

    // �X�L��1�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill1Push()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */
        animator.SetTrigger("skill1");
        sss.UseSkill1();

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima());

        PlaySoundEffect(SE[1]);
    }

    // �X�L��2�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill2Push()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */
        animator.SetTrigger("skill2");
        sss.UseSkill2();

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());

        PlaySoundEffect(SE[2]);
    }

    private IEnumerator BindParticleDelay(float time)
    {
        yield return new WaitForSeconds(time);
        bindParticleSystem.Stop();
        bindParticleSystem.Clear();
    }
}