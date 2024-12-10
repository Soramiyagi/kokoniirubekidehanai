using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalData : Player
{
    public string characterName = "DefaultCharacter";

    // スピードとジャンプ力、スキルのクールダウン時間を派生クラスで設定
    protected override float Speed { get; set; } = 2.0f; // スピード値
    protected override float JumpForce { get; set; } = 5.0f; // ジャンプ力
    protected override float Skill1CooldownTime { get; set; } = 4.0f; // スキル1のクールダウン
    protected override float Skill2CooldownTime { get; set; } = 9.0f; // スキル2のクールダウン

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // スキル1が押された時の処理をオーバーライド
    protected override void Skill1Push()
    {
        /*
        発動タイミングが押したときなら使おう
        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima());

        //リリース時に発動するスキルなら必要
        Skill1PushCheck = true;
        */
    }

    // スキル2が押された時の処理をオーバーライド
    protected override void Skill2Push()
    {
        /*
        発動タイミングが押したときなら使おう
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima());

        //リリース時に発動するスキルなら必要
        Skill2PushCheck = true;
        */
    }
}