using UnityEngine;

public class FireBombWeapon : WeaponBase
{
    [SerializeField] private Transform playerArrow; //던지는 방향

    protected override void Update()
    {
        base.Update();

        if (IsShoot())
        {
            Shoot();
            ResetFireCoolTime();
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
        bomb.Init(dir, weaponStat.damage, bomb.fireRadius);
    }
}
