using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon: Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject wanderer;

    // �X�s�[�h�ƃW�����v�́A�X�L���̃N�[���_�E�����Ԃ�h���N���X�Őݒ�
    protected override float Speed { get; set; } = 2.0f; // �X�s�[�h�l
    protected override float JumpForce { get; set; } = 5.0f; // �W�����v��
    protected override float Skill1CooldownTime { get; set; } = 4.0f; // �X�L��1�̃N�[���_�E��
    protected override float Skill2CooldownTime { get; set; } = 9.0f; // �X�L��2�̃N�[���_�E��

    //ET = EffectTime(���ʎ���)
    private float skill2_ET = 0;
    public float skill2_ET_Set = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (skill2_ET > 0)
        {
            skill2_ET = skill2_ET - Time.deltaTime;
            Vector3 newPosition = this.transform.position;
            if (this.transform.position.y <= 2.5f)
            {
                newPosition.y = 2.5f;
            }
            transform.position = newPosition;
        }
        else
        {
            isGrounded = true;
        }
    }

    // �X�L��1�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill1Push()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */

        Instantiate(wanderer, this.transform.position, Quaternion.identity);

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        
    }

    // �X�L��1�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void Skill1Release()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        */
    }

    // �X�L��2�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill2Push()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        */
        skill2_ET = skill2_ET_Set;
        isGrounded = false;

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
    }

    // �X�L��2�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void Skill2Release()
    {
        /*
        �����^�C�~���O���������Ƃ��Ȃ�g����
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        */
    }
}