using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject Skill1Preview;

    // �X�s�[�h�ƃW�����v�́A�X�L���̃N�[���_�E�����Ԃ�h���N���X�Őݒ�
    protected override float Speed { get; set; } = 2.0f; // �X�s�[�h�l
    protected override float JumpForce { get; set; } = 5.0f; // �W�����v��
    protected override float Skill1CooldownTime { get; set; } = 4.0f; // �X�L��1�̃N�[���_�E��
    protected override float Skill2CooldownTime { get; set; } = 9.0f; // �X�L��2�̃N�[���_�E��

    // �v���n�u�𐶐����邽�߂̕ϐ�
    public GameObject skill2Prehub; // �X�L��2�̃v���n�u
    private GameObject spawnedPrefab; // �������ꂽ�v���n�u�̎Q��

    private bool previewSkill1 = false; // skill1�̃v���r���[�̂��߂̃t���O

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Skill1Preview.SetActive(false);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Debug.Log(moveHorizontal);

        if (previewSkill1 && spawnedPrefab != null)
        {
            // �v���C���[�̈ʒu�ɒǏ] (Y����-0.5���ăv���C���[�̉��ɒu��)
            Vector3 followPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z + 4);
            spawnedPrefab.transform.position = followPosition;
            Debug.Log("Skill1 prefab is following the player.");

        }
    }

    protected override void Skill1Push()
    {
        Skill1Preview.SetActive(true);
        Collider previewCollider = Skill1Preview.GetComponent<Collider>();
        if (previewCollider != null)
        {
            previewCollider.enabled = false; // �R���W�������I�t��
        }
    }


    protected override void Skill1Release()
    {
        Collider previewCollider = Skill1Preview.GetComponent<Collider>();
        if (previewCollider != null)
        {
            Debug.Log("aa");
            previewCollider.enabled = true; // �R���W�������I����
        }
        StartCoroutine(DestroyPrefabAfterDelay(0.1f));

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
    }

    // �X�L��2��������Ă���Ԃ̏������I�[�o�[���C�h
    protected override void Skill2Push()
    {
        if (spawnedPrefab == null)
        {
            // �v���C���[�̈ʒu����Y����-0.5�����ʒu�Ƀv���n�u�𐶐�
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            spawnedPrefab = Instantiate(skill2Prehub, spawnPosition, Quaternion.identity);

            // �R���W�������I�t�ɂ��鏈��
            Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();
            if (prefabCollider != null)
            {
                prefabCollider.enabled = false; // �R���W�������I�t��
            }

            Debug.Log("Bomb is charging Skill 2! Prefab instantiated at adjusted position.");
        }
        else
        {
            // �v���C���[�̈ʒu�ɒǏ]������ (Y����-0.5���ăv���C���[�̉��ɒu��)
            Vector3 followPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            spawnedPrefab.transform.position = followPosition;
            Debug.Log("Prefab is following the player.");
        }
    }

    // �X�L��2�𔭓����鏈�����I�[�o�[���C�h
    protected override void Skill2Release()
    {
        if (spawnedPrefab != null)
        {
            // �v���n�u�����̈ʒu�ɌŒ肵�A�R���W�������I���ɂ���
            Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();
            if (prefabCollider != null)
            {
                prefabCollider.enabled = true; // �R���W�������I����
            }

            // �v���n�u���폜���鏈��
            StartCoroutine(DestroyPrefabAfterDelay(0.1f));
        }

        // �N�[���_�E������
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
    }

    // 1�b��Ƀv���n�u���폜���邽�߂̃R���[�`��
    private IEnumerator DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab); // �v���n�u���폜
        }
        Skill1Preview.SetActive(false);
    }
}