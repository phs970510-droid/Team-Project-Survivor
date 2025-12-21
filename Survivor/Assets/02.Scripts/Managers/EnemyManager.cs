using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//매니저는 관리
public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Pool")]
    [SerializeField] private GameObject enemyPrefab;   //실제 풀에 쓸 프리팹이므로 넣으면 안됨
    [SerializeField] private GameObject enemyType01;
    [SerializeField] private GameObject enemyType02;
    [SerializeField] private GameObject enemyType03;

    [SerializeField] private int poolSize = 20;

    private readonly List<GameObject> pool = new();

    public bool isActiveStage = false;

    private void Start()
    {
        int stageType = ChunkManager.Instance.typeNumb;
        SelectEnemyPrefab(stageType);
        CreatePool();
    }
    public void CreatePool()
    {
        for (int i = 0; i < poolSize; i++) 
        {
            GameObject enemy=Instantiate(enemyPrefab,transform);
            enemy.SetActive(false);
            pool.Add(enemy);
        }
    }

    public void Spawn(Vector3 position)
    {
        if (!isActiveStage)
            return;

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

        var agent=enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        { 
            agent.ResetPath();
        }
        enemy.SetActive(true);
    }
    private GameObject GetInactiveEnemy()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
                return pool[i];
        }
        return null;
    }
    private void SelectEnemyPrefab(int stageType)
    {
        switch (stageType)
        {
            case 1:
                enemyPrefab = enemyType01;
                break;
            case 2:
                enemyPrefab = enemyType02;
                break;
            case 3:
                enemyPrefab = enemyType03;
                break;
            default:
                Debug.LogError($"[EnemyManager] Invalid stageType : {stageType}");
                break;
        }
    }
}
