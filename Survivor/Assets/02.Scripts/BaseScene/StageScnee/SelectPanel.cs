using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectPanel : MonoBehaviour
{
    public SelectItemButton[] buttons;     
    public List<WeaponData> allWeaponData;
    private List<WeaponData> runtimeWeaponData;

    public WeaponStat playerWeaponStat;



    private void Awake()
    {
        runtimeWeaponData = new List<WeaponData>();
        foreach(var data in allWeaponData)
        {
            WeaponData runtime = Instantiate(data);
            runtime.starLevel = 0;
            runtimeWeaponData.Add(runtime);
        }
    }
    private void OnEnable()
    {
        ShowRandomItems();
    }

    public  void ShowRandomItems()
    {
        List<WeaponData> temp = new List<WeaponData>(runtimeWeaponData);

        for (int i = 0; i < buttons.Length; i++)
        {
            int rand = Random.Range(0, temp.Count);
            WeaponData data = temp[rand];
            temp.RemoveAt(rand);

            buttons[i].SetData(data);


        }

    }
}
