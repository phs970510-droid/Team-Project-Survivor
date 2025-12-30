using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public WeaponData weaponData;
    public GameObject weaponObject;
}
public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons;

    private void OnEnable()
    {
        RefreshWeaponObject();
    }

    private void RefreshWeaponObject()
    {
        foreach(var weapon in weapons)
        {
            if (weapon.weaponObject == null || weapon.weaponData == null) continue;

            weapon.weaponObject.SetActive(weapon.weaponData.isUnlocked);
        }
    }
}
