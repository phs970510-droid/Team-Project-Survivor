using System.Collections;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;

    public GameObject openSelectItme; //wy추가
    public GameObject joyStick; //wy추가
    public SelectItem selectItem; //WY추가

    public float level = 1;
    public float currentExp = 0;


    private void Start()
    {
        if(selectItem)
        selectItem = FindObjectOfType<SelectItem>();

    }
    public void GetEXP(float exp)
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
        joyStick.SetActive(false);//wy추가
        openSelectItme.SetActive(true); //wy추가
        selectItem.SelectItemSO();//wy추가


        //선택까지 일시정지
        Time.timeScale = 0f;
    }

    public void HPStatUp(BaseData baseData)
    {
        baseData.maxHp *= 1.1f;
    }

    public void SpeedStatUp(BaseData baseData)
    {
         baseData.moveSpeed *= 1.1f;
    }

    public void WeaponStatUp(WeaponStat weaponStat)
    {
        weaponStat.LevelUpStat();
    }

    public void MagnetStatUp(EXP exp)
    {
        exp.magnetRange += exp.levelUpRange;
    }
    
    public void EXPStatUp(EXP exp)
    {
        exp.expAmount *= exp.levelUpRange;
    }
}

