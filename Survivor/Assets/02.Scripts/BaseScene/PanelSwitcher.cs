using System.Collections;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelUPGrade;
    public GameObject panelMenu;
    public GameObject panelShop;
    public GameObject panelWarning;
    public GameObject panelSelcet;
    public GameObject joysitck;


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
        if (joysitck == null) return;
        StartCoroutine(StopAction());

    }
    public void CloseMenuPanel()
    {
        Time.timeScale = 1f;

        if (panelMenu == null) return;
        if (joysitck == null) return;

        panelMenu.SetActive(false);
        joysitck.SetActive(true);

    }
    public void CloseWarningPanel()
    {
        if(panelWarning == null) return;    
        panelWarning.SetActive(false);
    }

    public void CloseSelectItme()
    {
        if (panelSelcet == null) return;
        panelSelcet.SetActive(false);
        joysitck.SetActive(true);
        Time.timeScale = 1f;
    }

    private IEnumerator StopAction()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;

        joysitck.SetActive(false);

        panelMenu.SetActive(!panelMenu.activeSelf);
        if (panelMenu.gameObject.activeInHierarchy == false)
        {
            Time.timeScale = 1f;
            joysitck.SetActive(true);
        }

    }

}
