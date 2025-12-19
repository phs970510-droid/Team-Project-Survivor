using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StageUIController : MonoBehaviour
{

    public StageScenePanelSwitcher stageScenePanelSwitcher;
    public MissionBoard missionBoard;
    public GameQuit gameQuit;
    public void BattleSecneToggleMenu()
    {
        if (stageScenePanelSwitcher == null) return;
        stageScenePanelSwitcher.BattleSenceToggleMenu();
    }

    public void BattleSecneCloseMenuPanel()
    {
        if (stageScenePanelSwitcher == null) return;
        stageScenePanelSwitcher.BattleSecneCloseMenuPanel();
    }

    public void BaseSceneLoder()
    {
        missionBoard.BaseSceneLoder();
    }
    public void QuitGame()
    {
        if (gameQuit == null) return;
        gameQuit.QuitButton();
    }

    public void CloseSelectPanel()
    {
        stageScenePanelSwitcher.CloseSelectItem();
    }
}
