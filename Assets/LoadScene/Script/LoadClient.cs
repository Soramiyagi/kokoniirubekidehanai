using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

/*
LoadClient����LoadScene�ɂ���LoadServer�ցA�J�ڂ������V�[�����𑗂�
LoadRquest��Client���瑗�M���āAServer�Ŏ󂯎�銴���̃C���[�W
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
