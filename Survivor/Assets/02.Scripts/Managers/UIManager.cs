using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Controllers")]
    public PanelSwitcher panelSwitcher;
    public SoundOptions soundOptions; //추후 삭제 예정
    public SoundToggle soundToggle; //추후 삭제 예정
    public GameQuit gameQuit;
    public SceneLoader sceneLoader;

    [SerializeField] private TextMeshProUGUI moneyText;
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMoney(int money)
    {
        moneyText.text = money.ToString();
    }

    //MenuToggle
    public void ToggleMenu()
    {
        if(panelSwitcher == null)
        {
            return;
        }
        panelSwitcher.ToggleMenu();
    }
    //UISwitcher
    public void OpenMainPanel()
    {

        if (panelSwitcher == null) return;
        panelSwitcher.OpenMainPanel();
    }
    public void OpenUpGradePanel()
    {
        if (panelSwitcher == null) return;
        panelSwitcher.OpenUPGradePanel();
    }
    public void CloseMenuPanel()
    {
        if (panelSwitcher == null) return;
        panelSwitcher.CloseMenuPanel();
    }

    public void OpenShopPanel()
    {
        if (panelSwitcher == null) return;
        panelSwitcher.OpenShopPanel();
    }
    public void CloseWarning()
    {
        if (panelSwitcher == null) return;
        panelSwitcher.CloseWarningPanel();
    }

    //QuitGame
    public void QuitGame()
    {
        if(gameQuit == null) return;
        gameQuit.QuitButton();
    }
    //SceneLoad
    public void BattleSceneLoad()
    {
        if(sceneLoader == null) return;
        sceneLoader.BattleSceneLoader();
    }
    public void MainSceneLoad()
    {
        if(sceneLoader ==null) return;
        sceneLoader.MainSceneLoder();
    }

    //SelectItem
    public void ColseSelectItme()
    {
        if (panelSwitcher == null) return;
        panelSwitcher.CloseSelectItme();
    }
    //public void OpenSelcetItme()
    //{
    //    if (panelSwitcher == null) return;
    //    panelSwitcher.OpenSelcetItme();
    //}

    //SoundController
    //사운드 테스트를 위해 넣어놨습니다. 서준님께서 완성하시면 아래는 따로 전부 삭제 예정입니다.
    public void SetBGMVolume()
    {
        if(soundOptions == null) return;
        soundOptions.SetBgmVolme();
    }
    public void SetSFXVolume()
    {
        if (soundOptions == null) return;

        soundOptions.SetSFXVolme();
    }
    public void ToggleBGM()
    {
        if(soundToggle == null) return;    
        soundToggle.ToggleBGM();
    }
    public void ToggleSFX()
    {
        if (soundToggle == null) return;

        soundToggle.ToggleSFX();
    }

}