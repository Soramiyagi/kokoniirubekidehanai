using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

/*
LoadClientからLoadSceneにあるLoadServerへ、遷移したいシーン名を送る
LoadRquestをClientから送信して、Serverで受け取る感じのイメージ
 */

public class LoadClient : MonoBehaviour
{
    [SerializeField] private SceneAsset NextScene;
    private string nextSceneName;

    void Start()
    {
        nextSceneName = NextScene.name;
    }

    public void LoadStart()
    {
        LoadRequest.nextSceneName = nextSceneName;
        SceneManager.LoadScene("LoadScene");
    }
}
