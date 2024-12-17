using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

/*
LoadServerから送られるLoadRequestをもとに、遷移先シーンを決める
 */

public class LoadServer : MonoBehaviour
{
    [SerializeField] private float loadTime;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1.0f;
        }
        sceneName = LoadRequest.nextSceneName;

        if (!string.IsNullOrEmpty(sceneName))
        {
            // コルーチンを開始
            StartCoroutine(LoadScene(sceneName));
        }
    }

    private IEnumerator LoadScene(string sceneToLoad)
    {
        var async = SceneManager.LoadSceneAsync(sceneToLoad);

        async.allowSceneActivation = false;
        yield return new WaitForSeconds(loadTime);
        async.allowSceneActivation = true;
    }
}
