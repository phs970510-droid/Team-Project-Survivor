using UnityEngine;

public class AudioController : MonoBehaviour
{
    public void OnBgmSliderChanged(float value)
    {
        if (AudioManager.instance == null) return;
        AudioManager.instance.SetBgmVolume(value);
    }

    public void OnBgmToggleChanged(bool isOn)
    {
        if (AudioManager.instance == null) return;
        AudioManager.instance.SetBgmEnabled(isOn);
    }

    public void OnSfxSliderChanged(float value)
    {
        if (AudioManager.instance == null) return;
        AudioManager.instance.SetSfxVolume(value);
    }

    public void OnSfxToggleChanged(bool isOn)
    {
        if (AudioManager.instance == null) return;
        AudioManager.instance.SetSfxEnabled(isOn);
    }
}
