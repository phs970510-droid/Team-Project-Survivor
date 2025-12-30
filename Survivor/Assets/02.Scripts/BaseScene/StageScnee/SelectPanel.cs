using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    public SelectItemButton[] buttons;     
    public List<WeaponData> allWeaponData; 

    private void OnEnable()
    {
        ShowRandomItems();
    }

    void ShowRandomItems()
    {
        List<WeaponData> availableWeapons = allWeaponData
            .Where(w =>
                w.isUnlocked &&
                w.starLevel < w.maxStar)
            .ToList();
        for (int i = 0; i < buttons.Length; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int rand = Random.Range(0, availableWeapons.Count);
                WeaponData data = availableWeapons[rand];
                availableWeapons.RemoveAt(rand);

                buttons[i].SetData(data);
            }
            else
            {
                buttons[i].SetHeal();
            }

        }
    }
}
