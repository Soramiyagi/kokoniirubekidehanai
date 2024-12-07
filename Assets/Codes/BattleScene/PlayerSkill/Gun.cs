using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject Shaft, BulletPoint, Bind, FixSphere;

    public ParticleSystem particleSystem;
   private Animator animator;//アニメーションをGetComponentする変数
    private Vector3 previousPosition;
    private float movementThreshold = 0.001f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        //animator.SetBool("walking", true);//walkingをtureにする
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Shaft.transform.rotation = Quaternion.Euler(0.0f, 90 - R_angle,0.0f);

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
        /*
        発動タイミングが押したときなら使おう
        */
        Instantiate(Bind, BulletPoint.transform.position, Quaternion.Euler(0f, 90 - R_angle, 0f));

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
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima(false));
        */
    }

    // スキル2が押された時の処理をオーバーライド
    protected override void Skill2Push()
    {
        /*
        発動タイミングが押したときなら使おう
        */
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        animator.SetTrigger("skill2");
        Instantiate(FixSphere, this.transform.position, Quaternion.identity);

        // プレイヤーの位置にパーティクルを生成して再生
        ParticleSystem particleInstance = Instantiate(particleSystem, this.transform.position, Quaternion.identity);
        particleInstance.Play();

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima(true));

        // 一定時間後にパーティクルを停止・削除
        StartCoroutine(DestroyParticleAfterDelay(particleInstance, 1f));
    }

    // スキル2を離したときの処理をオーバーライド
    protected override void Skill2Release()
    {
        /*
        発動タイミングが離したときなら使おう
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima(false));
        */
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
}