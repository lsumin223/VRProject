using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using static AudioManager;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    public AudioClip[] bgmClip;
    public AudioSource bgmSource;
    public float bgmVolume;
    AudioHighPassFilter bgmEffect;

    public AudioClip[] buttonClip;
    public AudioSource buttonSource;
    AudioHighPassFilter buttonEffect;

    public AudioClip[] sfxClip;
    public AudioSource[] sfxSource;
    public float sfxVolume;
    public int sfxChannels;

    int channelIndex;

    public enum BGM
    {
        titleBGM,
        firstBGM,
        secondBGM,
        thirdBGM,
    }

    public enum SFX
    {
        countdown,
        EngineSound,
        Booster,
        Break,
        CatSpawn,
        Whale,
        Hit
    }

    public enum Button
    {
        select,
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if(this != instance)
            {
                Destroy(this.gameObject);
            }
        }
    }


    // Update is called once per frame
    void Start()
    {
        Init();
        PlayBGM(BGM.titleBGM);
    }


    private void CurrentBGMPlay()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "MainScene" || currentSceneName == "ClearScene")
        {
            PlayBGM(BGM.titleBGM);
        }
        else if(currentSceneName == "Stage1Scene")
        {
            PlayBGM(BGM.firstBGM);
        }
        else if (currentSceneName == "Stage2Scene")
        {
            PlayBGM(BGM.secondBGM);
        }
        else if (currentSceneName == "Stage3Scene")
        {
            PlayBGM(BGM.thirdBGM);
        }
    }

    private void Init()
    {
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = transform;
        bgmSource = bgmObject.AddComponent<AudioSource>();
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;

        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxSource = new AudioSource[sfxChannels];

        for (int index = 0; index < sfxSource.Length; index++)
        {
            sfxSource[index] = sfxObject.AddComponent<AudioSource>();
            sfxSource[index].playOnAwake = false;
            sfxSource[index].bypassListenerEffects = true;
        }

        GameObject buttonObject = new GameObject("ButtonPlayer");
        buttonObject.transform.parent = transform;
        buttonSource = buttonObject.AddComponent<AudioSource>();
        buttonSource.playOnAwake = false;

        InitVolume();
    }

    private void InitVolume()
    {
        bgmSource.volume = bgmVolume;
        for (int index = 0; index < sfxSource.Length; index++)
        {
            sfxSource[index].volume = sfxVolume;
        }
        buttonSource.volume = sfxVolume;
    }

    public void UpBGMVolume()
    {
        bgmVolume += 0.1f;
        if (bgmVolume >= 1) bgmVolume =1;
        bgmSource.volume = bgmVolume;
    }

    public void DownBGMVolume()
    {
        bgmVolume -= 0.1f;
        if (bgmVolume <= 0) bgmVolume = 0;

        bgmSource.volume = bgmVolume;
    }

    public void UpSFXVolume()
    {
        sfxVolume += 0.1f;
        if (sfxVolume >= 1) sfxVolume = 1;
        for (int index = 0; index < sfxSource.Length; index++)
        {
            sfxSource[index].volume = sfxVolume;
        }
        buttonSource.volume = sfxVolume;
    }

    public void DownSFXVolume()
    {
        sfxVolume -= 0.1f;
        if (sfxVolume <= 0) sfxVolume = 0;
        for (int index = 0; index < sfxSource.Length; index++)
        {
            sfxSource[index].volume = sfxVolume;
        }
        buttonSource.volume = sfxVolume;
    }

    public void SetButtonVolume(float volume)
    {
        sfxVolume = volume;
        buttonSource.volume = sfxVolume;
    }

    public void PlayBGM(BGM bgm)
    {
        bgmSource.clip = bgmClip[(int)bgm];
        bgmSource.Play();
    }

    public void PlaySFX(SFX sfx)
    {
        for (int index = 0; index < sfxSource.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxSource.Length;

            if (sfxSource[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxSource[loopIndex].clip = sfxClip[(int)sfx];
            sfxSource[loopIndex].Play();

            break;
        }
    }

    public int PlaySfxLoop(SFX sfx)
    {
        for (int index = 0; index < sfxSource.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxSource.Length;

            if (sfxSource[loopIndex].isPlaying)
            {
                continue;
            }
            channelIndex = loopIndex;
            sfxSource[loopIndex].clip = sfxClip[(int)sfx];
            sfxSource[loopIndex].Play();
            sfxSource[loopIndex].loop = true;
            return loopIndex;
        }
        return -1;
    }



    public void PlayButton(Button button)
    {
        buttonSource.clip = buttonClip[(int)button];
        buttonSource.Play();
    }
}
