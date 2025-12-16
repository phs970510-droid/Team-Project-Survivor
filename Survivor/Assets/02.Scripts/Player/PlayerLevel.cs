using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;

    public GameObject openSelectItme; //wy추가
    public GameObject joyStick; //wy추가
    public SelectItem SelectItem;

    public int level = 1;
    public int currentExp = 0;

    public void GetEXP(int exp)
    {
        currentExp += exp;

        if(currentExp >= playerData.expMax)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        currentExp -= playerData.expMax;

        playerData.expMax += 20; //다음 레벨업 필요경험치 증가
        //UI 추가
        joyStick.SetActive(false);
        openSelectItme.SetActive(true); //wy추가
        SelectItem.SelectItemSO();


        //선택까지 일시정지
        Time.timeScale = 0f;
    }

    public void BaseStatUp(BaseData baseData)
    {
        baseData.maxHp *= 1.1f;
        baseData.moveSpeed *= 1.1f;
    }

    public void WeaponStatUp(WeaponStat weaponStat)
    {
        weaponStat.LevelUpStat();
    }

    public void MagnetStatUp(EXP exp)
    {
        exp.magnetLevel++;
    }
}
