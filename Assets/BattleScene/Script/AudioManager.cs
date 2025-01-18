using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgmSource;       // BGM用AudioSource
    [SerializeField] private AudioSource sfxSource;       // 効果音用AudioSource
    [SerializeField] private AudioClip[] soundEffects;    // 効果音リスト

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されない
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // BGMを再生する
    public void PlayBGM(AudioClip bgm)
    {
        if (bgmSource.clip != bgm)
        {
            bgmSource.clip = bgm;
            bgmSource.Play();
        }
    }

    // BGMを停止する
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PauseBGM() 
    {
        bgmSource.Pause(); 
    }

    public void ResumeBGM() 
    {
        bgmSource.UnPause(); 
    }

    // 効果音を再生（名前で指定）
    public void PlaySFX(string clipName)
    {
        foreach (var clip in soundEffects)
        {
            if (clip.name == clipName)
            {
                sfxSource.PlayOneShot(clip);
                return;
            }
        }
        Debug.LogWarning($"Sound effect '{clipName}' not found!");
    }
    
    // BGMの音量を変更する
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp01(volume); // 0～1に制限
    }

    // 効果音の音量を変更する
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume); // 0～1に制限
    }

    // 効果音の音量をデフォルトに戻す
    public void SetSFXVolumeDefault()
    {
        sfxSource.volume = Mathf.Clamp01(1); // 0～1に制限
    }
}


