using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{

    public GameObject emptyMoneyPaenl;
    public List<GameObject> weaponButtons;

    [SerializeField] private TextMeshProUGUI currentMoneyText;
    public List<WeaponData> items = new List<WeaponData>();


    public void UnlcikWeaponDataSO(int index)
    {
        WeaponData weapon = items[index];

        if (float.TryParse(currentMoneyText.text, out float currentMoney))
        {

            if (currentMoney >= weapon.price)
            {
                currentMoney -= weapon.price;
                weaponButtons[index].SetActive(false);
                currentMoneyText.text = currentMoney.ToString();
            }
            else
            {
                emptyMoneyPaenl.SetActive(true);
            }
        }

    }
}
