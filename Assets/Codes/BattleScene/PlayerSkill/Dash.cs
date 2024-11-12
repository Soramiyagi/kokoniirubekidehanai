using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject clones, extendCollider, C_extendCollider;

    // スピードとジャンプ力、スキルのクールダウン時間を派生クラスで設定
    protected override float Speed { get; set; } = 2.0f; // スピード値
    protected override float JumpForce { get; set; } = 5.0f; // ジャンプ力
    protected override float Skill1CooldownTime { get; set; } = 4.0f; // スキル1のクールダウン
    protected override float Skill2CooldownTime { get; set; } = 9.0f; // スキル2のクールダウン

    public float forceAmount = 10f; // 加える力の大きさ(ダッシュの早さ)

    public ParticleSystem skill1ParticleSystem;
    public ParticleSystem skill2ParticleSystem;
    public ParticleSystem skill2ParticleSystem2;
    //ET = EffectTime(効果時間)
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
                // オブジェクトの速度と角速度をゼロに設定
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

    // スキル1が押された時の処理をオーバーライド
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
        // 力のベクトルを計算
        Vector3 force = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * forceAmount;

        // 指定した角度方向に力を加える
        rb.AddForce(force, ForceMode.Impulse);
        StartCoroutine(skill1DestroyPrefabAfterDelay(1f));
        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
    }

    // スキル1を離したときの処理をオーバーライド
    protected override void Skill1Release()
    {
    }

    // スキル2が押された時の処理をオーバーライド
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




    // スキル2を離したときの処理をオーバーライド
    protected override void Skill2Release()
    {
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