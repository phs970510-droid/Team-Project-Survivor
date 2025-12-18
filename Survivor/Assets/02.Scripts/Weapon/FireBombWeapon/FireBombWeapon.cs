using UnityEngine;

public class FireBombWeapon : WeaponBase
{
    [SerializeField] private Transform playerArrow; //던지는 방향
    [SerializeField] private BulletPool fireZonePool;

    protected override void Update()
    {
        if(playerArrow == null)
        {
            FindPlayerArrow();
            return;
        }
        base.Update();

        if (IsShoot())
        {
            Shoot();
            ResetFireCoolTime();
        }
    }

    //자동으로 플레이어 방향 찾기
    private void FindPlayerArrow()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            //플레이어 자식에 있는 애로우 트랜스폼 찾기
            playerArrow = player.transform.Find("Arrow");
        }
    }

    protected override void ShootEachWeapon(GameObject bulletObj, int index)
    {
        //기본 방향 - 플레이어 방향으로 쏘기
        float baseAngle = playerArrow.eulerAngles.z;

        //총알 늘어날 시
        int count = weaponStat.bulletCount;
        float angle = 360f / count;

        float shootAngle = baseAngle + angle * index;

        bulletObj.transform.rotation = Quaternion.Euler(0, 0, shootAngle);

        Vector2 dir = bulletObj.transform.up;

        FireBomb bomb = bulletObj.GetComponent<FireBomb>();

        //스탯 전달하기
        bomb.Init(dir, weaponStat.damage, bomb.fireRadius, bulletPool, fireZonePool);
    }
}
