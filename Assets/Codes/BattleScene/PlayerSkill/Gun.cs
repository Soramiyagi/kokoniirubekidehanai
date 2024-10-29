using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject Shaft, BulletPoint, Bind, FixSphere;

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

        Shaft.transform.rotation = Quaternion.Euler(0.0f, 90 - R_angle,0.0f);
    }

    // スキル1が押された時の処理をオーバーライド
    protected override void Skill1Push()
    {
        /*
        発動タイミングが押したときなら使おう
        */
        Instantiate(Bind, BulletPoint.transform.position, Quaternion.Euler(0f, 90 - R_angle, 0f));

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
        Instantiate(FixSphere, this.transform.position, Quaternion.identity);

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