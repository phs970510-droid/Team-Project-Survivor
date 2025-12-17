using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private CommonHP playerHP;
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerData playerData;

    void LateUpdate()
    {
        if (playerHP == null || player == null) return;
        //CommonHP참조
        hpBar.maxValue = playerHP.MaxHP;
        hpBar.value = playerHP.CurrentHP;
        
        //월드(UI)좌표에서 스크린 좌표로 변환
        Vector3 pos = mainCamera.WorldToScreenPoint(player.position + playerData.hpBarOffset);
        transform.position = pos;
    }
}
