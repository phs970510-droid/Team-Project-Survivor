using System;
using System.Collections;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;
    private GameObject selectPanel;


    public float level = 1;
    public float currentExp = 0;

    public void GetEXP(float exp)
    {
        Debug.Log(gameObject.name);
        currentExp += exp;

        if(currentExp >= playerData.expMax)
        {
            LevelUp();
        }
        Debug.Log($"PlayerLevel currentExp : {currentExp}, 맥스경험치 : {playerData.expMax}");
    }

    private void LevelUp()
    {

        if (playerData == null)
        {
            return;
        }
        if (selectPanel == null)
        {
            SelectPanel panel = FindObjectOfType<SelectPanel>(true);
            if (panel == null)
            {
                return;
            }
            selectPanel = panel.gameObject;
        }

        level++;
        currentExp -= playerData.expMax;

        playerData.expMax += 20; //다음 레벨업 필요경험치 증가

        //선택까지 일시정지
        Time.timeScale = 0f;
        selectPanel.SetActive(true);
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

