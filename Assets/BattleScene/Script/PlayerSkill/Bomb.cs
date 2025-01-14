using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject Skill1Preview;

    public ParticleSystem skill1ParticleSystem;
    public ParticleSystem skill2ParticleSystem;
    public ParticleSystem bindParticleSystem;


    // �v���n�u�𐶐����邽�߂̕ϐ�
    public GameObject skill1Prehub; // �X�L��1�̃v���n�u
    public GameObject skill2Prehub; // �X�L��2�̃v���n�u
    private GameObject spawnedPrefab; // �������ꂽ�v���n�u�̎Q��

    private bool previewSkill1 = false; // skill1�̃v���r���[�̂��߂̃t���O

    private bool skill2StateCheck = false;

    private float movementThreshold = 0.001f;

    private Animator animator;//�A�j���[�V������GetComponent����ϐ�

    // �O�t���[���̈ʒu���L�^����ϐ�
    private Vector3 previousPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Skill1Preview.SetActive(false);
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
        //animator.SetBool("walking", true);//walking��ture�ɂ���
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (previewSkill1 && spawnedPrefab != null)
        {
            // �v���C���[�̈ʒu�ɒǏ] (Y����-0.5���ăv���C���[�̉��ɒu��)
            Vector3 followPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z + 4);
            spawnedPrefab.transform.position = followPosition;
        }

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
    protected override void Binding()
    {
        if (bindParticleSystem != null)
        {
            bindParticleSystem.Play();
        }

        StartCoroutine(bindParticleDelay());
    }
    protected override void Skill1Push()
    {
        //walking��ture�ɂ���        
        animator.SetTrigger("skill1");
        Skill1Preview.SetActive(true);

        // �������ꂽ�p�[�e�B�N���C���X�^���X���Đ�
        if (skill1ParticleSystem != null)
        {
            skill1ParticleSystem.Play();
        }
        StartCoroutine(Skill1DestroyPrefabAndParticlesAfterDelay(0.1f, 1f));

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima());
    }

    // �X�L��2��������Ă���Ԃ̏������I�[�o�[���C�h
    protected override void Skill2Push()
    {
        if (skill2StateCheck == false)
        {
            animator.SetTrigger("skill2Put");
            if (spawnedPrefab == null)
            {
                // �v���C���[�̈ʒu����Y����-0.5�����ʒu�Ƀv���n�u�𐶐�
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                spawnedPrefab = Instantiate(skill2Prehub, spawnPosition, Quaternion.identity);

                StartCoroutine(Skill2DuringAnima());

                // �R���W�������I�t�ɂ��鏈��
                Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();
                if (prefabCollider != null)
                {
                    prefabCollider.enabled = false; // �R���W�������I�t��
                }

            }
            skill2StateCheck = true;
        }
        else if (skill2StateCheck == true)
        {
            if (spawnedPrefab != null)
            {
                animator.SetTrigger("skill2Kidou");
                // �v���n�u�����̈ʒu�ɌŒ肵�A�R���W�������I���ɂ���
                Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();
                if (prefabCollider != null)
                {
                    prefabCollider.enabled = true; // �R���W�������I����
                }

                // �p�[�e�B�N���𐶐����čĐ�
                if (skill2ParticleSystem != null)
                {
                    // �p�[�e�B�N���V�X�e���𐶐�
                    ParticleSystem particleInstance = Instantiate(skill2ParticleSystem, spawnedPrefab.transform.position, Quaternion.identity);
                    particleInstance.Play(); // �p�[�e�B�N�����Đ�
                }

                // �v���n�u�̍폜����
                StartCoroutine(Skill2DestroyPrefabAndParticlesAfterDelay(1f, 2f));
            }

            skill2StateCheck = false;

            // �N�[���_�E������
            canUseSkill2 = false;
            StartCoroutine(Skill2Cooldown());
            StartCoroutine(Skill2DuringAnima());
        }
    }

    private IEnumerator Skill1DestroyPrefabAndParticlesAfterDelay(float delay, float delay2)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        Skill1Preview.SetActive(false);
        Collider previewCollider = Skill1Preview.GetComponent<Collider>();
        yield return new WaitForSeconds(delay2); // �w�肵���b���ҋ@
        if (skill1ParticleSystem != null)
        {
            skill1ParticleSystem.Stop();
            skill1ParticleSystem.Clear();
        }
    }

    // 1�b��Ƀv���n�u���폜���邽�߂̃R���[�`��
    private IEnumerator Skill2DestroyPrefabAndParticlesAfterDelay(float delay, float delay2)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab); // �v���n�u���폜
        }
        yield return new WaitForSeconds(delay2);
        if (skill2ParticleSystem != null)
        {
            skill2ParticleSystem.Stop(); // �p�[�e�B�N����~
            skill2ParticleSystem.Clear(); // �p�[�e�B�N�����N���A
            skill2ParticleSystem.transform.SetParent(transform, true); // �v���C���[�̎q�I�u�W�F�N�g�ɖ߂�
            skill2ParticleSystem.transform.localPosition = Vector3.zero; // ���̈ʒu�ɖ߂��i�K�v�ɉ����Ē����j
        }
    }

    private IEnumerator bindParticleDelay()
    {
        yield return new WaitForSeconds(1.5f);
        bindParticleSystem.Stop();
        bindParticleSystem.Clear();
    }

    private void OnDestroy()
    {
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
        }
    }
}