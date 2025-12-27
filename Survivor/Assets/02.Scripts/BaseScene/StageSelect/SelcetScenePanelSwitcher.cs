using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelcetScenePanelSwitcher : MonoBehaviour
{
    public GameObject panelMenu;
    public GameObject panelLock;

    public void ToggleMenu()
    {
        panelMenu.SetActive(!panelMenu.activeSelf);

    }
    public void CloseMenuPanel()
    {
        panelMenu.SetActive(false);
    }
    public void CloseLockPanel()
    {
        panelLock.SetActive(false);
    }

}
