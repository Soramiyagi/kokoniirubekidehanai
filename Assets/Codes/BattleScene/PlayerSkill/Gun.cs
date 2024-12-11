using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Player
{
    public string characterName = "DefaultCharacter";

<<<<<<< HEAD

    [SerializeField] private GameObject BulletPoint, Bind, Gun_FixSphere;
=======
    [SerializeField] private GameObject BulletPoint, Bind, Gun_FixSphere;
   
>>>>>>> feature-animation-Soramiyagi

    public ParticleSystem particleSystem;
    private Animator animator;//�A�j���[�V������GetComponent����ϐ�
    private Vector3 previousPosition;
    private float movementThreshold = 0.001f;
<<<<<<< HEAD

    private bool Skill1Delay = false;
    private bool Skill2Delay = false;

    public float skill1InstantiateInterval = 0;


=======
>>>>>>> feature-animation-Soramiyagi
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

<<<<<<< HEAD


=======
>>>>>>> feature-animation-Soramiyagi
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
<<<<<<< HEAD
        if (Skill1Delay)//skill1�̎g�p
        {
            
            Instantiate(Bind, BulletPoint.transform.position, Quaternion.Euler(0f, 90 - R_angle, 0f));

            canUseSkill1 = false;
            Skill1Delay=false;
            StartCoroutine(Skill1Cooldown());
            StartCoroutine(Skill1DuringAnima());
       
        }
=======
>>>>>>> feature-animation-Soramiyagi
        previousPosition = transform.position;
    }

    // �X�L��1�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill1Push()
    {
        animator.SetTrigger("skill1");

        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */

<<<<<<< HEAD
       
        StartCoroutine(Skill1DelaySystem(0.2f));

    }

    // �X�L��1�𗣂����Ƃ��̏������I�[�o�[���C�h

=======
        StartCoroutine(Skill1DelaySystem(0.2f));

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima());
    }
>>>>>>> feature-animation-Soramiyagi

    // �X�L��2�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill2Push()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */
        animator.SetTrigger("skill2");
<<<<<<< HEAD
        Instantiate(Gun_FixSphere, this.transform.position, Quaternion.identity);

=======
>>>>>>> feature-animation-Soramiyagi

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

<<<<<<< HEAD

            Instantiate(Gun_FixSphere, this.transform.position, Quaternion.identity);

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());

=======
        Instantiate(Gun_FixSphere, this.transform.position, Quaternion.identity);
>>>>>>> feature-animation-Soramiyagi

        // �v���C���[�̈ʒu�Ƀp�[�e�B�N���𐶐����čĐ�
        ParticleSystem particleInstance = Instantiate(particleSystem, this.transform.position, Quaternion.identity);
        particleInstance.Play();

<<<<<<< HEAD
            canUseSkill2 = false;
            StartCoroutine(Skill2Cooldown());
            StartCoroutine(Skill2DuringAnima());

            // ��莞�Ԍ�Ƀp�[�e�B�N�����~�E�폜
            StartCoroutine(DestroyParticleAfterDelay(particleInstance, 1f));
        }
=======
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());

        // ��莞�Ԍ�Ƀp�[�e�B�N�����~�E�폜
        StartCoroutine(DestroyParticleAfterDelay(particleInstance, 1f));
>>>>>>> feature-animation-Soramiyagi
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

    private IEnumerator Skill1Instantiate(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        Instantiate(Bind, BulletPoint.transform.position, Quaternion.Euler(0f, 90 - R_angle, 0f));
    }
}