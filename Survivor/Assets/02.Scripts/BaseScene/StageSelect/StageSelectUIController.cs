using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectUIController : MonoBehaviour
{
    public SelcetScenePanelSwitcher selcetScenePanelSwitcher;
    public GameQuit gameQuit;
    public void BattleSecneToggleMenu()
    {
        if (selcetScenePanelSwitcher == null) return;
        selcetScenePanelSwitcher.ToggleMenu();
    }

    public void BattleSecneCloseMenuPanel()
    {
        if (selcetScenePanelSwitcher == null) return;
        selcetScenePanelSwitcher.CloseMenuPanel();
    }
    public void QuitGame()
    {
        if (gameQuit == null) return;
        gameQuit.QuitButton();
    }
}
