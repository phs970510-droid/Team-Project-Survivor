using System.Collections.Generic;
using UnityEngine;
/*
기능 기입

공통적으로 들어갈 기능

-유지 시간 : protected
    ㄴTypeA: 15s
    ㄴTypeB: 10s

-오브젝트 체력 : abstract
    ㄴTypeA: none
    ㄴTypeB: player maxHp * 2.5

-상호작용 : abstract
    ㄴTypeA: none
    ㄴTypeB: E key
    -충돌
        ㄴTypeA: 플레이어, 몬스터
        ㄴTypeB: 플레이어, 몬스터

-스폰간격 : protected

-스폰 : 드랍 시 오브젝트 겹칠경우? 사망 : 밀어내기
    ㄴ OnCollisionEnter2D : 충돌 시 밀어내기
    ㄴ OnCollisionStay2D : if 오브젝트 위치와 겹쳐서 enter가 씹힐 경우 : 밀어내기
    ㄴ OnCollisionExit2D : 미정
*/

//부모클레스에서 컴포넌트 할당코드가 자식클래스에서 구동이 되는지 확인필요
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Obstacles : MonoBehaviour
{
    #region field
    [Header("컴포넌트")]
    protected Rigidbody2D rb;

    protected Transform player;

    protected int obstacleSize = 5;
    protected int loadRange = 6;
    protected int nearRange = 3;
    protected int unloadRange = 6;

    [Header("유지시간")]
    [SerializeField] protected float useTime;

    [Header("오브젝트 체력")]
    [SerializeField] protected int fabricHp;

    [Header("상호작용")]
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected float interactionRange = 3.0f;

    [Header("스폰프리팹")]
    [SerializeField] protected GameObject obstacleParents;
    [SerializeField] protected Transform[] obstaclePrefabs;

    [Header("생성된 장애물 활용")]
    [SerializeField] protected GameObject createdParent;
    [SerializeField] protected Transform[] createdPrefabs;

    [Header("스폰간격")]
    [SerializeField] protected float intervalTime = 20.0f;

    private Vector2Int currentCenter;
    private Dictionary<Vector2Int, GameObject> activeObstacles = new Dictionary<Vector2Int, GameObject>();
    private GameObject checkIndex;
    [SerializeField] private List<GameObject> activeTrue = new List<GameObject>();
    #endregion

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CreateObstacles();
    }

    protected virtual void Update()
    {
        Vector2Int newCenter = GetPlayerObstacle();
        if (newCenter != currentCenter)
        {
            currentCenter = newCenter;
            UpdateObstacle();
        }
    }

    #region method
    protected Vector2Int GetPlayerObstacle()
    {
        int x = Mathf.FloorToInt(player.position.x / obstacleSize);
        int y = Mathf.FloorToInt(player.position.y / obstacleSize);
        return new Vector2Int(x, y);
    }

    protected void CreateObstacles()
    {
        HashSet<Vector2Int> needed = new HashSet<Vector2Int>();

        for (int x = -loadRange; x <= loadRange; x++)
        {
            for (int y = -loadRange; y <= loadRange; y++)
            {
                needed.Add(currentCenter + new Vector2Int(x, y));
            }
        }

        foreach (var key in needed)
        {
            Vector3 pos = new Vector3(key.x * obstacleSize, key.y * obstacleSize, 0.0f);
            GameObject prefab = GetObstaclePrefab();
            GameObject obstacle = Instantiate(
                prefab,
                pos,
                Quaternion.identity);
            activeObstacles[key] = obstacle;
        }
    }

    protected void UpdateObstacle()
    {
        HashSet<Vector2Int> needed = new HashSet<Vector2Int>();

        for (int x = -loadRange; x <= loadRange; x++)
        {
            for (int y = -loadRange; y <= loadRange; y++)
            {
                needed.Add(currentCenter + new Vector2Int(x, y));
            }
        }

        List<Vector2Int> toFalse = new List<Vector2Int>();
        foreach (var kv in activeObstacles)
        {
            Vector2Int key = kv.Key;
            if (Mathf.Abs(key.x - currentCenter.x) > unloadRange ||
                Mathf.Abs(key.y - currentCenter.y) > unloadRange)
            {
                kv.Value.SetActive(false);
                toFalse.Add(key);
                activeTrue.Remove(kv.Value);
            }
        }

        foreach (var key in toFalse)
        {
            activeObstacles.Remove(key);
        }

        foreach ( var key in needed)
        {
            if (!activeObstacles.ContainsKey(key))
            {
                Vector3 pos = new Vector3(key.x * obstacleSize, key.y * obstacleSize, 0.0f);
                GameObject prefab = SelectPrefab();

                prefab.transform.position = pos;
                prefab.SetActive(true);

                GameObject obstacle = prefab;
                activeObstacles[key] = obstacle;
                activeTrue.Add(obstacle);
            }
        }
    }

    protected GameObject GetObstaclePrefab()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0)
        {
            Debug.LogError("[Obstacles : GetObstaclePrefab()] 옵스타클 프리팹이 설정되지 않았습니다.");
            return null;
        }

        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            return obstaclePrefabs[i].gameObject;
        }

        return null;
    }

    protected GameObject SelectPrefab()
    {
        if (createdPrefabs == null || createdPrefabs.Length == 0) return null;

        checkIndex = createdPrefabs[Random.Range(0, createdPrefabs.Length)].gameObject;

        GameObject isActive = activeTrue.Find(x => x.gameObject == checkIndex);

        if (isActive != null)
        {
            while(isActive != null)
            {
                checkIndex = createdPrefabs[Random.Range(0, createdPrefabs.Length)].gameObject;
                isActive = activeTrue.Find(x => x.gameObject == checkIndex);
            }
        }

        return checkIndex;
    }

    //유지시간
    protected abstract void HordingTime();

    //오브젝트 체력
    protected abstract void ObjectHp();

    //상호작용
    //충돌
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    //스폰간격
    protected abstract void SpawnInteraction();

    //스폰 조건&&etc
    protected void ActiveSpawn()
    {

    }
    #endregion
}
