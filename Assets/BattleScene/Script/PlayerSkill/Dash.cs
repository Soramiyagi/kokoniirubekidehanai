using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject clones, extendCollider, C_extendCollider;
    [SerializeField] private GameObject EndPoint;

    [HideInInspector] public bool walkingFlag = false, skill1Flag = false, skill2Flag = false;

    public float dashSpeed;
    private Vector3 currentPos, endPos;

    public ParticleSystem skill1ParticleSystem;
    public ParticleSystem skill2ParticleSystem;
    public ParticleSystem skill2ParticleSystem2;
    public ParticleSystem bindParticleSystem;

    private float dashTime = 1;

    //ET = EffectTime(���ʎ���)
    private float skill2_ET = 0;
    public float skill2_ET_Set = 0;
    private Vector3 previousPosition;
    private Animator animator;//�A�j���[�V������GetComponent����ϐ�
    private float movementThreshold = 0.001f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (systemStop == false)
        {
            if (dashTime < 1)
            {
                canMoveInput = false;

                dashTime += dashSpeed * Time.deltaTime;

                if (-2 <= this.transform.position.x && this.transform.position.x <= 22)
                {
                    if (-2 <= this.transform.position.z && this.transform.position.z <= 22)
                    {
                        this.transform.position = Vector3.Lerp(currentPos, endPos, dashTime);
                    }
                }
            }
            else if (dashTime >= 1)
            {
                if (canMoveInput == false)
                {
                    dashTime = 1.0f;

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

            if (floatTime > 0)
            {
                {
                    clones.SetActive(false);
                    C_extendCollider.SetActive(false);
                }
            }
        }
        float distanceMoved = Vector3.Distance(transform.position, previousPosition);

        // �ړ�������臒l�𒴂�����walking��true�ɂ���
        if (distanceMoved > movementThreshold)
        {
            animator.SetBool("walking", true);
            walkingFlag = true;
        }
        else
        {
            animator.SetBool("walking", false);
            walkingFlag = false;
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
    // �X�L��1�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill1Push()
    {
        animator.SetTrigger("skill1");
        skill1Flag = true;
        StartCoroutine(CloneSkill1());
        dashTime = 0;
        if (skill1ParticleSystem != null)
        {
            skill1ParticleSystem.Play();
        }
        extendCollider.SetActive(true);

        if (skill2_ET > 0)
        {
            C_extendCollider.SetActive(true);
        }

        currentPos = this.transform.position;
        endPos = EndPoint.transform.position;

        StartCoroutine(skill1DestroyPrefabAfterDelay(1f));

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima());
    }

    // �X�L��2�������ꂽ���̏������I�[�o�[���C�h
    protected override void Skill2Push()
    {
        animator.SetTrigger("skill2");
        skill2Flag = true;
        StartCoroutine(CloneSkill2());
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
        StartCoroutine(Skill2DuringAnima());
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

    private IEnumerator CloneSkill1()
    {
        yield return null;
        skill1Flag = false;
    }
    private IEnumerator CloneSkill2()
    {
        yield return null;
        skill2Flag = false;
    }
    private IEnumerator bindParticleDelay()
    {
        yield return new WaitForSeconds(1.5f);
        bindParticleSystem.Stop();
        bindParticleSystem.Clear();
    }
}