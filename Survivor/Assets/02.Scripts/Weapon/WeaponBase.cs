using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponStat weaponStat;
    public Transform firePoint;
    public GameObject bulletObj;

    protected float fireTimer;

    protected virtual void Start()
    {
        if(weaponStat != null)
        {
            weaponStat.StartStat();
        }
    }
    protected virtual void Update()
    {
        if( fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
        }
    }

    protected void Shoot()
    {
        for(int i = 0; i < weaponStat.bulletCount; i++)
        {
            bulletObj = Instantiate(weaponStat.weaponData.weaponPrefab, firePoint.position, Quaternion.identity);

            //각 무기에 맞는 총 발사 구현하기
            ShootEachWeapon(bulletObj, i);
        }
    }

    //자식에서 세부사항 구현하기
    protected abstract void ShootEachWeapon(GameObject bulletObj, int index);

    protected bool IsShoot()
    {
        return fireTimer <= 0f;
    }

    protected void ResetFireCoolTime()
    {
        fireTimer = weaponStat.fireCoolTime;
    }
}
