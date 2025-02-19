using UnityEngine;

public class FpsSetting : MonoBehaviour
{
    void Start()
    {
        // フレームレートを60に設定
        Application.targetFrameRate = 60;
    }
}