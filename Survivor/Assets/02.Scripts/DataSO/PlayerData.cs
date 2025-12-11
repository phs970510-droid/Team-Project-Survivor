using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("화살표 오프셋")]
    public float offset = 1.0f;

    [Header("레벨업")]
    public int expMax = 100;
}
