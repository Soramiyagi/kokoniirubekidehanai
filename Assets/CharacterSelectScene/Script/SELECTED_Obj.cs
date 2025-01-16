using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SELECTED_Obj : MonoBehaviour
{
    private float duration = 0.15f; // スケールダウンの時間
    private Vector3 startScale = new Vector3(2f, 2f, 2f); // 最初のスケール
    private Vector3 targetScale = new Vector3(1f, 1f, 1f); // 目標とするスケール

    void OnEnable()
    {
        // 初期スケールを設定
        transform.localScale = startScale;

        // コルーチンを開始
        StartCoroutine(ScaleDownCoroutine());
    }

    IEnumerator ScaleDownCoroutine()
    {
        Vector3 initialScale = startScale; // 現在のスケール
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // スケールを徐々に変更
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            // 次のフレームまで待機
            yield return null;
        }

        // 最終的にターゲットスケールに設定
        transform.localScale = targetScale;
    }
}
