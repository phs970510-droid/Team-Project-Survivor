using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private CommonHP playerHP;

    void Update()
    {
        //CommonHPÂüÁ¶
        hpBar.maxValue = playerHP.MaxHP;
        hpBar.value = playerHP.CurrentHP;
    }
}
