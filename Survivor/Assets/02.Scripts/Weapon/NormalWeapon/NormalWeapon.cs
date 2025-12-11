using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class NormalWeapon : WeaponBase
{
    private List<Transform> enemies = new List<Transform>();

    protected override void Update()
    {
        //WeaponBase업데이트 가져오기
        base.Update();

        if (IsShoot())
        {
            FindCloseEnemy();

            if (enemies.Count > 0)
            {
                Shoot();
                ResetFireCoolTime();
            }
        }
    }

    private void FindCloseEnemy()
    {
        //에너미에 태그 붙여주기
        GameObject[] closeEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        //Linq를 이용해 적들을 거리 순으로 정렬하기
        enemies = closeEnemies
            .Select(enemy => enemy.transform)   //GameObject배열을 Transform리스트로 변환
            //firePoint 기준으로 오름차순 정렬
            .OrderBy(enemyTransform => Vector2.Distance(firePoint.position, enemyTransform.position))
            //사거리 내의 적만 찾아내기
            .Where(enemyTransform => Vector2.Distance(firePoint.position, enemyTransform.position) <= weaponStat.weaponData.attackRange)
            .ToList();  //리스트로 변환
    }

    protected override void ShootEachWeapon(GameObject bulletObj, int index)
    {
        //총알이 적들보다 많으면 초과한 인덱스는 파괴
        if(index >= enemies.Count)
        {
            Destroy(bulletObj);
            return;
        }

        Transform currentTarget = enemies[index];
        
        //타겟이 사라지면 파괴(나중에 실험)
        //if(currentTarget == null)
        //{
        //    Destroy(bulletObj);
        //    return;
        //}

        NormalBullet normalBullet = bulletObj.GetComponent<NormalBullet>();

        //총알은 적 향하도록
        Vector2 dir = (currentTarget.position - firePoint.position).normalized;

        //총알 스탯
        normalBullet.BulletStat(
            weaponStat.damage, weaponStat.weaponData.speed, dir);

        //총알이 바라보는 방향으로 회전
        float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        bulletObj.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
