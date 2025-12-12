using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("무기 설정")]
    public GameObject weaponPrefab;
    public string weaponName;
    public bool isLocked;
    public float damage = 10f;
    public float speed = 10f;
    public float fireCoolTime = 1.0f;
    public int bulletCount = 1;
    public float attackRange = 10f;
    public int price;
}
