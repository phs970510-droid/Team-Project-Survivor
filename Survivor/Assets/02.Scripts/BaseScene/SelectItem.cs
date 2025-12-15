using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SelectItem : MonoBehaviour
{
    public List<GameObject> itemButtons;

    public List<WeaponData> itmes = new List<WeaponData>();

    public void SelectItem(int index)
    {
        WeaponData weapon = itmes[index];
    }
}
