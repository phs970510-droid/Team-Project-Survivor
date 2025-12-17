using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI moneyText;
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMoney(int money)
    {
        if(moneyText!= null)
        moneyText.text = $"${money}";
        ;
    }


}