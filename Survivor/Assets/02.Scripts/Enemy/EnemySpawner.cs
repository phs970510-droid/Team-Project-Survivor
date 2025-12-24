using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

//스포너는 스폰
public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 설정")]
    public EnemyManager enemyManagers;
    public Vector2 areaMin; //스폰 영역 최소 좌표
    public Vector2 areaMax; //스폰 영역 최대 좌표

    [Header("타이밍 설정")]
    public float startDelay=3.0f;    //초기 스폰 간격
    public float minDelay=0.5f;      //최소 간격(도달 후 고정)
    public float decreasedAmount=0.05f;   //스폰 후 간격 감소량

    [Header("보스 스폰 타이밍")]
    public float bossSpawnTime = 60f;
    public float finalBossSpawnTime = 120f;
    public Transform bossSpawnPoint;

    [Header("충돌 검사")]
    public float checkRadius = 0.6f;
    public LayerMask enemyLayer;

    private float currentDelay;
    private float timer;
    private float bossTimer;
    private bool bossSpawned = false;
    //private bool finalBossSpawned = false;

    private Vector2 spawnPos;

    //플레이어 좌표는 여기서만 참조하고, 몬스터는 이걸 읽는걸로 변경
    public Transform player;
    public Vector3 PlayerPos { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        currentDelay = startDelay;

        int stageType = ChunkManager.Instance.typeNumb;

        enemyManagers.SetStage(stageType);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentDelay)
        {
            TrySpawnEnemy();
            timer = 0f;
            currentDelay=Mathf.Max(minDelay,currentDelay - decreasedAmount);
        }

        bossTimer += Time.deltaTime;
        if (!bossSpawned&&bossTimer >= bossSpawnTime)
        {
            TrySpawnBoss();
        }

        //여기에 무한맵용 최종보스 스폰코드 구현하기
        //if (!bossSpawned && bossTimer >= finalBossSpawnTime)
        //{
        //    TrySpawnInfinite();
        //}

        PlayerPos = new Vector3(player.position.x, player.position.y, 0f);
    }

    private bool TrySpawn()
    {
        for (int i = 0; i < 10; i++)    //최대 10번 위치 재시도하기
        {
            spawnPos = (Vector2)transform.position + new Vector2(
                Random.Range(areaMin.x, areaMax.x),
                Random.Range(areaMin.y, areaMax.y)
                );

            Collider2D hit =Physics2D.OverlapCircle(spawnPos,checkRadius,enemyLayer);
            if (hit == null)
            {
                return true;
            }
        }
        spawnPos = Vector2.zero;
        return false;
    }

    private void TrySpawnEnemy()
    {
        if (TrySpawn()==true)
        {
            enemyManagers.Spawn(spawnPos);
        }
    }
    private void TrySpawnBoss()
    {
        if (bossSpawned) return;
        if (TrySpawn()==true)
        {
            enemyManagers.SpawnMid(spawnPos);
            bossSpawned = true;
        }
    }
    //무한맵 로직 전까진 주석처리 유지
    //private void TrySpawnInfinite()
    //{
    //    if (finalBossSpawned) return;
    //    if (TrySpawn()==true)
    //    {
    //        enemyManagers.SpawnInfinite(spawnPos);
    //        finalBossSpawned = true;
    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3((areaMin.x + areaMax.x) * 0.5f, (areaMin.y + areaMax.y) * 0.5f, 0.0f);
        Vector3 size = new Vector3(Mathf.Abs(areaMax.x - areaMin.x), Mathf.Abs(areaMax.y - areaMin.y), 1.0f);
        Gizmos.DrawWireCube(center,size);
    }
}
