using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using static AudioManager;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [Header("경제/자원")]
    public int Money;   //코인

    [Header("데이터 참조")]
    public PlayerData playerData;
    public BaseData baseData;
    public List<WeaponData> allWeaponData = new();

    public int CurrentSlot { get; private set; } = 0;

    public float bgmVolume;     // BGM 볼륨 값(0~1)
    AudioSource bgmPlayer;      // BGM을 실제로 재생하는 AudioSource
    public float sfxVolume;      // 전체 효과음 볼륨
    AudioSource[] sfxPlayers;    // 효과음 재생용 AudioSource 배열
    int channelIndex;
    public AudioClip[] sfxClips;
    public AudioClip bgmClip;
    public int channels;
    AudioHighPassFilter bgmEffect;
    [Header("UI")]
    public Toggle bgmToggle;
    public Toggle sfxToggle;
    public Slider bgmSlider;
    public Slider sfxSlider;

    public enum Sfx {Coin, Select }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        Money = Mathf.Max(0, Money + amount);
        UIManager.Instance?.UpdateMoney(Money);
        Save();
    }

    public bool TrySpendMoney(int amount)
    {
        if (Money < amount) return false;
        Money -= amount;
        UIManager.Instance?.UpdateMoney(Money);
        Save();
        return true;
    }
    public int GetBaseUpgradeCost(int index)
    {
        return baseData.baseUpgradeCosts[index];
    }

    public void IncreaseBaseUpgradeCost(int index)
    {
        baseData.baseUpgradeCosts[index] *= 2;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.Save();

        MirrorQuickSaveToCurrentSlotIfAny();
    }

    public void Load()
    {
        Money = PlayerPrefs.GetInt("Money", 0);
        UIManager.Instance?.UpdateMoney(Money);
    }
    public void SaveAllData(int slotIndex)
    {
        string prefix = $"Save{slotIndex}_";
        PlayerPrefs.SetInt(prefix + "Money", Money);

        if (baseData != null)
        {
            PlayerPrefs.SetFloat(prefix + "Player_MaxHP", baseData.maxHp);
            PlayerPrefs.SetFloat(prefix + "Player_MoveSpeed", baseData.moveSpeed);
        }

        foreach (var weapon in allWeaponData)
        {
            if (weapon == null) continue;
            PlayerPrefs.SetInt(prefix + $"{weapon.weaponName}_Unlocked", weapon.isUnlocked ? 1 : 0);
            PlayerPrefs.SetFloat(prefix + $"{weapon.weaponName}_Damage", weapon.damage);
        }

        for (int i = 0; i < baseData.baseUpgradeCosts.Length; i++)
        {
            PlayerPrefs.SetInt(
                prefix + $"BaseUpgradeCost_{i}",
                baseData.baseUpgradeCosts[i]
            );
        }

        PlayerPrefs.Save();
        Debug.Log($"[DataManager] 슬롯 {slotIndex} 저장 완료");
    }
    public void LoadAllData(int slotIndex)
    {
        string prefix = $"Save{slotIndex}_";
        Money = PlayerPrefs.GetInt(prefix + "Money", 0);

        if (baseData != null)
        {
            baseData.maxHp = PlayerPrefs.GetFloat(prefix + "Player_MaxHP", baseData.maxHp);
            baseData.moveSpeed = PlayerPrefs.GetFloat(prefix + "Player_MoveSpeed", baseData.moveSpeed);
        }

        foreach (var weapon in allWeaponData)
        {
            if (weapon == null) continue;
            weapon.isUnlocked = PlayerPrefs.GetInt(prefix + $"{weapon.weaponName}_Unlocked", 0) == 1;
            weapon.damage = PlayerPrefs.GetFloat(prefix + $"{weapon.weaponName}_Damage", weapon.damage);
        }

        for (int i = 0; i < baseData.baseUpgradeCosts.Length; i++)
        {
            baseData.baseUpgradeCosts[i] = PlayerPrefs.GetInt(
                prefix + $"BaseUpgradeCost_{i}",
                baseData.baseUpgradeCosts[i]
            );
        }

        UIManager.Instance?.UpdateMoney(Money);

        Debug.Log($"[DataManager] 슬롯 {slotIndex} 불러오기 완료");
    }
    public bool HasSaveSlot(int slotIndex)
    {
        string prefix = $"Save{slotIndex}_";
        return PlayerPrefs.HasKey(prefix + "Money") || PlayerPrefs.HasKey(prefix + "Player_MaxHP");
    }

    public void SetCurrentSlot(int slotIndex)
    {
        CurrentSlot = slotIndex;
        PlayerPrefs.SetInt("LastSaveSlot", slotIndex);
        PlayerPrefs.Save();
    }

    public void UpgradeBaseStat(int index)
    {
        switch (index)
        {
            case 0:
                baseData.moveSpeed += 0.2f;
                break;

            case 1:
                baseData.maxHp += 10f;
                break;
        }
    }

    private void MirrorQuickSaveToCurrentSlotIfAny()
    {
        if (CurrentSlot <= 0) return;
        string prefix = $"Save{CurrentSlot}_";
        PlayerPrefs.SetInt(prefix + "Money", Money);
        PlayerPrefs.Save();
    }
    // ===================================================================
    //               [ UI에서 연동하는 오디오 설정 함수들 ]
    // ===================================================================
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
    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }
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

    public void PlaySfxs(Sfx sfx)
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

    // UI 버튼 사운드
    public void PlaySelectSound()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    //코인 사운드
    public void PlayCoinSound()
    {
        PlaySfx(Sfx.Coin);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Coin);
            Destroy(gameObject);
        }
    }
    // ================= 상태 조회용 (UI 동기화용) =================

    public bool IsBgmPlaying()
    {
        return bgmPlayer != null && bgmPlayer.isPlaying;
    }

    public bool IsSfxEnabled()
    {
        if (sfxPlayers == null || sfxPlayers.Length == 0)
            return true;

        return !sfxPlayers[0].mute;
    }

    public float GetBgmVolume()
    {
        return bgmVolume;
    }

    public float GetSfxVolume()
    {
        return sfxVolume;
    }
    

   

   
}

