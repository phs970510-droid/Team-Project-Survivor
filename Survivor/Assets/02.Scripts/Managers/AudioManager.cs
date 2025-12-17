using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // ---------------------------------------------------------
    // 싱글톤 설정: 프로젝트 어디서든 AudioManager.instance로 접근 가능
    // ---------------------------------------------------------
    public static AudioManager instance;

    [Header("BGM")]
    public AudioClip bgmClip;   // 배경음으로 사용할 오디오 클립
    public float bgmVolume;     // BGM 볼륨 값(0~1)
    AudioSource bgmPlayer;      // BGM을 실제로 재생하는 AudioSource
    AudioHighPassFilter bgmEffect; // BGM에 하이패스 필터 효과 적용용

    [Header("SFX")]
    public AudioClip[] sfxClips; // 효과음 클립 배열 (enum Sfx 순서대로 넣기)
    public float sfxVolume;      // 전체 효과음 볼륨
    public int channels;         // 동시에 재생할 효과음 채널 수
    AudioSource[] sfxPlayers;    // 효과음 재생용 AudioSource 배열
    int channelIndex;            // 다음 사용할 채널 인덱스(라운드 로빈 방식)

    // 효과음을 인덱스로 부르기 위한 enum
    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Melee, Range = 7, Select, Win }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        // ---------------------------------------------------------
        // BGM 플레이어 생성 & 초기화
        // ---------------------------------------------------------

        // 빈 GameObject 만들고 AudioSource 붙이기
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;

        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;   // 시작 시 자동 재생 금지
        bgmPlayer.loop = true;           // 무한 반복 재생
        bgmPlayer.volume = bgmVolume;    // 초기 볼륨 적용
        bgmPlayer.clip = bgmClip;        // 재생할 배경음 설정

        // 메인 카메라에 있는 HighPassFilter를 가져와서 저장
        //bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // ---------------------------------------------------------
        // 효과음(SFX) 재생 채널 생성
        // ---------------------------------------------------------

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;

        // 설정한 채널 수 만큼 AudioSource 생성
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;          // 자동 재생 금지
            sfxPlayers[i].bypassListenerEffects = true; // 3D 효과 무시 (2D 사운드처럼 들림)
            sfxPlayers[i].volume = sfxVolume;           // 초기 볼륨 동일하게 적용
        }
    }

    // ---------------------------------------------------------
    // BGM 재생/정지 함수
    // ---------------------------------------------------------
    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
            bgmPlayer.Play();   // 켜기
        else
            bgmPlayer.Stop();   // 끄기
    }

    // ---------------------------------------------------------
    // BGM 하이패스 필터 효과 켜기/끄기
    // ---------------------------------------------------------
    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    // ---------------------------------------------------------
    // 효과음 재생 함수
    // (여러 AudioSource 중 비어 있는 채널 선택하여 재생)
    // ---------------------------------------------------------
    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            // 라운드 로빈 방식으로 다음 채널 선택
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            // 현재 채널이 이미 재생 중이면 다음 채널로
            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            // 사용 가능한 채널 발견 → 여기에 효과음 재생
            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    // ===================================================================
    //               [ UI에서 연동하는 오디오 설정 함수들 ]
    // ===================================================================

    // ---------------------------------------------------------
    // UI: BGM 켜기 / 끄기 (Toggle 연결)
    // ---------------------------------------------------------
    public void SetBgmEnabled(bool isOn)
    {
        if (isOn)
            bgmPlayer.Play();   // BGM 재생
        else
            bgmPlayer.Stop();   // BGM 정지
    }

    // ---------------------------------------------------------
    // UI: SFX 켜기 / 끄기 (Toggle 연결)
    // ---------------------------------------------------------
    public void SetSfxEnabled(bool isOn)
    {
        // mute 속성으로 전체 효과음을 한 번에 On/Off
        foreach (var p in sfxPlayers)
            p.mute = !isOn;
    }

    // ---------------------------------------------------------
    // UI: BGM 볼륨 슬라이더 (0~1)
    // ---------------------------------------------------------
    public void SetBgmVolume(float volume)
    {
        bgmVolume = volume;

        if (bgmPlayer != null)
            bgmPlayer.volume = bgmVolume;
    }

    // ---------------------------------------------------------
    // UI: SFX 볼륨 슬라이더 (0~1)
    // ---------------------------------------------------------
    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;

        if (sfxPlayers != null)
        {
            foreach (var p in sfxPlayers)
                p.volume = sfxVolume;
        }
    }
    
    public void PlaySelectSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }
   

   
}

