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
        if (float.TryParse(currentMoneyText.text, out float currentGold))
        {

            if (float.TryParse(upgradeCost[index].text, out float price))
            {
                if (currentGold >= price)
                {
                    currentGold -= price;
                    price *= 2;
                    currentMoneyText.text = currentGold.ToString();
                    upgradeCost[index].text = price.ToString();
                }
                else
                {
                    emptyGoldPaenl.SetActive(true);
                }
            }
        }

    }
}
