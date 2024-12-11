using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject BulletPoint, Bind, Gun_FixSphere;
   

    public ParticleSystem particleSystem;
    private Animator animator;//�A�j���[�V������GetComponent����ϐ�
    private Vector3 previousPosition;
    private float movementThreshold = 0.001f;
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
        animator.SetTrigger("skill1");

        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */

        StartCoroutine(Skill1DelaySystem(0.2f));

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

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        Instantiate(Gun_FixSphere, this.transform.position, Quaternion.identity);

        // �v���C���[�̈ʒu�Ƀp�[�e�B�N���𐶐����čĐ�
        ParticleSystem particleInstance = Instantiate(particleSystem, this.transform.position, Quaternion.identity);
        particleInstance.Play();

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());

        // ��莞�Ԍ�Ƀp�[�e�B�N�����~�E�폜
        StartCoroutine(DestroyParticleAfterDelay(particleInstance, 1f));
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

    private IEnumerator Skill1DelaySystem(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Instantiate(Bind, BulletPoint.transform.position, Quaternion.Euler(0f, 90 - R_angle, 0f));
    }
    private IEnumerator Skill2DelaySystem(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}