using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpGradeItem : MonoBehaviour
{
    public GameObject emptyGoldPaenl;

    [Header("UpgradeCost")]
    public List<TextMeshProUGUI> upgradeCost;

    [Header("CurrentMoneyText")]
    [SerializeField] private TextMeshProUGUI currentMoneyText;

    public void UpgradeItem(int index)
    {
        int price = DataManager.Instance.GetBaseUpgradeCost(index);

        if (DataManager.Instance.TrySpendMoney(price))
        {
            DataManager.Instance.UpgradeBaseStat(index);

            DataManager.Instance.IncreaseBaseUpgradeCost(index);

            upgradeCost[index].text =
            DataManager.Instance.GetBaseUpgradeCost(index).ToString();
        }
        else
        {
            emptyGoldPaenl.SetActive(true);
        }

    }
    private void OnEnable()
    {
        RefreshUpgradeCostUI();
    }
    private void RefreshUpgradeCostUI()
    {
        for (int i = 0; i < upgradeCost.Count; i++)
        {
            upgradeCost[i].text =
                DataManager.Instance.GetBaseUpgradeCost(i).ToString();
        }
    }
}
