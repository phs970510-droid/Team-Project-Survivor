using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour
{
    [SerializeField] private Slider expBar;
    [SerializeField] private PlayerLevel playerExp;


    private void Start()
    {
        playerExp = FindObjectOfType<PlayerLevel>();
    }
    void Update()
    {
        //PlayerLevelÂüÁ¶
        expBar.maxValue = playerExp.playerData.expMax;
        expBar.value = playerExp.currentExp;
    }
}
