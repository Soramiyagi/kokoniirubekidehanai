using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject bulletPoint, bindBullets, gunFixSphere;

    public ParticleSystem particleSystem; 
    public ParticleSystem bindParticleSystem;

    private GameObject bind;
    private Animator animator;//アニメーションをGetComponentする変数
    private Vector3 previousPosition;
    private float movementThreshold = 0.001f;

    public float skill1InstantiateInterval = 0;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        bind = Instantiate(bindBullets, this.transform.position, Quaternion.identity);
        bind.SetActive(false);

        //animator.SetBool("walking", true);//walkingをtureにする
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

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
       
        StartCoroutine(Skill1DelaySystem(0.2f));

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
        animator.SetTrigger("skill2");

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        Instantiate(gunFixSphere, this.transform.position, Quaternion.identity);

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());

        // プレイヤーの位置にパーティクルを生成して再生
        ParticleSystem particleInstance = Instantiate(particleSystem, this.transform.position, Quaternion.identity);
        particleInstance.Play();

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());

        PlaySoundEffect(SE[2]);

        // 一定時間後にパーティクルを停止・削除
        StartCoroutine(DestroyParticleAfterDelay(particleInstance, 1f));

    }

    // パーティクルを再生するメソッド
    public void PlayParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }

    private IEnumerator DestroyParticleAfterDelay(ParticleSystem particleInstance, float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        particleInstance.Stop();
        particleInstance.Clear();
        Destroy(particleInstance.gameObject); // パーティクルオブジェクトを削除
     
    }

    private IEnumerator Skill1DelaySystem(float delay)
    {
        bind.SetActive(false);
        yield return new WaitForSeconds(delay);
        
        //Instantiate(bind, bulletPoint.transform.position, Quaternion.Euler(0f, 90 - rightAngle, 0f));

        bind.transform.position = bulletPoint.transform.position;
        bind.transform.rotation = Quaternion.Euler(0f, 90 - rightAngle, 0f);
        bind.SetActive(true);
    }

    private IEnumerator Skill2DelaySystem(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    private IEnumerator BindParticleDelay(float time)
    {
        yield return new WaitForSeconds(time);
        bindParticleSystem.Stop();
        bindParticleSystem.Clear();

    }
}