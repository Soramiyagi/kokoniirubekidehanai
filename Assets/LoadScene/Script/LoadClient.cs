using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadClient : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // シーン名を直接入力

    public void LoadStart()
    {
        LoadRequest.nextSceneName = nextSceneName;
        SceneManager.LoadScene("LoadScene");
    }
}


