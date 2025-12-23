using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIController : MonoBehaviour
{
    [Header("Controllers")]
    public PanelSwitcher panelSwitcher;
    public GameQuit gameQuit;
    public MissionBoard sceneLoader;

    //MenuToggle
    public void ToggleMenu()
    {
        if (panelSwitcher == null)
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
    public void OpenSave()
    {
        panelSwitcher.OpenSave();
    }
    public void CloseMenuPanel()
    {
        if (panelSwitcher == null) return;
        panelSwitcher.CloseMenuPanel();
    }
    public void CloseSavePanel()
    {
        panelSwitcher.CloseSavePanel();
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
        if (gameQuit == null) return;
        gameQuit.QuitButton();
    }
    //SceneLoad
    public void BattleSceneLoad()
    {
        if (sceneLoader == null) return;
        sceneLoader.BattleSceneLoader();
    }
    public void SelectSceneLoad()
    {
        sceneLoader.StageSelectLoder();
    }
    public void OnSelectSaveSlot(int slotIndex)
    {
        DataManager.Instance.SetCurrentSlot(slotIndex);
        Debug.Log($"[SafehouseUIManager] 슬롯 {slotIndex} 저장 중...");
        DataManager.Instance.SaveAllData(slotIndex);
        PlayerPrefs.SetInt("LastSaveSlot", slotIndex);
        PlayerPrefs.Save();

        Debug.Log($"[SafehouseUIManager] 슬롯 {slotIndex} 저장 완료!");
        Debug.Log("저장 경로: " + Application.persistentDataPath);

    }
}
