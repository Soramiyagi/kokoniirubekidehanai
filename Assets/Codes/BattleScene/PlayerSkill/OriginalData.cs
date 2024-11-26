using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalData : Player
{
    public string characterName = "DefaultCharacter";

    // �X�s�[�h�ƃW�����v�́A�X�L���̃N�[���_�E�����Ԃ�h���N���X�Őݒ�
    protected override float Speed { get; set; } = 2.0f; // �X�s�[�h�l
    protected override float JumpForce { get; set; } = 5.0f; // �W�����v��
    protected override float Skill1CooldownTime { get; set; } = 4.0f; // �X�L��1�̃N�[���_�E��
    protected override float Skill2CooldownTime { get; set; } = 9.0f; // �X�L��2�̃N�[���_�E��

    // �v���n�u�𐶐����邽�߂̕ϐ�
    public GameObject prefab; // �v���n�u�̎Q�Ƃ��C���X�y�N�^�Őݒ�
    private GameObject spawnedPrefab; // �������ꂽ�v���n�u�̎Q��

    private bool skill1PushCheck, skill2PushCheck = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // �X�L��1�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill1Push()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima(true));

        //�����[�X���ɔ�������X�L���Ȃ�K�v
        Skill1PushCheck = true;
        */
    }

    // �X�L��1�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void Skill1Release()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima(false));

        //�����[�X���ɔ�������X�L���Ȃ�K�v
        if (Skill1PushCheck == true)
        {
            //�X�L�������̋L�q
            Skill1PushCheck = false;
        }
        */
    }

    // �X�L��2�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill2Push()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima(true));

        //�����[�X���ɔ�������X�L���Ȃ�K�v
        Skill2PushCheck = true;
        */
    }

    // �X�L��2�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void Skill2Release()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima(false));
        
        //�����[�X���ɔ�������X�L���Ȃ�K�v
        if (Skill2PushCheck == true)
        {
            //�X�L�������̋L�q
            Skill2PushCheck = false;
        }
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