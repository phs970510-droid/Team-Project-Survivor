using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{
    public GameObject emptyMoneyPaenl;
    public List<GameObject> weaponButtons;

    [SerializeField] private TextMeshProUGUI currentMoneyText;

    public List<WeaponData> items = new List<WeaponData>();

    public void UnclikWeaponDataSO(int index)
    {
        WeaponData weapon = items[index];

        if (DataManager.Instance.TrySpendMoney(weapon.price))
        {
            //DataManager.Instance.UnlockWeapon();
            weaponButtons[index].SetActive(false);
            weapon.isUnlocked = true;
        }
        else
        {
            emptyMoneyPaenl.SetActive(true);
        }
    }
}