using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject wanderer;

    //ET = EffectTime(���ʎ���)
    private float skill2_ET = 0;
    public float skill2_ET_Set = 0;

    private float downStop = 0;

    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Debug.Log(this.transform.position);

        if (skill2_ET > 0)
        {
            skill2_ET -= Time.deltaTime;

            float Pos_X = this.transform.position.x;
            float Pos_Z = this.transform.position.z;

            this.transform.position = new Vector3(Pos_X, downStop, Pos_Z);
        }
        else
        {
            rb.useGravity = true;
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
        StartCoroutine(Skill1DuringAnima(true));

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
        skill2_ET = skill2_ET_Set;
        isGrounded = false;

        downStop = this.transform.position.y + 0.5f;

        rb.useGravity = false;

        canUseSkill2 = false;
        PlayParticles();
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima(true));
        StartCoroutine(DestroyPrefabAfterDelay(6f));
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

    // �p�[�e�B�N�����~���郁�\�b�h
    public void StopParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Stop();
            particleSystem.Clear();
        }
    }

    private IEnumerator DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �w�肵���b���ҋ@
        StopParticles();
    }
}