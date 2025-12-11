using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioManager audioManager;

    [Header("BGM")]
    public AudioClip bgmClip;  //배경음
    public float bgmVolume; //BGM 볼륨
    AudioSource bgmPlayer; //BGM을 재생
    AudioHighPassFilter bgmEffect; // BGM에 하이패스

    [Header("SFX")]
    public AudioClip[] sfxClips; //효과음 배열 클립
    public float sfxVolume; // 전체 효과음볼륨
    public int channels; // 동시 재생할 효과채널 수
    AudioSource[] sfxPlayers; //플레이어 효과 재생용 배열
    int channelindex; // 다음사용할 채널 

    public enum Sfx { Dead, Hit, LevelUp = 3, Lose, Mellee, Range = 7, Select, Win }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;

        bgmPlayer = gameObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false; // 시작시 자동 재생 금지 
        bgmPlayer.loop = true;  // 무한 반복 재생
        bgmPlayer.volume = bgmVolume; //초기 볼륨적용
        bgmPlayer.clip = bgmClip; // 재생할 배경음을 설정

        //메인카메라 있는 하이패스 가져오기
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();


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
    // BGM 재생 / 정지 
    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
            bgmPlayer.Play();   // 켜기
        else
            bgmPlayer.Stop();   // 끄기
    }

    //BGM 하이패스 필터 
    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }
    //효과음 재생
    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            //라운드 로빈 으로 다음채널 선택하기
            int loopIndex = (i + channelindex) % sfxPlayers.Length;

            // 현재 채널이 이미 재생 중이면 다음 채널
            if (sfxPlayers[loopIndex].isPlaying)
                continue;
            // 사용가능 채널 > 여기로 효과음 재생
            channelindex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    //UI : sfx 켜기 끄기 연결
    public void SetBgmEnabled(bool ison)
    {
        if (ison)
            bgmPlayer.Play();  // bgm 재생

        else
            bgmPlayer.Stop();  // 끄기
    }

    public void SetsfxEnabled(bool ison)
    {
        //mute 속성으로 전체 효과음 한번에 온 오프
        foreach (var p in sfxPlayers)
            p.mute = !ison;

    }

    // UI BGM 볼륨 조절 슬라이드
    public void SetBgmVolume(float volume)
    {
        bgmVolume = volume;

        if (bgmPlayer != null)
            bgmPlayer.volume = bgmVolume;
    }

    // UI SFX 볼륨 슬라이드 조절
    public void SetsfxVolume(float volume)
    {
        sfxVolume = volume;

        if (sfxPlayers != null)
        {
            foreach (var p in sfxPlayers)
                p.volume = sfxVolume;
        }
    }
}
