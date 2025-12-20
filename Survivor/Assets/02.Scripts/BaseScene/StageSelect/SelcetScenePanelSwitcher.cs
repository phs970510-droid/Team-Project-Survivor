using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelcetScenePanelSwitcher : MonoBehaviour
{
    public GameObject panelMenu;

    public void ToggleMenu()
    {
        panelMenu.SetActive(!panelMenu.activeSelf);

    }
    public void CloseMenuPanel()
    {
        panelMenu.SetActive(false);
    }

}
