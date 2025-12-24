using UnityEngine;

[CreateAssetMenu(fileName = "BaseData", menuName = "Data/BaseData")]

public class BaseData : ScriptableObject
{
    [Header("기본 능력치")]
    public float moveSpeed = 5.0f;
    public float maxHp = 100;
    public float magnetRange = 0f; //플레이어만 사용
    public float damage = 10.0f; //enemy만 사용

    [Header("Base Upgrade Cost")]
    public int[] baseUpgradeCosts;

    //상점 스탯 올라갈 시 저장
    public void SaveStat()
    {
        PlayerPrefs.SetFloat(name + "_moveSpeed", moveSpeed);
        PlayerPrefs.SetFloat(name + "_maxHp", maxHp);
        PlayerPrefs.Save();
    }

    //저장된 스탯 가져오기
    public void LoadStat()
    {
        moveSpeed = PlayerPrefs.GetFloat(name + "_moveSpeed", moveSpeed);
        maxHp = PlayerPrefs.GetFloat(name + "_maxHp", maxHp);
    }
}
