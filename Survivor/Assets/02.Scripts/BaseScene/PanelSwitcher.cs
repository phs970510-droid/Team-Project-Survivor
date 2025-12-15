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
        if (panelMain == null || panelShop == null || panelUPGrade == null) return;
        panelUPGrade.SetActive(false);
        panelShop.SetActive(false);
        panelMain.SetActive(true);
    }

    public void OpenUPGradePanel() 
    {
        if (panelMain == null || panelShop == null || panelUPGrade == null) return;

        panelMain.SetActive(false);
        panelShop.SetActive(false);
        panelUPGrade.SetActive(true);

    }
    public void OpenShopPanel()
    {
        if (panelMain == null || panelShop == null || panelUPGrade == null) return;

        panelMain.SetActive(false);
        panelUPGrade.SetActive(false);
        panelShop.SetActive(true);
    }

    public void ToggleMenu()
    {
        Time.timeScale = 0f;
        if (panelMenu == null) return;
        panelMenu.SetActive(!panelMenu.activeSelf);
        if (panelMenu.gameObject.activeInHierarchy == false)
        {
            Time.timeScale = 1f;

        }
    }
    public void CloseMenuPanel()
    {
        Time.timeScale = 1f;

        if (panelMenu == null) return;

        panelMenu.SetActive(false);
    }
    public void CloseWarningPanel()
    {
        if(panelWarning == null) return;    
        panelWarning.SetActive(false);
    }

}
