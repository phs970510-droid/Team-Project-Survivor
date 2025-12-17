using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//매니저는 관리
public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Pool")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 20;

    private readonly List<GameObject> pool = new();

    private void Awake()
    {
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
        GameObject enemy = GetInactiveEnemy();
        if (enemy == null) 
        { 
            return; 
        }
        enemy.transform.position = position;

        //CommonHP hp=enemy.GetComponent<CommonHP>();
        //if (hp != null)
        //{
        //    hp.ResetHP();
        //}

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
}
