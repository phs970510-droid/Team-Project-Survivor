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
    private void Start()
    {
        if (DataManager.Instance != null)
        {
            UpdateMoney(DataManager.Instance.Money);
        }
    }

    public void UpdateMoney(int money)
    {
        if(moneyText!= null)
        moneyText.text = $"${money}";
        ;
    }


}