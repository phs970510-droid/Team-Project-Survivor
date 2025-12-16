using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("오프셋")]
    public float arrowOffset = 1.0f;
    public Vector3 hpBarOffset = new Vector3(0, 1, 0);

    [Header("레벨업")]
    public int expMax = 100;
}
