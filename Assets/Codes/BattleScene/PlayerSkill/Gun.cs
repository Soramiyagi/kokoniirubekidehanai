using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Player
{
    public string characterName = "DefaultCharacter";


    [SerializeField] private GameObject BulletPoint, Bind, Gun_FixSphere;

    public ParticleSystem particleSystem;
    private Animator animator;//アニメーションをGetComponentする変数
    private Vector3 previousPosition;
    private float movementThreshold = 0.001f;

    private bool Skill1Delay = false;
    private bool Skill2Delay = false;

    public float skill1InstantiateInterval = 0;


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
        if (Skill1Delay)//skill1の使用
        {
            
            Instantiate(Bind, BulletPoint.transform.position, Quaternion.Euler(0f, 90 - R_angle, 0f));

            canUseSkill1 = false;
            Skill1Delay=false;
            StartCoroutine(Skill1Cooldown());
            StartCoroutine(Skill1DuringAnima());
       
        }
        previousPosition = transform.position;
        Debug.Log(Skill1Delay);


    }

    // スキル1が押された時の処理をオーバーライド
    protected override void Skill1Push()
    {
        animator.SetTrigger("skill1");
        
        /*
        発動タイミングが押したときなら使おう
        */

       
        StartCoroutine(Skill1DelaySystem(0.2f));

    }

    // スキル1を離したときの処理をオーバーライド


    // スキル2が押された時の処理をオーバーライド
    protected override void Skill2Push()
    {
        /*
        発動タイミングが押したときなら使おう
        */
        animator.SetTrigger("skill2");
        Instantiate(Gun_FixSphere, this.transform.position, Quaternion.identity);


        if (Skill2Delay)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);


            Instantiate(Gun_FixSphere, this.transform.position, Quaternion.identity);

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());


            // プレイヤーの位置にパーティクルを生成して再生
            ParticleSystem particleInstance = Instantiate(particleSystem, this.transform.position, Quaternion.identity);
            particleInstance.Play();

            canUseSkill2 = false;
            StartCoroutine(Skill2Cooldown());
            StartCoroutine(Skill2DuringAnima());

            // 一定時間後にパーティクルを停止・削除
            StartCoroutine(DestroyParticleAfterDelay(particleInstance, 1f));
        }
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
        yield return new WaitForSeconds(delay);
        
        Skill1Delay = true;
    }
    private IEnumerator Skill2DelaySystem(bool Skill2Delay, float delay)
    {
        yield return new WaitForSeconds(delay);
        Skill2Delay = true;
    }

    private IEnumerator Skill1Instantiate(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        Instantiate(Bind, BulletPoint.transform.position, Quaternion.Euler(0f, 90 - R_angle, 0f));
    }
}