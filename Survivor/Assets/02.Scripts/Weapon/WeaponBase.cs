using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected WeaponStat weaponStat;

    [SerializeField] protected Transform firePoint;
    [SerializeField] protected BulletPool bulletPool;
    [SerializeField] protected float lifeTime;

    protected float fireTimer;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

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
        firePoint.position = player.position;

        for (int i = 0; i < weaponStat.bulletCount; i++)
        {
            GameObject bulletObj = bulletPool.SpawnBullet
                (firePoint.position, Quaternion.identity, lifeTime);

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
