using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject clones, extendCollider, cloneExtendCollider;
    [SerializeField] private GameObject endPoint;

    [HideInInspector] public bool walkingFlag = false, skill1Flag = false, skill2Flag = false;

    public float dashSpeed;
    private Vector3 currentPos, endPos;

    public ParticleSystem skill1ParticleSystem;
    public ParticleSystem skill2ParticleSystem;
    public ParticleSystem skill2ParticleSystem2;
    public ParticleSystem bindParticleSystem;

    private float dashTime = 1;

    //ET = EffectTime(効果時間)
    private float skill2EffectTime = 0;
    public float skill2EffectTimeSet = 0;
    private Vector3 previousPosition;
    private Animator animator;//アニメーションをGetComponentする変数
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
                    cloneExtendCollider.SetActive(false);
                }
            }

            if (skill2EffectTime > 0)
            {
                skill2EffectTime = skill2EffectTime - Time.deltaTime;
            }
            else
            {
                clones.SetActive(false);
                cloneExtendCollider.SetActive(false);
            }

            if (floatTime > 0)
            {
                {
                    clones.SetActive(false);
                    cloneExtendCollider.SetActive(false);
                }
            }
        }
        float distanceMoved = Vector3.Distance(transform.position, previousPosition);

        // 移動距離が閾値を超えたらwalkingをtrueにする
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

    protected override void Jumping()
    {

        animator.SetTrigger("jumping");
    }

    protected override void Landing()
    {

        animator.SetTrigger("Landing");
    }

    protected override void Binding(bool super)
    {
        if (super == true)
        {
            if (bindParticleSystem != null)
            {
                bindParticleSystem.startColor = Color.red;
                bindParticleSystem.Play();
            }

            StartCoroutine(BindParticleDelay(2.5f));
        }
        else
        {
            if (bindParticleSystem != null)
            {
                bindParticleSystem.startColor = Color.blue;
                bindParticleSystem.Play();
            }

            StartCoroutine(BindParticleDelay(1.5f));
        }
    }

    // スキル1が押された時の処理をオーバーライド
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

        if (skill2EffectTime > 0)
        {
            cloneExtendCollider.SetActive(true);
        }

        currentPos = this.transform.position;
        endPos = endPoint.transform.position;

        StartCoroutine(Skill1DestroyPrefabAfterDelay(1f));

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima());

        PlaySoundEffect(SE[1]);
    }

    // スキル2が押された時の処理をオーバーライド
    protected override void Skill2Push()
    {
        animator.SetTrigger("skill2");
        skill2Flag = true;
        StartCoroutine(CloneSkill2());
        clones.SetActive(true);
        skill2EffectTime = skill2EffectTimeSet;
        if (skill2ParticleSystem != null && skill2ParticleSystem2 != null)
        {
            skill2ParticleSystem.Play();
            skill2ParticleSystem2.Play();
        }
        StartCoroutine(Skill2DestroyPrefabAfterDelay(2f));

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());

        PlaySoundEffect(SE[2]);
    }

    private IEnumerator Skill1DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        if (skill1ParticleSystem != null)
        {
            skill1ParticleSystem.Stop();
            skill1ParticleSystem.Clear();
        }
    }

    private IEnumerator Skill2DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
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
    private IEnumerator BindParticleDelay(float time)
    {
        yield return new WaitForSeconds(time);
        bindParticleSystem.Stop();
        bindParticleSystem.Clear();
    }
}