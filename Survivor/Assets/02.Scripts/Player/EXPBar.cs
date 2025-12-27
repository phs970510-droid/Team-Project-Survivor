using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour
{
    [SerializeField] private Slider expBar;
    private PlayerLevel playerLevel;


    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerLevel = player.GetComponent<PlayerLevel>();
    }
    void Update()
    {
        UpdateEXPBar();

        Debug.Log($"플레이어 이름 : {playerLevel.gameObject.name}, EXPBar.currentExp : {playerLevel.currentExp}");
    }

    private void UpdateEXPBar()
    {
        expBar.maxValue = playerLevel.playerData.expMax;
        expBar.value = playerLevel.currentExp;
    }
}
