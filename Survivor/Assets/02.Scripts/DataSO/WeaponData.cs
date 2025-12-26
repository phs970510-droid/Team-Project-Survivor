using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("무기 설정")]
    public GameObject weaponPrefab;
    public string weaponName;
    public bool isUnlocked;
    public int price;

    [Header("무기 스탯")]
    public float damage = 10f;
    public float speed = 10f;
    public float fireCoolTime = 1.0f;
    public int bulletCount = 1;
    public float attackRange = 10f;
    public Sprite Sprite;

    [Header("풀링")]
    public int poolSize;

    [Header("강화")]
    public int upgradeCost;

    [Header("Star")]
    public int starLevel = 0;
    public int maxStar = 5;

}
