using UnityEngine;

[CreateAssetMenu(fileName = "BaseData", menuName = "Data/BaseData")]

public class BaseData : ScriptableObject
{
    [Header("기본 능력치")]
    public float moveSpeed = 5.0f;
    public float maxHp = 100;
    public float damage = 10.0f; //enemy만 사용
}
