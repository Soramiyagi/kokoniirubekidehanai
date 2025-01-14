using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject Skill1Preview;

    public ParticleSystem skill1ParticleSystem;
    public ParticleSystem skill2ParticleSystem;
    public ParticleSystem bindParticleSystem;


    // プレハブを生成するための変数
    public GameObject skill1Prehub; // スキル1のプレハブ
    public GameObject skill2Prehub; // スキル2のプレハブ
    private GameObject spawnedPrefab; // 生成されたプレハブの参照

    private bool previewSkill1 = false; // skill1のプレビューのためのフラグ

    private bool skill2StateCheck = false;

    private float movementThreshold = 0.001f;

    private Animator animator;//アニメーションをGetComponentする変数

    // 前フレームの位置を記録する変数
    private Vector3 previousPosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Skill1Preview.SetActive(false);
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
        //animator.SetBool("walking", true);//walkingをtureにする
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (previewSkill1 && spawnedPrefab != null)
        {
            // プレイヤーの位置に追従 (Y軸を-0.5してプレイヤーの下に置く)
            Vector3 followPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z + 4);
            spawnedPrefab.transform.position = followPosition;
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

    protected override void jumping()
    {

        animator.SetTrigger("jumping");
    }

    protected override void Landing()
    {

        animator.SetTrigger("Landing");
    }
    protected override void Binding()
    {
        if (bindParticleSystem != null)
        {
            bindParticleSystem.Play();
        }

        StartCoroutine(bindParticleDelay());
    }
    protected override void Skill1Push()
    {
        //walkingをtureにする        
        animator.SetTrigger("skill1");
        Skill1Preview.SetActive(true);

        // 生成されたパーティクルインスタンスを再生
        if (skill1ParticleSystem != null)
        {
            skill1ParticleSystem.Play();
        }
        StartCoroutine(Skill1DestroyPrefabAndParticlesAfterDelay(0.1f, 1f));

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
        StartCoroutine(Skill1DuringAnima());
    }

    // スキル2が押されている間の処理をオーバーライド
    protected override void Skill2Push()
    {
        if (skill2StateCheck == false)
        {
            animator.SetTrigger("skill2Put");
            if (spawnedPrefab == null)
            {
                // プレイヤーの位置からY軸を-0.5した位置にプレハブを生成
                Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                spawnedPrefab = Instantiate(skill2Prehub, spawnPosition, Quaternion.identity);

                StartCoroutine(Skill2DuringAnima());

                // コリジョンをオフにする処理
                Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();
                if (prefabCollider != null)
                {
                    prefabCollider.enabled = false; // コリジョンをオフに
                }

            }
            skill2StateCheck = true;
        }
        else if (skill2StateCheck == true)
        {
            if (spawnedPrefab != null)
            {
                animator.SetTrigger("skill2Kidou");
                // プレハブをその位置に固定し、コリジョンをオンにする
                Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();
                if (prefabCollider != null)
                {
                    prefabCollider.enabled = true; // コリジョンをオンに
                }

                // パーティクルを生成して再生
                if (skill2ParticleSystem != null)
                {
                    // パーティクルシステムを生成
                    ParticleSystem particleInstance = Instantiate(skill2ParticleSystem, spawnedPrefab.transform.position, Quaternion.identity);
                    particleInstance.Play(); // パーティクルを再生
                }

                // プレハブの削除処理
                StartCoroutine(Skill2DestroyPrefabAndParticlesAfterDelay(1f, 2f));
            }

            skill2StateCheck = false;

            // クールダウン処理
            canUseSkill2 = false;
            StartCoroutine(Skill2Cooldown());
            StartCoroutine(Skill2DuringAnima());
        }
    }

    private IEnumerator Skill1DestroyPrefabAndParticlesAfterDelay(float delay, float delay2)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        Skill1Preview.SetActive(false);
        Collider previewCollider = Skill1Preview.GetComponent<Collider>();
        yield return new WaitForSeconds(delay2); // 指定した秒数待機
        if (skill1ParticleSystem != null)
        {
            skill1ParticleSystem.Stop();
            skill1ParticleSystem.Clear();
        }
    }

    // 1秒後にプレハブを削除するためのコルーチン
    private IEnumerator Skill2DestroyPrefabAndParticlesAfterDelay(float delay, float delay2)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab); // プレハブを削除
        }
        yield return new WaitForSeconds(delay2);
        if (skill2ParticleSystem != null)
        {
            skill2ParticleSystem.Stop(); // パーティクル停止
            skill2ParticleSystem.Clear(); // パーティクルをクリア
            skill2ParticleSystem.transform.SetParent(transform, true); // プレイヤーの子オブジェクトに戻す
            skill2ParticleSystem.transform.localPosition = Vector3.zero; // 元の位置に戻す（必要に応じて調整）
        }
    }

    private IEnumerator bindParticleDelay()
    {
        yield return new WaitForSeconds(1.5f);
        bindParticleSystem.Stop();
        bindParticleSystem.Clear();
    }

    private void OnDestroy()
    {
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab);
        }
    }
}