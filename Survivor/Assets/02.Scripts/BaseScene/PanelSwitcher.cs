using System.Collections;
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
        if (panelMenu == null) return;
        panelMenu.SetActive(!panelMenu.activeSelf);

    }

    public void CloseMenuPanel()
    {

        if (panelMenu == null) return;

        panelMenu.SetActive(false);

    }

    public void CloseWarningPanel()
    {
        if(panelWarning == null) return;    
        panelWarning.SetActive(false);
    }


}
