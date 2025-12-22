using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class AudioController : MonoBehaviour
{
    public void BGMSlider(float value)
    {
        DataManager.Instance.SetBgmVolume(value);
    }
    public void SfxSlider(float value)
    {
        DataManager.Instance.SetSfxVolume(value);
    }
    public void BGMToggle(bool value)
    {
        DataManager.Instance.SetBgmEnabled(value);
    }
    public void SfxToggle(bool value) 
    {
        DataManager.Instance.SetSfxEnabled(value);
    }
}
