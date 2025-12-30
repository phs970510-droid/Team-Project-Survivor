using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weaponObjects;

    private void OnEnable()
    {
        RefreshWeaponObject();
    }

    private void RefreshWeaponObject()
    {
        for(int i = 0; i < DataManager.Instance.allWeaponData.Count; i++)
        {
            bool unlocked = DataManager.Instance.allWeaponData[i].isUnlocked;
            if(i < weaponObjects.Count)
            {
                weaponObjects[i].SetActive(unlocked);
            }
        }
    }
}
