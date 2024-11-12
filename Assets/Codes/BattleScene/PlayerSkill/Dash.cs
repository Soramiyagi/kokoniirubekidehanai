using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject clones, extendCollider, C_extendCollider;

    // �X�s�[�h�ƃW�����v�́A�X�L���̃N�[���_�E�����Ԃ�h���N���X�Őݒ�
    protected override float Speed { get; set; } = 2.0f; // �X�s�[�h�l
    protected override float JumpForce { get; set; } = 5.0f; // �W�����v��
    protected override float Skill1CooldownTime { get; set; } = 4.0f; // �X�L��1�̃N�[���_�E��
    protected override float Skill2CooldownTime { get; set; } = 9.0f; // �X�L��2�̃N�[���_�E��

    public float forceAmount = 10f; // ������͂̑傫��(�_�b�V���̑���)

    public ParticleSystem skill1ParticleSystem;
    public ParticleSystem skill2ParticleSystem;
    public ParticleSystem skill2ParticleSystem2;
    //ET = EffectTime(���ʎ���)
    private float skill1_ET = 0;
    private float skill2_ET = 0;
    public float skill1_ET_Set = 0;
    public float skill2_ET_Set = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (skill1_ET > 0)
        {
            skill1_ET = skill1_ET - Time.deltaTime;

            float downStop = this.transform.position.y;

            float Pos_X = this.transform.position.x;
            float Pos_Z = this.transform.position.z;

            this.transform.position = new Vector3(Pos_X, downStop, Pos_Z);
        }
        else
        {
            if (canMoveInput == false)
            {
                // �I�u�W�F�N�g�̑��x�Ɗp���x���[���ɐݒ�
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                canMoveInput = true;

                extendCollider.SetActive(false);
                C_extendCollider.SetActive(false);
            }
        }

        if (skill2_ET > 0)
        {
            skill2_ET = skill2_ET - Time.deltaTime;
        }
        else
        {
            clones.SetActive(false);
            C_extendCollider.SetActive(false);
        }
    }

    // �X�L��1�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill1Push()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z);
        canMoveInput = false;

        skill1_ET = skill1_ET_Set;
        if (skill1ParticleSystem != null)
        {
            skill1ParticleSystem.Play();
        }
        extendCollider.SetActive(true);

        if (skill2_ET > 0)
        {
            C_extendCollider.SetActive(true);
        }

        float radians = L_angle * Mathf.Deg2Rad;
        // �͂̃x�N�g�����v�Z
        Vector3 force = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * forceAmount;

        // �w�肵���p�x�����ɗ͂�������
        rb.AddForce(force, ForceMode.Impulse);
        StartCoroutine(skill1DestroyPrefabAfterDelay(1f));
        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
    }

    // �X�L��1�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void Skill1Release()
    {
    }

    // �X�L��2�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill2Push()
    {
        clones.SetActive(true);
        skill2_ET = skill2_ET_Set;
        if (skill2ParticleSystem != null && skill2ParticleSystem2 != null)
        {
            skill2ParticleSystem.Play();
            skill2ParticleSystem2.Play();
        }
        StartCoroutine(skill2DestroyPrefabAfterDelay(2f));
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
 
    }




    // �X�L��2�𗣂����Ƃ��̏������I�[�o�[���C�h
    protected override void Skill2Release()
    {
    }

    private IEnumerator skill1DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        if (skill1ParticleSystem != null)
        {
            skill1ParticleSystem.Stop();
            skill1ParticleSystem.Clear();

        }
    }

    private IEnumerator skill2DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        if (skill2ParticleSystem != null)
        {
            skill2ParticleSystem.Stop();
            skill2ParticleSystem.Clear();
        }
        if (skill2ParticleSystem2 != null)
        {
            skill2ParticleSystem2.Stop();
            skill2ParticleSystem2.Clear();
        }
    }
    
}