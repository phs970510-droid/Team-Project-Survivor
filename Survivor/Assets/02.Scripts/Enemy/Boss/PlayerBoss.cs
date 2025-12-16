using System.Collections;
using UnityEngine;

public class PlayerBoss : MonoBehaviour
{
    [SerializeField] private float patternCoolTime = 5f;
    [SerializeField] private BaseData baseData;

    [Header("노말 패턴")]
    [SerializeField] private GameObject npPrefab;
    [SerializeField] private int normalCount = 5;
    [SerializeField] private float normalDelay = 0.5f;
    [SerializeField] private float normalDamage = 10f;
    [SerializeField] private float normalSpeed = 10f;

    [Header("원형 패턴")]
    [SerializeField] public GameObject cpPrefab;
    [SerializeField] private float circleDelay = 3f;
    [SerializeField] private int circleCount = 4;
    [SerializeField] private float circleRadius = 3f;
    [SerializeField] private float circleRotateSpeed = 180f;
    [SerializeField] private float circleDamage = 10f;

    [Header("화염병 패턴")]
    [SerializeField] private GameObject fpPrefab;
    [SerializeField] private int fireCount = 10;
    [SerializeField] private float fireDelay = 0.3f;
    [SerializeField] private float fireDamage = 5f;
    [SerializeField] private float fireRadius = 3f;
    [SerializeField] private float minRadius = 3f;
    [SerializeField] private float maxRadius = 10f;

    [Header("드론 패턴")]
    [SerializeField] private GameObject dpPrefab;
    [SerializeField] private int droneCount = 10;
    [SerializeField] private float droneDelay = 0.01f;
    [SerializeField] private float droneDamage = 5f;
    [SerializeField] private int shootCount = 5;
    [SerializeField] private float droneRadius = 7f;
    [SerializeField] private Transform drone;

    private bool isPattern = false;

    private Transform player;
    private SpriteRenderer sr;
    private Animator anim;

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
        int patternIndex = Random.Range(0, 0);

        switch (patternIndex)
        {
            case 0:
                yield return StartCoroutine(FireBombShoot());
                break;
            case 1:
                yield return StartCoroutine(CircleShoot());
                break;
            case 2:
                yield return StartCoroutine(FireBombShoot());
                break;
            case 3:
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
        for (int i = 0; i < normalCount; i++)
        {
            //총은 플레이어 향하게
            Vector2 dir = (player.position - transform.position).normalized;

            GameObject bulletObj = Instantiate(npPrefab, transform.position, Quaternion.identity);

            //스탯 적용
            BossBullet bullet = bulletObj.GetComponent<BossBullet>();
            bullet.BossBulletStat(normalDamage, normalSpeed, dir);

            //플레이어 바라보기
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            bulletObj.transform.rotation = Quaternion.Euler(0, 0, angle);

            yield return new WaitForSeconds(normalDelay);
        }
    }

    //원형 패턴은 n개 무기가 n초동안 유지
    private IEnumerator CircleShoot()
    {
        //원형무기 오브젝트 생성
        GameObject circle = new GameObject("BossCircleBullet");
        circle.transform.position = transform.position;

        //스크립트 가져오기
        BossCircleWeapon weapon = circle.AddComponent<BossCircleWeapon>();

        weapon.boss = this.transform;
        weapon.circlePrefab = cpPrefab;
        weapon.radius = circleRadius;
        weapon.rotateSpeed = circleRotateSpeed;
        weapon.count = circleCount;
        weapon.damage = circleDamage;

        yield return new WaitForSeconds(circleDelay);
        //지속시간 후 삭제
        Destroy(circle);
    }

    //화염병 패턴은 min ~ max이내 랜덤한 곳 n개 뿌리기
    private IEnumerator FireBombShoot()
    {
        for (int i = 0; i < fireCount; i++)
        {
            GameObject bulletObj = Instantiate(fpPrefab, transform.position, Quaternion.identity);

            //방향 랜덤
            Vector2 dir = Random.insideUnitCircle.normalized;

            //min ~ max 사이 랜덤값
            float radius = Random.Range(minRadius, maxRadius);
            Vector2 randomPos = (Vector2)transform.position + dir * radius;

            FireBomb bomb = bulletObj.GetComponent<FireBomb>();
            bomb.BossInit(randomPos, fireDamage, fireRadius);

            yield return new WaitForSeconds(fireDelay);
        }
    }

    //드론 패턴은 n이내 랜덤한 곳 k개 i번 뿌리기
    private IEnumerator DroneShoot()
    {
        for (int k = 0; k < shootCount; k++)
        {
            for (int i = 0; i < droneCount; i++)
            {
                //7이내 랜덤으로 쏘기
                Vector2 randomPos = Random.insideUnitCircle * droneRadius;
                Vector2 randomDir = randomPos.normalized;

                GameObject bulletObj = Instantiate(dpPrefab, drone.position, Quaternion.identity);

                EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();
                bullet.BulletDamage(normalDamage);
                bullet.BulletDirection(randomDir);

                yield return new WaitForSeconds(droneDelay);
            }
        }
    }
}