using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleUIController : MonoBehaviour
{

    public BattleScenePanelSwitcher BattleScenePanelSwitcher;
    public MissionBoard missionBoard;
    public GameQuit gameQuit;
    public void BattleSecneToggleMenu()
    {
        if (BattleScenePanelSwitcher == null) return;
        BattleScenePanelSwitcher.BattleSenceToggleMenu();
    }

    public void BattleSecneCloseMenuPanel()
    {
        if (BattleScenePanelSwitcher == null) return;
        BattleScenePanelSwitcher.BattleSecneCloseMenuPanel();
    }

    public void MainSceneLoder()
    {
        missionBoard.MainSceneLoder();
    }
    public void QuitGame()
    {
        if (gameQuit == null) return;
        gameQuit.QuitButton();
    }

    public void CloseSelectPanel()
    {
        BattleScenePanelSwitcher.CloseSelectItme();
    }
}
