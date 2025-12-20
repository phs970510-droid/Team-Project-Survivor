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
        int price = int.Parse(upgradeCost[index].text);

        if (DataManager.Instance.TrySpendMoney(price))
        {
            //DataManager.Instance.UpgradeItem();
            int newPrice = price * 2;
            upgradeCost[index].text = newPrice.ToString();
        }
        else
        {
            emptyGoldPaenl.SetActive(true);
        }

    }
}
