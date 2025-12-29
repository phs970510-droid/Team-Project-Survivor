using UnityEngine;

public class MainScenePanelSwitcher : MonoBehaviour
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
