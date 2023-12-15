using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundSlider : MonoBehaviour
{
    public Image[] bgmVolume;
    public Image[] sfxVolume;

    // Start is called before the first frame update
    private void Start()
    {
        BGMSetting();
        SFXSetting();
    }

    public void BGMSetting()
    {
        for (int i = 0; i < bgmVolume.Length; i++)
        {
            bgmVolume[i].color = new Color(bgmVolume[i].color.r, bgmVolume[i].color.g, bgmVolume[i].color.b, 0.5f);
        }
        float index = Mathf.FloorToInt(AudioManager.Instance.bgmSource.volume * 10);
        for (int i = 0; i < index; i++)
        {
            bgmVolume[i].color = new Color(bgmVolume[i].color.r, bgmVolume[i].color.g, bgmVolume[i].color.b, 1f);
        }
    }

    public void SFXSetting()
    {
        for (int i = 0; i < sfxVolume.Length; i++)
        {
            sfxVolume[i].color = new Color(sfxVolume[i].color.r, sfxVolume[i].color.g, sfxVolume[i].color.b, 0.5f);
        }
        float index = Mathf.FloorToInt(AudioManager.Instance.sfxSource[1].volume * 10);
        for (int i = 0; i < index; i++)
        {
            sfxVolume[i].color = new Color(sfxVolume[i].color.r, sfxVolume[i].color.g, sfxVolume[i].color.b, 1f);
        }
    }

    public void BGMUPVolume()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        AudioManager.Instance.UpBGMVolume();
        BGMSetting();
    }

    public void BGMDOWNVolume()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        AudioManager.Instance.DownBGMVolume();
        BGMSetting();
    }

    public void SFXUPVolume()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        AudioManager.Instance.UpSFXVolume();
        SFXSetting();

    }

    public void SFXDOWNVolume()
    {
        AudioManager.Instance.PlayButton(AudioManager.Button.select);
        AudioManager.Instance.DownSFXVolume();
        SFXSetting();
    }
}