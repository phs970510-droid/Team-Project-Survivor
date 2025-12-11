using UnityEngine;

public class OpenUI : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelUPGrade;
    public GameObject panelMenu;
    public GameObject panelShop;

    public void OpenBasePanel()
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
    public void CloseMenu()
    {
        panelMenu.SetActive(false);
    }

}
