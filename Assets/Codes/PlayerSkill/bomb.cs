using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Player
{
    public string characterName = "DefaultCharacter";

    [SerializeField] private GameObject Skill1Preview;

    // スピードとジャンプ力、スキルのクールダウン時間を派生クラスで設定
    protected override float Speed { get; set; } = 2.0f; // スピード値
    protected override float JumpForce { get; set; } = 5.0f; // ジャンプ力
    protected override float Skill1CooldownTime { get; set; } = 4.0f; // スキル1のクールダウン
    protected override float Skill2CooldownTime { get; set; } = 9.0f; // スキル2のクールダウン

    // プレハブを生成するための変数
    public GameObject skill2Prehub; // スキル2のプレハブ
    private GameObject spawnedPrefab; // 生成されたプレハブの参照

    private bool previewSkill1 = false; // skill1のプレビューのためのフラグ

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Skill1Preview.SetActive(false);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Debug.Log(moveHorizontal);

        if (previewSkill1 && spawnedPrefab != null)
        {
            // プレイヤーの位置に追従 (Y軸を-0.5してプレイヤーの下に置く)
            Vector3 followPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z + 4);
            spawnedPrefab.transform.position = followPosition;
            Debug.Log("Skill1 prefab is following the player.");

        }
    }

    protected override void Skill1Push()
    {
        Skill1Preview.SetActive(true);
        Collider previewCollider = Skill1Preview.GetComponent<Collider>();
        if (previewCollider != null)
        {
            previewCollider.enabled = false; // コリジョンをオフに
        }
    }


    protected override void Skill1Release()
    {
        Collider previewCollider = Skill1Preview.GetComponent<Collider>();
        if (previewCollider != null)
        {
            Debug.Log("aa");
            previewCollider.enabled = true; // コリジョンをオンに
        }
        StartCoroutine(DestroyPrefabAfterDelay(0.1f));

        canUseSkill1 = false;
        StartCoroutine(Skill1Cooldown());
    }

    // スキル2が押されている間の処理をオーバーライド
    protected override void Skill2Push()
    {
        if (spawnedPrefab == null)
        {
            // プレイヤーの位置からY軸を-0.5した位置にプレハブを生成
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            spawnedPrefab = Instantiate(skill2Prehub, spawnPosition, Quaternion.identity);

            // コリジョンをオフにする処理
            Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();
            if (prefabCollider != null)
            {
                prefabCollider.enabled = false; // コリジョンをオフに
            }

            Debug.Log("Bomb is charging Skill 2! Prefab instantiated at adjusted position.");
        }
        else
        {
            // プレイヤーの位置に追従させる (Y軸を-0.5してプレイヤーの下に置く)
            Vector3 followPosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            spawnedPrefab.transform.position = followPosition;
            Debug.Log("Prefab is following the player.");
        }
    }

    // スキル2を発動する処理をオーバーライド
    protected override void Skill2Release()
    {
        if (spawnedPrefab != null)
        {
            // プレハブをその位置に固定し、コリジョンをオンにする
            Collider prefabCollider = spawnedPrefab.GetComponent<Collider>();
            if (prefabCollider != null)
            {
                prefabCollider.enabled = true; // コリジョンをオンに
            }

            // プレハブを削除する処理
            StartCoroutine(DestroyPrefabAfterDelay(0.1f));
        }

        // クールダウン処理
        canUseSkill2 = false;
        StartCoroutine(Skill2Cooldown());
    }

    // 1秒後にプレハブを削除するためのコルーチン
    private IEnumerator DestroyPrefabAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 指定した秒数待機
        if (spawnedPrefab != null)
        {
            Destroy(spawnedPrefab); // プレハブを削除
        }
        Skill1Preview.SetActive(false);
    }
}