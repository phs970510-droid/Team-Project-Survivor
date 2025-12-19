using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour
{
    public List<WeaponData> itmes = new List<WeaponData>();

    public Image[] itemIcon;
    public TextMeshProUGUI[] itemName;
    //public TextMeshProUGUI[] itemDescription;

    public StarDisplay[] starDisplays;

    //weaponSo중 하나 랜덤 선택
    public void SelectItemSO()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, itmes.Count);
            WeaponData selectedItem = itmes[randomIndex];

            UpdateItemUI(i, selectedItem);
        }
    }
    //뽑은 아이템의 Sprite, Name 넣기
    private void UpdateItemUI(int slotIndex, WeaponData item)
    {
        if (itemIcon[slotIndex] != null && item.Sprite != null)
        {
            itemIcon[slotIndex].sprite = item.Sprite;
        }
        if(itemName[slotIndex] != null)
        {
            itemName[slotIndex].text = item.weaponName;
        }
        //if(itemDescription[slotIndex] != null)
        //{
        //    itemDescription[slotIndex].text = item.description;
        //}
    }
}
