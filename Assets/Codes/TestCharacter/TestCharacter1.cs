using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter1 : Player_Test
{
    public string characterName = "DefaultCharacter";

    // HPとスピードとジャンプ力、スキルのクールダウン時間を派生クラスで設定
    protected override int HP { get; set; } = 5; // 体力
    protected override float Speed { get; set; } = 2.0f; // スピード値
    protected override float JumpForce { get; set; } = 5.0f; // ジャンプ力
    protected override float A_SkillCooldownTime { get; set; } = 1.0f; // 攻撃スキルのクールダウン
    protected override float D_SkillCooldownTime { get; set; } = 1.0f; // 防御スキルのクールダウン
    protected override float SP1_SkillCooldownTime { get; set; } = 5.0f; // スペシャルスキル1のクールダウン
    protected override float SP2_SkillCooldownTime { get; set; } = 10.0f; // スペシャルスキル2のクールダウン

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // 攻撃スキルが押された時の処理をオーバーライド
    protected override void A_SkillPush()
    {
        /*
        発動タイミングが押したときなら使おう
        A_canUseSkill = false;
        StartCoroutine(A_SkillCooldown());
        */
    }

    // 攻撃スキルを離したときの処理をオーバーライド
    protected override void A_SkillRelease()
    {
        /*
        発動タイミングが離したときなら使おう
        A_canUseSkill = false;
        StartCoroutine(A_SkillCooldown());
        */
    }

    // 防御スキルが押された時の処理をオーバーライド
    protected override void D_SkillPush()
    {
        /*
        発動タイミングが押したときなら使おう
        D_canUseSkill = false;
        StartCoroutine(D_SkillCooldown());
        */
    }

    // 防御スキルを離したときの処理をオーバーライド
    protected override void D_SkillRelease()
    {
        /*
        発動タイミングが離したときなら使おう
        D_canUseSkill = false;
        StartCoroutine(D_SkillCooldown());
        */
    }

    // スペシャルスキル1が押された時の処理をオーバーライド
    protected override void SP1_SkillPush()
    {
        /*
        発動タイミングが押したときなら使おう
        SP1_canUseSkill = false;
        StartCoroutine(SP1_SkillCooldown());
        */
    }

    // スペシャルスキル1を離したときの処理をオーバーライド
    protected override void SP1_SkillRelease()
    {
        /*
        発動タイミングが離したときなら使おう
        SP1_canUseSkill = false;
        StartCoroutine(SP1_SkillCooldown());
        */
    }

    // スペシャルスキル2が押された時の処理をオーバーライド
    protected override void SP2_SkillPush()
    {
        /*
        発動タイミングが押したときなら使おう
        SP2_canUseSkill = false;
        StartCoroutine(SP2_SkillCooldown());
        */
    }

    // スペシャルスキル2を離したときの処理をオーバーライド
    protected override void SP2_SkillRelease()
    {
        /*
        発動タイミングが離したときなら使おう
        SP2_canUseSkill = false;
        StartCoroutine(SP2_SkillCooldown());
        */
    }
}