using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShootEnemy : MonoBehaviour
{
    [SerializeField] private WeaponData data;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float lifeTime = 3f;

    private bool isShooting = false;
    private Coroutine shootCoroutine;

    private Transform player;
    private NavMeshAgent agent;
    private SpriteRenderer sr;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponent<SpriteRenderer>();

        //2D설정
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        //사거리 안이면 멈추고 발사하기
        if (distance <= data.attackRange)
        {
            //이동 멈추기
            if(agent != null)
            {
                agent.isStopped = true;
            }
            //공격
            if (!isShooting)
            {
                isShooting = true;
                shootCoroutine = StartCoroutine(ShootCoroutine());
            }
        }
        else
        {
            //사거리 밖이면 다시 이동
            if(agent != null)
            {
                agent.isStopped = false;
            }
            //총 안쏨
            if (isShooting)
            {
                isShooting = false;
                if (shootCoroutine != null)
                {
                    StopCoroutine(shootCoroutine);
                    shootCoroutine = null;
                }
            }
            agent.SetDestination(player.position);
        }

        //Shoot Enemy전용 좌우반전
        Vector2 dir = player.position - transform.position;
        if(dir.x > 0)
        {
            sr.flipX = false;
        }
        else if(dir.x < 0)
        {
            sr.flipX = true;
        }
    }

    private IEnumerator ShootCoroutine()
    {
        //첫 발사 딜레이
        yield return new WaitForSeconds(data.fireCoolTime);
        while (true)
        {
            EnemyShoot();
            yield return new WaitForSeconds(data.fireCoolTime);
        }
    }

    private void EnemyShoot()
    {
        GameObject bulletObj = bulletPool.SpawnBullet(transform.position, Quaternion.identity, lifeTime);

        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();
        bullet.Init(data.damage, (player.position - transform.position).normalized, lifeTime, bulletPool);
    }
}
