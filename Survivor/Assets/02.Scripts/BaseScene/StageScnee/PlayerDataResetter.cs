using UnityEngine;

public class PlayerDataResetter : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private void Awake()
    {
        ResetAll();
    }

    public void ResetAll()
    {
        playerData.ResetStat();
    }
}
