using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon: Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject wanderer;

    // スピードとジャンプ力、スキルのクールダウン時間を派生クラスで設定
    protected override float Speed { get; set; } = 2.0f; // スピード値
    protected override float JumpForce { get; set; } = 5.0f; // ジャンプ力
    protected override float Skill1CooldownTime { get; set; } = 4.0f; // スキル1のクールダウン
    protected override float Skill2CooldownTime { get; set; } = 9.0f; // スキル2のクールダウン

    //ET = EffectTime(効果時間)
    private float skill2_ET = 0;
    public float skill2_ET_Set = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (skill2_ET > 0)
        {
            skill2_ET = skill2_ET - Time.deltaTime;
            Vector3 newPosition = this.transform.position;
            if (this.transform.position.y <= 2.5f)
            {
                newPosition.y = 2.5f;
            }
            transform.position = newPosition;
        }
        else
        {
            isGrounded = true;
        }
    }

    // スキル1が押された時の処理をオーバーライド
    protected override void Skill1Push()
    {
        /*
        発動タイミングが押したときなら使おう
        */

        Instantiate(wanderer, this.transform.position, Quaternion.identity);

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        
    }

    // スキル1を離したときの処理をオーバーライド
    protected override void Skill1Release()
    {
        /*
        発動タイミングが離したときなら使おう
        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        */
    }

    // スキル2が押された時の処理をオーバーライド
    protected override void Skill2Push()
    {
        /*
        発動タイミングが押したときなら使おう
        */
        skill2_ET = skill2_ET_Set;
        isGrounded = false;

        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
    }

    // スキル2を離したときの処理をオーバーライド
    protected override void Skill2Release()
    {
        /*
        発動タイミングが離したときなら使おう
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        */
    }
}