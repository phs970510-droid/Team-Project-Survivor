using UnityEngine;
using UnityEngine.UI;

public class EXPBar : MonoBehaviour
{
    [Header("EXP")]
    [SerializeField] private Slider expBar;
    [SerializeField] private PlayerLevel playerExp;

    void Update()
    {
        //PlayerLevelÂüÁ¶
        expBar.maxValue = playerExp.playerData.expMax;
        expBar.value = playerExp.currentExp;
    }
}
