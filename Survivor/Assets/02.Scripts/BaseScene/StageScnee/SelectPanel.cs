using System.Collections.Generic;
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
        List<WeaponData> temp = new List<WeaponData>(allWeaponData);

        for (int i = 0; i < buttons.Length; i++)
        {
            int rand = Random.Range(0, temp.Count);
            WeaponData data = temp[rand];
            temp.RemoveAt(rand);

            buttons[i].SetData(data);


        }

    }
}
