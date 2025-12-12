//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class DataSoWeaponUnlock : MonoBehaviour
//{

//    public GameObject emptyGoldPaenl;
//    public List<GameObject> weaponButtons;

//    [SerializeField] private TextMeshProUGUI currentGoldText;
//    [SerializeField] private float currentGold = 2000; // 골드 확인용 추후 삭제 예정
    

//    //public List<WeaponData> items = new List<WeaponData>();


//    void Update()
//    {
//        currentGoldText.text = currentGold.ToString();
//    }

//    public void UnlcikWeaponDataSO(int index)
//    {
//        WeaponData weapon = items[index];

//        if (currentGold >= weapon.price)
//        {
//            currentGold -= weapon.price;
//            weaponButtons[index].SetActive(false);
//            currentGoldText.text = currentGold.ToString();
//        }
//        else
//        {
//            emptyGoldPaenl.SetActive(true);
//        }

//    }
//}
