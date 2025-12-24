using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;

public class AudioController : MonoBehaviour
{ 
    public Slider bgmSlider;
    public Slider sfxSlider;

    public Toggle bgmToggle;
    public Toggle sfxToggle;

    private void Start()
    {
        SyncUIFromAudioManager();
    }
    void SyncUIFromAudioManager()
    {
        var audio = AudioManager.instance;

        bgmToggle.isOn = audio.IsBgmPlaying();
        bgmSlider.value = audio.GetBgmVolume();

        sfxToggle.isOn = audio.IsSfxEnabled();
        sfxSlider.value = audio.GetSfxVolume();
    }

    public void BGMSlider(float value)
    {
        AudioManager.instance.SetBgmVolume(value);
    }

    public void SFXSlider(float value)
    {
        AudioManager.instance.SetSfxVolume(value);
    }

    public void BGMToggle (bool value)
    {
        AudioManager.instance.SetBgmEnabled(value);
    }

    public void SfxToggle (bool value)
    {
        AudioManager.instance.SetSfxEnabled(value);
    }
}
