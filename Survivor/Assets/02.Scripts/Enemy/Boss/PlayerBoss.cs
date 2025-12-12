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

    [Header("드론 패턴")]
    [SerializeField] private GameObject dpPrefab;
    [SerializeField] private int droneCount = 10;
    [SerializeField] private float droneDelay = 0.01f;
    [SerializeField] private float droneDamage = 5f;
    [SerializeField] private Transform drone;

    private bool isPattern = false;

    private Transform player;
    private SpriteRenderer sr;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
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

        //패턴 랜덤레인지
        int patternIndex = Random.Range(0, 4);

        switch (patternIndex)
        {
            case 0:
                yield return StartCoroutine(NormalShoot());
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
        //패턴 사용하면 쿨타임간 기다리기
        yield return new WaitForSeconds(patternCoolTime);

        isPattern = false;
    }

    //노말 패턴은 플레이어 향해 1발씩 n번 쏘기
    private IEnumerator NormalShoot()
    {
        for (int i = 0; i < normalCount; i++)
        {
            GameObject bulletObj = Instantiate(npPrefab, transform.position, Quaternion.identity);

            Vector2 dir = (player.position - transform.position).normalized;

            EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();
            bullet.BulletDamage(normalDamage);
            bullet.BulletDirection(dir);

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

    //화염병 패턴은 n이내 랜덤한 곳 n개 뿌리기
    private IEnumerator FireBombShoot()
    {
        for (int i = 0; i < normalCount; i++)
        {
            GameObject bulletObj = Instantiate(fpPrefab, transform.position, Quaternion.identity);

            //7이내 랜덤으로 쏘기
            Vector2 randomPos = Random.insideUnitCircle * 7f;
            Vector2 randomDir = randomPos.normalized;

            FireBomb fire = bulletObj.GetComponent<FireBomb>();
            fire.Init(randomDir, fireDamage, fireRadius);

            yield return new WaitForSeconds(fireDelay);
        }
    }

    //드론 패턴은 n이내 랜덤한 곳 n개 뿌리기
    private IEnumerator DroneShoot()
    {
        for(int i = 0;i < droneCount; i++)
        {
            //7이내 랜덤으로 쏘기
            Vector2 randomPos = Random.insideUnitCircle * 7f;
            Vector2 randomDir = randomPos.normalized;

            GameObject bulletObj = Instantiate(dpPrefab, drone.position, Quaternion.identity);

            EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();
            bullet.BulletDamage(normalDamage);
            bullet.BulletDirection(randomDir);

            yield return new WaitForSeconds(droneDelay);
        }
    }
}
