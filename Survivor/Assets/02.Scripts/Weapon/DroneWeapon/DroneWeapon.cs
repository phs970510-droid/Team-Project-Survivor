using System.Collections;
using UnityEngine;

public class DroneWeapon : WeaponBase
{
    [Header("드론 위치 설정")]
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float playerDistance = 2f;

    [Header("드론 발사 설정")]
    [SerializeField] private float shootDelay = 0.1f;   //1발간 딜레이(쿨타임은 따로)

    protected override void Awake()
    {
        base.Awake();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        shooter = transform;
    }
    protected override void Update()
    {
        base.Update();

        FollowPlayer();

        if( IsShoot() )
        {
            StartCoroutine(DroneShoot());
            ResetFireCoolTime();
        }
    }

    private void FollowPlayer()
    {
        if (player == null) return;

        //플레이어 기준 위치
        Vector3 pos = player.position + Vector3.right * playerDistance + Vector3.up * 1f;
        //플레이어를 향해 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, pos, followSpeed * Time.deltaTime);
    }

    private IEnumerator DroneShoot()
    {
        for (int i = 0; i < weaponStat.bulletCount; i++)
        {
            Shoot();
            yield return new WaitForSeconds(shootDelay);    //0.1초마다 쏘기
        }
    }

    protected override void ShootEachWeapon(GameObject bulletObj, int index)
    {
        //반지름 3f 이내에서 랜덤한 위치
        Vector2 randomPos = Random.insideUnitCircle * 3f;
        Vector2 randomDir = randomPos.normalized;

        DroneBullet droneBullet = bulletObj.GetComponent<DroneBullet>();
        //스탯전달
        droneBullet.Init(randomDir, weaponStat.weaponData.speed, weaponStat.damage, bulletPool);
    }
}
