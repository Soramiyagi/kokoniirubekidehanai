using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadClient : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // ƒV[ƒ“–¼‚ğ’¼Ú“ü—Í

    public void LoadStart()
    {
        LoadRequest.nextSceneName = nextSceneName;
        SceneManager.LoadScene("LoadScene");
    }
}


