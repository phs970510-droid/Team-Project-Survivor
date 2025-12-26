using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    public WeaponData weaponData;
    public int level = 1;
    public float damage;
    public float fireCoolTime;
    public int bulletCount;

    private int maxLevel = 5;
    public void StartStat()
    {
        level = 1;
        damage = weaponData.damage;
        fireCoolTime = weaponData.fireCoolTime;
        bulletCount = weaponData.bulletCount;
    }

    public void LevelUpStat()
    {
        if (!IsMaxLevel())
        {
            level++;
            damage *= 1.1f;      //데미지 1.1배
            fireCoolTime *= 0.9f;//발사쿨타임 0.9배
            bulletCount += 1;
        }
        else return;
    }

    public bool IsMaxLevel()
    {
        if (level >= maxLevel) return true;
        else return false;
    }
}
