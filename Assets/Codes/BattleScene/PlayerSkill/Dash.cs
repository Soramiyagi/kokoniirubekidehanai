using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject clones, extendCollider, C_extendCollider;
    [SerializeField] private GameObject EndPoint;

    public float dashSpeed;
    private Vector3 currentPos, endPos;

    public ParticleSystem skill1ParticleSystem;
    public ParticleSystem skill2ParticleSystem;
    public ParticleSystem skill2ParticleSystem2;

    private float dashTime = 1;

    //ET = EffectTime(効果時間)
    private float skill2_ET = 0;
    public float skill2_ET_Set = 0;
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

                this.transform.position = Vector3.Lerp(currentPos, endPos, dashTime);
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
        }
        float distanceMoved = Vector3.Distance(transform.position, previousPosition);

        // 移動距離が閾値を超えたらwalkingをtrueにする
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

    // スキル1が押された時の処理をオーバーライド
    protected override void Skill1Push()
    {
        animator.SetTrigger("skill1");
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
        StartCoroutine(Skill1DuringAnima(true));
    }

    // スキル1を離したときの処理をオーバーライド
    protected override void Skill1Release()
    {
        /*
        発動タイミングが離したときなら使おう
        canUseSkill1 = false;
        duringAnima = true;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima(false));

        //リリース時に発動するスキルなら必要
        if (Skill1PushCheck == true)
        {
            //スキル処理の記述
            Skill1PushCheck = false;
        }
        */
    }

    // スキル2が押された時の処理をオーバーライド
    protected override void Skill2Push()
    {
        animator.SetTrigger("skill2");
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
        StartCoroutine(Skill2DuringAnima(true));
    }

    // スキル2を離したときの処理をオーバーライド
    protected override void Skill2Release()
    {
        /*
        発動タイミングが離したときなら使おう
        canUseSkill2 = false;
        duringAnima = true;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima(false));

        //リリース時に発動するスキルなら必要
        if (Skill2PushCheck == true)
        {
            //スキル処理の記述
            Skill2PushCheck = false;
        }
        */
    }

    private IEnumerator skill1DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        if (skill1ParticleSystem != null)
        {
            skill1ParticleSystem.Stop();
            skill1ParticleSystem.Clear();

        }
    }

    private IEnumerator skill2DestroyPrefabAfterDelay(float delay)
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
    
}