using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScenePanelSwitcher : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelSelcet;
    public GameObject joyStick;

    private void Start()
    {
        if (joyStick == null)
        {
            joyStick = GameObject.FindWithTag("JoyStick");
        }
    }
    public void BattleSenceToggleMenu()
    {
        if(panelMenu == null)
        {
            return;
        }
        bool openMenu = !panelMenu.activeSelf;

        panelMenu.SetActive(openMenu);

        if (GetJoyStick())
        {
            if (openMenu)
            {
                joyStick.SetActive(false);
                Time.timeScale = 0f;
            }
            else
            {
                joyStick.SetActive(true);
                Time.timeScale = 1f;
            }
        }

    }
    public void BattleSecneCloseMenuPanel()
    {
        Time.timeScale = 1f;

        if (panelMenu == null) return;
        if (joyStick == null) return;

        panelMenu.SetActive(false);
        joyStick.SetActive(true);
    }
    public void CloseSelectItem()
    {
        if (panelSelcet == null) return;
        panelSelcet.SetActive(false);
        joyStick.SetActive(true);
        Time.timeScale = 1f;
    }
    private bool GetJoyStick()
    {
        if (joyStick != null) return true;

        joyStick = GameObject.FindWithTag("JoyStick");
        return joyStick != null;
    }
}
