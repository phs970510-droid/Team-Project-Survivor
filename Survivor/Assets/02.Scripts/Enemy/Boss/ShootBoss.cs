using System.Collections;
using UnityEngine;

public class ShootBoss : MonoBehaviour
{
    [SerializeField] private float patternCoolTime = 5f;
    [SerializeField] private GameObject bossBulletPrefab;
    [SerializeField] private BaseData baseData;

    [Header("부채꼴 패턴")]
    [SerializeField] private int coneBulletCount = 5;   //한번에 5발
    [SerializeField] private float spreadAngle = 60f;   //60도 나눠서
    [SerializeField] private float coneShootDelay = 0.3f;//0.3초에 한번 발사
    [SerializeField] private int conShootCount = 5;     //5번 발사(총 25발)

    [Header("원형 패턴")]
    [SerializeField] private int circleBulletCount = 36;

    [Header("랜덤 패턴")]
    [SerializeField] private int randomBulletCount = 40;    //40발
    [SerializeField] private float randomShootDelay = 0.1f; //0.1초마다

    private bool isPattern = false;
    private float damage;

    private SpriteRenderer sr;
    private Transform player;

    private void Awake()
    {
        damage = baseData.damage;
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        int patternIndex = Random.Range(0, 3);
   
        switch (patternIndex)
        {
            case 0:
                yield return StartCoroutine(ConeShoot());
                break;
            case 1:
                yield return StartCoroutine(CircleShoot());
                break;
            case 2:
                yield return StartCoroutine(RandomShoot());
                break;
        }
        //패턴 사용하면 쿨타임간 기다리기
        yield return new WaitForSeconds(patternCoolTime);

        isPattern = false;
    }

    //원뿔모양 우다다 코루틴
    private IEnumerator ConeShoot()
    {
        //방향은 플레이어 향해
        Vector2 dir = (player.position - transform.position).normalized;

        //벡터 방향에서 각도로 변환
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //시작각도, 각도간격 계산
        float startAngle = angle - spreadAngle * 0.5f;
        float angleStep = spreadAngle / (coneBulletCount - 1);

        //10발씩 5번 와다다다 쏘기
        for (int s = 0; s < conShootCount; s++)
        {
            for (int i = 0; i < coneBulletCount; i++)
            {
                //발사할 총알의 각도
                float shootAngle = startAngle + angleStep * i;

                //각도에서 벡터로 변환
                Vector2 shootDir = new Vector2(Mathf.Cos(shootAngle * Mathf.Deg2Rad), Mathf.Sin(shootAngle * Mathf.Deg2Rad));

                Shoot(shootDir);
            }
            yield return new WaitForSeconds(coneShootDelay);
        }
    }

    //원형 패턴
    private IEnumerator CircleShoot()
    {
        float angleStep = 360f / circleBulletCount;

        for(int i = 0;i < circleBulletCount; i++)
        {
            float shootAngle = angleStep * i;
            
            Vector2 dir = new Vector2(Mathf.Cos(shootAngle * Mathf.Rad2Deg), Mathf.Sin(shootAngle * Mathf.Rad2Deg));

            Shoot(dir);
        }
        yield return null;
    }

    //랜덤으로 쏘는 패턴
    private IEnumerator RandomShoot()
    {
        for(int i = 0; i< randomBulletCount; i++)
        {
            //0~360 random
            float angle = Random.Range(0, 360);

            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Rad2Deg), Mathf.Sin(angle * Mathf.Rad2Deg));

            Shoot(dir);

            yield return new WaitForSeconds(randomShootDelay);
        }
    }
    //총쏘는 공통 로직
    private void Shoot(Vector2 dir)
    {
        GameObject bossBullet = Instantiate(bossBulletPrefab, transform.position, Quaternion.identity);

        EnemyBullet eb = bossBullet.GetComponent<EnemyBullet>();
        eb.BulletDamage(damage);
        eb.BulletDirection(dir);
    }
}
