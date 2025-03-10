using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject wanderer;

    //ET = EffectTime(効果時間)
    private float skill2_ET = 0;
    public float skill2_ET_Set = 0;

    private float downStop = 0;

    public ParticleSystem particleSystem;
    public ParticleSystem bindParticleSystem;
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
        
        if (floatTime <= 0)
        {
            if (skill2_ET > 0)
            {
                skill2_ET -= Time.deltaTime;

                float Pos_X = this.transform.position.x;
                float Pos_Z = this.transform.position.z;

                rb.velocity = Vector3.zero;

                this.transform.position = new Vector3(Pos_X, downStop, Pos_Z);
            }
            else
            {
                rb.useGravity = true;
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
        /*
        発動タイミングが押したときなら使おう
        */

        animator.SetTrigger("skill1");
        Instantiate(wanderer, this.transform.position, Quaternion.identity);


        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima());

        PlaySoundEffect(SE[1]);

    }

    // スキル2が押された時の処理をオーバーライド
    protected override void Skill2Push()
    {
        /*
        発動タイミングが押したときなら使おう
        */
        animator.SetBool("skill2", true);
        skill2_ET = skill2_ET_Set;
        isGrounded = false;

        downStop = this.transform.position.y + 1f;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        canUseSkill2 = false;
        PlayParticles();
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());
        PlaySoundEffect(SE[2]);
        StartCoroutine(DestroyPrefabAfterDelay(5.5f));
    }

    protected override void Jumping()
    {
        animator.SetTrigger("jumping");
    }

    protected override void Landing()
    {
        animator.SetTrigger("Landing");
    }

    // パーティクルを再生するメソッド
    public void PlayParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }

    // パーティクルを停止するメソッド
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
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        StopParticles();
        animator.SetBool("skill2", false);
    }

    private IEnumerator BindParticleDelay(float time)
    {
        yield return new WaitForSeconds(time);
        bindParticleSystem.Stop();
        bindParticleSystem.Clear();

    }
}