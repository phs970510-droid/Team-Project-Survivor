using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Controllers")]
    public PanelSwitcher panelSwitcher;
    public SoundOptions soundOptions; //추후 삭제 예정
    public SoundToggle soundToggle; //추후 삭제 예정
    public GameQuit gameQuit;

    private void Awake()
    {
        Instance = this;
    }

    //MenuToggle
    public void ToggleMenu()
    {
        panelSwitcher.ToggleMenu();
    }
    //UISwitcher
    public void OpenMainPanel()
    {
        panelSwitcher.OpenMainPanel();
    }
    public void OpenUpGradePanel()
    {
        panelSwitcher.OpenUPGradePanel();
    }
    public void CloseMenuPanel()
    {
        panelSwitcher.CloseMenuPanel();
    }

    public void OpenShopPanel()
    {
        panelSwitcher.OpenShopPanel();
    }
    public void CloseWarning()
    {
        panelSwitcher.CloseWarningPanel();
    }

    //QuitGame
    public void QuitGame()
    {
        gameQuit.QuitButton();
    }

    //SoundController
    //사운드 테스트를 위해 넣어놨습니다. 서준님께서 완성하시면 아래는 따로 전부 삭제 예정입니다.
    public void SetBGMVolume()
    {
        soundOptions.SetBgmVolme();
    }
    public void SetSFXVolume()
    {
        soundOptions.SetSFXVolme();
    }
    public void ToggleBGM()
    {
        soundToggle.ToggleBGM();
    }
    public void ToggleSFX()
    {
        soundToggle.ToggleSFX();
    }

}