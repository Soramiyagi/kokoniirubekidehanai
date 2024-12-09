using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : Player
{
    [SerializeField] private GameObject Shaft;

    public string characterName = "DefaultCharacter";

    // プレハブを生成するための変数
    public GameObject prefab; // プレハブの参照をインスペクタで設定
    private GameObject spawnedPrefab; // 生成されたプレハブの参照

    private ShootingStar_SkillManager SSS;
    private Animator animator;//アニメーションをGetComponentする変数
    private float movementThreshold = 0.001f;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SSS = this.GetComponent<ShootingStar_SkillManager>();
        animator = GetComponent<Animator>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Shaft.transform.rotation = Quaternion.Euler(0.0f, 90 - R_angle, 0.0f);
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
        /*
        発動タイミングが押したときなら使おう
        */
        animator.SetTrigger("skill1");
        SSS.UseSkill1();

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
        animator.SetTrigger("skill2");
        SSS.UseSkill2();

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
        StartCoroutine(Skill2Cooldown());
        StartCoroutine(Skill2DuringAnima(false));
        */
    }

    // 1秒後にプレハブを削除するためのコルーチン
    private IEnumerator DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab); // プレハブを削除

        }
    }
}