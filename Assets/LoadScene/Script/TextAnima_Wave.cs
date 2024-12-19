using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnima_Wave : MonoBehaviour
{
    private TMP_Text textMeshPro;
    private TMP_TextInfo textInfo;
    [SerializeField] private float waveSpeed = 0f;
    [SerializeField] private float waveHeight = 0f;
    [SerializeField] private float waveInterval = 0f;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = this.GetComponent<TMP_Text>();
    }

    void FixedUpdate()
    {
        textMeshPro.ForceMeshUpdate(); // テキストメッシュを強制的に更新
        var textInfo = textMeshPro.textInfo; // 最新のtextInfoを取得

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible) continue;
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            float waveOffset = Mathf.Sin(Time.time * waveSpeed + i * waveInterval) * waveHeight;

            if (waveOffset >= 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    verts[charInfo.vertexIndex + j] = new Vector3(verts[charInfo.vertexIndex + j].x, verts[charInfo.vertexIndex + j].y + waveOffset, verts[charInfo.vertexIndex + j].z);
                }
            }
            /*else
            {
                for (int j = 0; j < 4; j++)
                {
                    verts[charInfo.vertexIndex + j] = new Vector3(verts[charInfo.vertexIndex + j].x, 0, verts[charInfo.vertexIndex + j].z);
                }
            }*/
        }


        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
    }
}