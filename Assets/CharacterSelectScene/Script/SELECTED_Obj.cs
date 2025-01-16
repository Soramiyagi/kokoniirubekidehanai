using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SELECTED_Obj : MonoBehaviour
{
    private float duration = 0.15f; // �X�P�[���_�E���̎���
    private Vector3 startScale = new Vector3(2f, 2f, 2f); // �ŏ��̃X�P�[��
    private Vector3 targetScale = new Vector3(1f, 1f, 1f); // �ڕW�Ƃ���X�P�[��

    void OnEnable()
    {
        // �����X�P�[����ݒ�
        transform.localScale = startScale;

        // �R���[�`�����J�n
        StartCoroutine(ScaleDownCoroutine());
    }

    IEnumerator ScaleDownCoroutine()
    {
        Vector3 initialScale = startScale; // ���݂̃X�P�[��
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // �X�P�[�������X�ɕύX
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            // ���̃t���[���܂őҋ@
            yield return null;
        }

        // �ŏI�I�Ƀ^�[�Q�b�g�X�P�[���ɐݒ�
        transform.localScale = targetScale;
    }
}
