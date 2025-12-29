using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningMessage : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI warningMessage;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject PBwarningPanel;
    [SerializeField] private float showTime;

    private void Awake()
    {
        warningPanel.SetActive(false);
        PBwarningPanel.SetActive(false);
    }

    public void ShowWarning()
    {
        StartCoroutine(WarningCoroutine());
    }

    private IEnumerator WarningCoroutine()
    {
        if (warningPanel == null) yield return null;
        if (warningPanel != null)
        {
            warningPanel.SetActive(true);

            yield return new WaitForSeconds(showTime);

            warningPanel.SetActive(false);
        }
    }

    public void ShowPBWarning()
    {
        StartCoroutine(PBWarningCoroutine());
    }

    private IEnumerator PBWarningCoroutine()
    {
        if (PBwarningPanel == null) yield return null;
        if (PBwarningPanel != null)
        {
            PBwarningPanel.SetActive(true);

            yield return new WaitForSeconds(showTime);

            PBwarningPanel.SetActive(false);
        }
    }
}
