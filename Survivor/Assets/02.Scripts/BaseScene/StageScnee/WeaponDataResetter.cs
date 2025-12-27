using System.Collections.Generic;
using UnityEngine;

public class WeaponDataResetter : MonoBehaviour
{
    [SerializeField] private List<WeaponData> allWeaponData;

    private void Awake()
    {
        ResetAll();
    }

    public void ResetAll()
    {
        foreach (var data in allWeaponData)
        {
            data.ResetStat();
        }
    }
}
