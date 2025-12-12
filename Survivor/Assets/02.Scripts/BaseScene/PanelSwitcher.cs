using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelUPGrade;
    public GameObject panelMenu;
    public GameObject panelShop;
    public GameObject panelWarning;

    public void OpenMainPanel()
    {
        panelUPGrade.SetActive(false);
        panelShop.SetActive(false);
        panelMain.SetActive(true);
    }

    public void OpenUPGradePanel() 
    {
        panelMain.SetActive(false);
        panelShop.SetActive(false);
        panelUPGrade.SetActive(true);

    }
    public void OpenShopPanel()
    {
        panelMain.SetActive(false);
        panelUPGrade.SetActive(false);
        panelShop.SetActive(true);
    }

    public void ToggleMenu()
    {
        panelMenu.SetActive(!panelMenu.activeSelf);
    }
    public void CloseMenuPanel()
    {
        panelMenu.SetActive(false);
    }
    public void CloseWarningPanel()
    {
        panelWarning.SetActive(false);
    }

}
