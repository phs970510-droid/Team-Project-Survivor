using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScenePanelSwitcher : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelSelcet;
    public GameObject joysitck;
    public PlayerController playerController;


    public void BattleSenceToggleMenu()
    {
        bool OpenMenu = !panelMenu.activeSelf;

        panelMenu.SetActive(OpenMenu);  

        if (OpenMenu)
        {            
            joysitck.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            joysitck.SetActive(true);
            Time.timeScale = 1f;
        }

    }

    public void BattleSecneCloseMenuPanel()
    {
        Time.timeScale = 1f;

        if (panelMenu == null) return;
        if (joysitck == null) return;

        panelMenu.SetActive(false);
        joysitck.SetActive(true);
    }
    public void CloseSelectItme()
    {
        if (panelSelcet == null) return;
        panelSelcet.SetActive(false);
        joysitck.SetActive(true);
        Time.timeScale = 1f;
    }

}
