using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject Shaft, BulletPoint, Bind, FixSphere;

    public ParticleSystem particleSystem;
    private Animator animator;//�A�j���[�V������GetComponent����ϐ�
    private Vector3 previousPosition;
    private float movementThreshold = 0.001f;
    private bool Skill1Delay = false;
    private bool Skill2Delay = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        //animator.SetBool("walking", true);//walking��ture�ɂ���
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
        Debug.Log(Skill1Delay);
    }

    // �X�L��1�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill1Push()
    {
        animator.SetTrigger("skill1");
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */
        Skill1DelaySystem(Skill1Delay, 1f);
        if (Skill1Delay)
        {
            Instantiate(Bind, BulletPoint.transform.position, Quaternion.Euler(0f, 90 - R_angle, 0f));

            canUseSkill1 = false;
            StartCoroutine(Skill1Cooldown());
            StartCoroutine(Skill1DuringAnima(true));
        }
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

        if (Skill2Delay)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

            Instantiate(FixSphere, this.transform.position, Quaternion.identity);

            // �v���C���[�̈ʒu�Ƀp�[�e�B�N���𐶐����čĐ�
            ParticleSystem particleInstance = Instantiate(particleSystem, this.transform.position, Quaternion.identity);
            particleInstance.Play();

            canUseSkill2 = false;
            StartCoroutine(Skill2Cooldown());
            StartCoroutine(Skill2DuringAnima(true));

            // ��莞�Ԍ�Ƀp�[�e�B�N�����~�E�폜
            StartCoroutine(DestroyParticleAfterDelay(particleInstance, 1f));
        }
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

    // �p�[�e�B�N�����Đ����郁�\�b�h
    public void PlayParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }



    private IEnumerator DestroyParticleAfterDelay(ParticleSystem particleInstance, float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        particleInstance.Stop();
        particleInstance.Clear();
        Destroy(particleInstance.gameObject); // �p�[�e�B�N���I�u�W�F�N�g���폜
    }

    private IEnumerator Skill1DelaySystem(bool Skill1Delay, float delay)
    {
        yield return new WaitForSeconds(delay);
        Skill1Delay = true;
    }
    private IEnumerator Skill2DelaySystem(bool Skill2Delay, float delay)
    {
        yield return new WaitForSeconds(delay);
        Skill2Delay = true;
    }
}