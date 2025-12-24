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

    [Header("UpGradeExplane")]
    public List<TextMeshProUGUI> upGradeExplane;


    public void UpgradeItem(int index)
    {
        int price = DataManager.Instance.GetBaseUpgradeCost(index);

        if (DataManager.Instance.TrySpendMoney(price))
        {
            DataManager.Instance.UpgradeBaseStat(index);
            DataManager.Instance.IncreaseBaseUpgradeCost(index);

            RefreshUpgradeExplainUI();

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

    private void RefreshUpgradeExplainUI()
    {
        for (int i = 0; i < upGradeExplane.Count; i++)
        {
            float current = GetCurrentStat(i);
            float next = DataManager.Instance.GetNextState(i);
            upGradeExplane[i].text =
                $"{GetStatName(i)}\n{current} -> {next}";
        }
    }

    float GetCurrentStat(int index)
    {
        switch (index)
        {
            case 0:
                return DataManager.Instance.baseData.moveSpeed;
            case 1:
                return DataManager.Instance.baseData.maxHp;
            default:
                return 0f;
        }
    } 

    string GetStatName(int index)
    {
        switch (index)
        {
            case 0:
                return "MoveSpeed";
            case 1:
                return "Max Hp";
            default:
                return "";
        }
    }
    
}
