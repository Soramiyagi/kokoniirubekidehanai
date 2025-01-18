using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgmSource;       // BGM�pAudioSource
    [SerializeField] private AudioSource sfxSource;       // ���ʉ��pAudioSource
    [SerializeField] private AudioClip[] soundEffects;    // ���ʉ����X�g

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��j������Ȃ�
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // BGM���Đ�����
    public void PlayBGM(AudioClip bgm)
    {
        if (bgmSource.clip != bgm)
        {
            bgmSource.clip = bgm;
            bgmSource.Play();
        }
    }

    // BGM���~����
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

    // ���ʉ����Đ��i���O�Ŏw��j
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
    
    // BGM�̉��ʂ�ύX����
    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = Mathf.Clamp01(volume); // 0�`1�ɐ���
    }

    // ���ʉ��̉��ʂ�ύX����
    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume); // 0�`1�ɐ���
    }

    // ���ʉ��̉��ʂ��f�t�H���g�ɖ߂�
    public void SetSFXVolumeDefault()
    {
        sfxSource.volume = Mathf.Clamp01(1); // 0�`1�ɐ���
    }
}


