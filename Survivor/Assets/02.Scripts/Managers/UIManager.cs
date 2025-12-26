using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public EnemyKillCount enemyKillCount;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI killCountText;
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
    void Update()
    {
        if(enemyKillCount != null)
        {
            killCountText.text = enemyKillCount.killcount.ToString();
        }
    }


}