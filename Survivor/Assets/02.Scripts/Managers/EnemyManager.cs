using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//매니저는 관리
public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Pool")]
    [SerializeField] private GameObject enemyType01;
    [SerializeField] private GameObject enemyType02;
    [SerializeField] private GameObject enemyType03;

    [SerializeField] private int poolSize = 20;
    private GameObject enemyPrefab;

    private GameObject[] pool;

    [Header("BOSS")]
    [SerializeField] private GameObject bossType1;
    [SerializeField] private GameObject bossType2;
    [SerializeField] private GameObject bossType3;
    [SerializeField] private GameObject bossForInfinite;
    
    private GameObject bossPrefab;
    private GameObject bossInfinite;
    public void SetStage(int stageType)
    {
        SelectEnemyPrefab(stageType);
        bossInfinite = bossForInfinite;
        CreatePool();
    }

    public void CreatePool()
    { 
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++) 
        {
            pool[i]=Instantiate(enemyPrefab,transform);
            pool[i].SetActive(false);
        }
    }

    private void SelectEnemyPrefab(int stageType)
    {
        switch (stageType)
        {
            case 1:
                enemyPrefab = enemyType01;
                bossPrefab = bossType1;
                break;
            case 2:
                enemyPrefab = enemyType02;
                bossPrefab = bossType2;
                break;
            case 3:
                enemyPrefab = enemyType03;
                bossPrefab = bossType3;
                break;
            default:
                Debug.LogError($"[EnemyManager] Invalid stageType : {stageType}");
                break;
        }
    }

    public void Spawn(Vector3 position)
    {
        GameObject enemy = GetInactiveEnemy();
        if (enemy == null)
        {
            return;
        }
        enemy.transform.position = position;

        CommonHP hp = enemy.GetComponent<CommonHP>();
        if (hp != null)
        {
            hp.ResetHP();
        }

        var agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.ResetPath();
        }
        enemy.SetActive(true);
    }
    private GameObject GetInactiveEnemy()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeSelf)
                return pool[i];
        }
        return null;
    }

    public void SpawnBoss(GameObject prefab, Vector3 position)
    {
        if (prefab == null)
        {
            return;
        }
        GameObject Boss=Instantiate(prefab);
        Boss.transform.position = position;

        var agent = Boss.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.ResetPath();
        }
        Boss.SetActive(true);
    }
    public void SpawnMid(Vector3 position)
    { 
        SpawnBoss(bossPrefab,position);
    }
    public void SpawnInfinite(Vector3 position)
    {
        SpawnBoss(bossInfinite, position);
    }
}
