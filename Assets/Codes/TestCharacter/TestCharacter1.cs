using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter1 : Player_Test
{
    public string characterName = "DefaultCharacter";

    // HP�ƃX�s�[�h�ƃW�����v�́A�X�L���̃N�[���_�E�����Ԃ�h���N���X�Őݒ�
    protected override int HP { get; set; } = 5; // �̗�
    protected override float Speed { get; set; } = 2.0f; // �X�s�[�h�l
    protected override float JumpForce { get; set; } = 5.0f; // �W�����v��
    protected override float A_SkillCooldownTime { get; set; } = 1.0f; // �U���X�L���̃N�[���_�E��
    protected override float D_SkillCooldownTime { get; set; } = 1.0f; // �h��X�L���̃N�[���_�E��
    protected override float SP1_SkillCooldownTime { get; set; } = 5.0f; // �X�y�V�����X�L��1�̃N�[���_�E��
    protected override float SP2_SkillCooldownTime { get; set; } = 10.0f; // �X�y�V�����X�L��2�̃N�[���_�E��

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // �U���X�L���������ꂽ���̏������I�[�o�[���C�h
    protected override void A_SkillPush()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        A_canUseSkill = false;
        StartCoroutine(A_SkillCooldown());
        */
    }

    // �U���X�L���𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void A_SkillRelease()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        A_canUseSkill = false;
        StartCoroutine(A_SkillCooldown());
        */
    }

    // �h��X�L���������ꂽ���̏������I�[�o�[���C�h
    protected override void D_SkillPush()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        D_canUseSkill = false;
        StartCoroutine(D_SkillCooldown());
        */
    }

    // �h��X�L���𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void D_SkillRelease()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        D_canUseSkill = false;
        StartCoroutine(D_SkillCooldown());
        */
    }

    // �X�y�V�����X�L��1�������ꂽ���̏������I�[�o�[���C�h
    protected override void SP1_SkillPush()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        SP1_canUseSkill = false;
        StartCoroutine(SP1_SkillCooldown());
        */
    }

    // �X�y�V�����X�L��1�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void SP1_SkillRelease()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        SP1_canUseSkill = false;
        StartCoroutine(SP1_SkillCooldown());
        */
    }

    // �X�y�V�����X�L��2�������ꂽ���̏������I�[�o�[���C�h
    protected override void SP2_SkillPush()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        SP2_canUseSkill = false;
        StartCoroutine(SP2_SkillCooldown());
        */
    }

    // �X�y�V�����X�L��2�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void SP2_SkillRelease()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        SP2_canUseSkill = false;
        StartCoroutine(SP2_SkillCooldown());
        */
    }
}