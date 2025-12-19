using System.Collections;
using UnityEngine;

public class PlayerBoss : MonoBehaviour
{
    [Header("기본 설정")]
    [SerializeField] private float patternCoolTime = 5f;
    [SerializeField] private BaseData baseData;

    [Header("노말 패턴")]
    [SerializeField] private WeaponData normalPattern;
    [SerializeField] private float normalLife = 4f;
    [SerializeField] BulletPool normalPool;

    [Header("원형 패턴")]
    [SerializeField] private WeaponData circlePattern;
    [SerializeField] private float circleDelay = 3f;
    [SerializeField] private float circleRadius = 3f;
    [SerializeField] private float circleRotateSpeed = 180f;
    [SerializeField] private GameObject circlePivot;
    [SerializeField] BulletPool circlePool;

    [Header("화염병 패턴")]
    [SerializeField] private WeaponData fireBombPattern;
    [SerializeField] private float fireRadius = 3f;
    [SerializeField] private float minRadius = 3f;
    [SerializeField] private float maxRadius = 10f;
    [SerializeField] private float fireLife = 2f;
    [SerializeField] BulletPool firePool;
    [SerializeField] BulletPool fireZonePool;

    [Header("드론 패턴")]
    [SerializeField] private WeaponData dronePattern;
    [SerializeField] private float droneRadius = 7f;
    [SerializeField] private Transform drone;
    [SerializeField] private float droneLife = 3f;
    [SerializeField] private int burstCount = 10;
    [SerializeField] private BulletPool dronePool;

    private bool isPattern = false;

    private Transform player;
    private SpriteRenderer sr;
    private Animator anim;
    private WeaponData data;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isPattern)
        {
            StartCoroutine(Pattern());
        }

        //좌우반전
        Vector2 dir = player.position - transform.position;
        if (dir.x > 0)
        {
            sr.flipX = false;
        }
        else if (dir.x < 0)
        {
            sr.flipX = true;
        }
    }

    private IEnumerator Pattern()
    {
        isPattern = true;
        anim.SetBool("Pattern", true);

        //패턴 랜덤레인지
        int patternIndex = Random.Range(0, 4);

        switch (patternIndex)
        {
            case 0:
                data = normalPattern;
                yield return StartCoroutine(NormalShoot());
                break;
            case 1:
                data = circlePattern;
                yield return StartCoroutine(CircleShoot());
                break;
            case 2:
                data = fireBombPattern;
                yield return StartCoroutine(FireBombShoot());
                break;
            case 3:
                data = dronePattern;
                yield return StartCoroutine(DroneShoot());
                break;
        }
        anim.SetBool("Pattern", false);
        //패턴 사용하면 쿨타임간 기다리기
        yield return new WaitForSeconds(patternCoolTime);

        isPattern = false;
    }

    //노말 패턴은 플레이어 향해 1발씩 n번 쏘기
    private IEnumerator NormalShoot()
    {
        for (int i = 0; i < data.bulletCount; i++)
        {
            //총은 플레이어 향하게
            Vector2 dir = (player.position - transform.position).normalized;

            GameObject bulletObj = normalPool.SpawnBullet(transform.position, Quaternion.identity, normalLife);

            //스탯 적용
            BossBullet bullet = bulletObj.GetComponent<BossBullet>();
            bullet.BossBulletStat(data.damage, data.speed, dir, normalPool);

            //플레이어 바라보기
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            bulletObj.transform.rotation = Quaternion.Euler(0, 0, angle);

            yield return new WaitForSeconds(data.fireCoolTime);
        }
    }

    //원형 패턴은 n개 무기가 n초동안 유지
    private IEnumerator CircleShoot()
    {
        //원형무기 오브젝트 생성
        GameObject pivotObj = Instantiate(circlePivot, transform.position, Quaternion.identity);

        //스크립트 가져오기
        BossCircleWeapon weapon = pivotObj.GetComponent<BossCircleWeapon>();

        weapon.Init(data.damage, circleRadius, circleRotateSpeed, data.bulletCount, circlePool, this.transform);

        yield return new WaitForSeconds(circleDelay);
        //지속시간 후 삭제
        Destroy(pivotObj);
    }

    //화염병 패턴은 min ~ max이내 랜덤한 곳 n개 뿌리기
    private IEnumerator FireBombShoot()
    {
        for (int i = 0; i < data.bulletCount; i++)
        {
            GameObject bulletObj = firePool.SpawnBullet(transform.position, Quaternion.identity, fireLife);

            //방향 랜덤
            Vector2 dir = Random.insideUnitCircle.normalized;

            //min ~ max 사이 랜덤값
            float radius = Random.Range(minRadius, maxRadius);
            Vector2 randomPos = (Vector2)transform.position + dir * radius;

            BossFireBomb bomb = bulletObj.GetComponent<BossFireBomb>();
            bomb.BossInit(randomPos, data.damage, fireRadius, firePool, fireZonePool);

            yield return new WaitForSeconds(data.fireCoolTime);
        }
    }

    //드론 패턴은 n이내 랜덤한 곳 k개 i번 뿌리기
    private IEnumerator DroneShoot()
    {
        for (int k = 0; k < burstCount; k++)
        {
            for (int i = 0; i < data.bulletCount; i++)
            {
                //7이내 랜덤으로 쏘기
                Vector2 randomPos = Random.insideUnitCircle * droneRadius;
                Vector2 randomDir = randomPos.normalized;

                GameObject bulletObj = dronePool.SpawnBullet(drone.position, Quaternion.identity, droneLife);

                EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();
                bullet.Init(data.damage, randomDir, droneLife, dronePool);

                yield return new WaitForSeconds(data.fireCoolTime);
            }
        }
    }
}