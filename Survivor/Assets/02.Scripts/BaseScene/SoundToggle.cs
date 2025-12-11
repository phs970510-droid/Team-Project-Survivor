
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public void ToggleBGM()
    {
        if (bgmSource.volume == 0f)
        {
            bgmSource.volume = 1f; 
        }
        else
        {
            bgmSource.volume = 0f;
        }
    }
    public void ToggleSFX()
    {
        if (sfxSource.volume == 0f)
        {
            sfxSource.volume = 1f;
        }
        else
        {
            sfxSource.volume = 0f;
        }
    }
}
