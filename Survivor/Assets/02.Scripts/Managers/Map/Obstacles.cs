using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Obstacles : MonoBehaviour
{
    #region field
    private int obstacleSize = 5;

    [Header("컴포넌트")]
    public Transform player;

    [Header("유지시간")]
    [SerializeField] protected float useTimeTypeA = 25.0f;
    [SerializeField] protected float useTimeTypeB = 20.0f;

    [Header("오브젝트 체력")]
    [SerializeField] protected int fabricHp;

    [Header("상호작용")]
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected float interactionRange = 3.0f;

    [Header("스폰프리팹")]
    [SerializeField] protected GameObject obstacleParentA;
    [SerializeField] protected Transform[] obstaclePrefabsA;
    [SerializeField] protected GameObject obstacleParentB;
    [SerializeField] protected Transform[] obstaclePrefabsB;

    [Header("스폰간격")]
    protected float currentTime = 0.0f;
    protected float triggerTimeA = 0.0f;
    protected bool isTriggerA = true;
    protected float triggerTimeB = 0.0f;
    protected bool isTriggerB = true;
    [SerializeField] protected float intervalTime = 20.0f;
    protected float obstacleAKey;
    protected float obstacleBKey;

    private Vector3Int currentCenter;
    private Dictionary<float, GameObject> activeObstaclesA = new Dictionary<float, GameObject>();
    private Dictionary<float, GameObject> activeObstaclesB = new Dictionary<float, GameObject>();

    KeyValuePair<float, GameObject> saveObstacleA = new KeyValuePair<float, GameObject>();
    KeyValuePair<float, GameObject> saveObstacleB = new KeyValuePair<float, GameObject>();
    #endregion

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UnActiveObstacle();
    }

    protected virtual void Update()
    {
        Vector3Int newCenter = GetPlayerObstacle();
        if (newCenter != currentCenter)
        {
            currentCenter = newCenter;
        }

        currentTime += Time.deltaTime;
        triggerTimeA += Time.deltaTime;
        triggerTimeB += Time.deltaTime;

        ActiveObstacleA();
        ActiveObstacleB();
        Initialization();
        HoldingA();
        HoldingB();
    }

    #region method
    protected void UnActiveObstacle()
    {
        for (int i = 0; i < obstaclePrefabsA.Length; i++)
        {
            obstaclePrefabsA[i].gameObject.SetActive(false);
        }

        obstaclePrefabsB[0].gameObject.SetActive(false);
    }

    protected Vector3Int GetPlayerObstacle()
    {
        int x = (int)player.position.x;
        int y = (int)player.position.y;
        return new Vector3Int(x, y, 0);
    }

    //생성되지 말아야할 최소범위, 생성 되어야할 범위지정
    //생성하고자 하는 위치가 이미 활성화 되어있는 자리라면 다시 반복
    //A obstacle은 한 번에 최대 2개 까지 활성화
    //겹치는 현상이 잦을 경우 while문 안에 Random.Range의 범위를 조정함으로 빈도 수 조절
    protected void ActiveObstacleA()
    {
        if (activeObstaclesA.Count >= 2) return;

        if (!isTriggerA) return;

        int indexA = Random.Range(0, obstaclePrefabsA.Length);

        Vector3 playerPosition = GetPlayerObstacle();

        if (playerPosition.x > 0 && playerPosition.y > 0)
        {
            int extraX = Random.Range(-2, 5) * obstacleSize;
            int extraY = Random.Range(-2, 5) * obstacleSize;

            playerPosition.x += extraX;
            playerPosition.y += extraY;

            if (activeObstaclesA.ContainsKey(obstacleAKey))
            {
                while (activeObstaclesA[obstacleAKey].transform.position == playerPosition)
                {
                    extraX = Random.Range(-2, 5) * obstacleSize;
                    extraY = Random.Range(-2, 5) * obstacleSize;

                    playerPosition.x += extraX;
                    playerPosition.y += extraY;
                }
            }

        }

        if (playerPosition.x < 0 && playerPosition.y < 0)
        {
            int extraX = Random.Range(-2, 5) * obstacleSize;
            int extraY = Random.Range(-2, 5) * obstacleSize;

            playerPosition.x += extraX;
            playerPosition.y += extraY;

            if (activeObstaclesA.ContainsKey(obstacleAKey))
            {
                while (activeObstaclesA[obstacleAKey].transform.position == playerPosition)
                {
                    extraX = Random.Range(-2, 5) * obstacleSize;
                    extraY = Random.Range(-2, 5) * obstacleSize;

                    playerPosition.x += extraX;
                    playerPosition.y += extraY;
                }
            }
        }

        if (playerPosition.x > 0 && playerPosition.y < 0)
        {
            int extraX = Random.Range(-2, 5) * obstacleSize;
            int extraY = Random.Range(-2, 5) * obstacleSize;

            playerPosition.x += extraX;
            playerPosition.y += extraY;

            if (activeObstaclesA.ContainsKey(obstacleAKey))
            {
                while (activeObstaclesA[obstacleAKey].transform.position == playerPosition)
                {
                    extraX = Random.Range(-2, 5) * obstacleSize;
                    extraY = Random.Range(-2, 5) * obstacleSize;

                    playerPosition.x += extraX;
                    playerPosition.y += extraY;
                }
            }
        }

        if (playerPosition.x < 0 && playerPosition.y > 0)
        {
            int extraX = Random.Range(-2, 5) * obstacleSize;
            int extraY = Random.Range(-2, 5) * obstacleSize;

            playerPosition.x += extraX;
            playerPosition.y += extraY;

            if (activeObstaclesA.ContainsKey(obstacleAKey))
            {
                while (activeObstaclesA[obstacleAKey].transform.position == playerPosition)
                {
                    extraX = Random.Range(-2, 5) * obstacleSize;
                    extraY = Random.Range(-2, 5) * obstacleSize;

                    playerPosition.x += extraX;
                    playerPosition.y += extraY;
                }
            }
        }

        if (playerPosition.x == 0 && playerPosition.y == 0)
        {
            Debug.Log("0,0에서 생성 X");
        }

        obstaclePrefabsA[indexA].position = playerPosition;
        obstaclePrefabsA[indexA].gameObject.SetActive(true);
        obstacleAKey = useTimeTypeA + currentTime;
        activeObstaclesA.Add(obstacleAKey, obstaclePrefabsA[indexA].gameObject);
        isTriggerA = false;

    } //생성간격 조건 추가필요 :: 최대 수 활성화 시 카운트 종료

    //B obstacle은 사용자가 활용할 수 있는 오브젝트 최대 1개 활성화
    protected void ActiveObstacleB()
    {
        if (activeObstaclesB.Count > 0) return;

        if (!isTriggerB) return;

        Vector3 playerPosition = GetPlayerObstacle();

        if (playerPosition.x > 0 && playerPosition.y > 0)
        {
            int extraX = Random.Range(-3, 5) * obstacleSize;
            int extraY = Random.Range(-3, 5) * obstacleSize;

            playerPosition.x += extraX;
            playerPosition.y += extraY;

            if (activeObstaclesA.ContainsKey(obstacleAKey))
            {
                while (activeObstaclesA[obstacleAKey].transform.position == playerPosition)
                {
                    extraX = Random.Range(-3, 5) * obstacleSize;
                    extraY = Random.Range(-3, 5) * obstacleSize;

                    playerPosition.x += extraX;
                    playerPosition.y += extraY;
                }
            }
        }

        if (playerPosition.x < 0 && playerPosition.y < 0)
        {
            int extraX = Random.Range(-3, 5) * obstacleSize;
            int extraY = Random.Range(-3, 5) * obstacleSize;

            playerPosition.x += extraX;
            playerPosition.y += extraY;

            if (activeObstaclesA.ContainsKey(obstacleAKey))
            {
                while (activeObstaclesA[obstacleAKey].transform.position == playerPosition)
                {
                    extraX = Random.Range(-3, 5) * obstacleSize;
                    extraY = Random.Range(-3, 5) * obstacleSize;

                    playerPosition.x += extraX;
                    playerPosition.y += extraY;
                }
            }
        }

        if (playerPosition.x > 0 && playerPosition.y < 0)
        {
            int extraX = Random.Range(-3, 5) * obstacleSize;
            int extraY = Random.Range(-3, 5) * obstacleSize;

            playerPosition.x += extraX;
            playerPosition.y += extraY;

            if (activeObstaclesA.ContainsKey(obstacleAKey))
            {
                while (activeObstaclesA[obstacleAKey].transform.position == playerPosition)
                {
                    extraX = Random.Range(-3, 5) * obstacleSize;
                    extraY = Random.Range(-3, 5) * obstacleSize;

                    playerPosition.x += extraX;
                    playerPosition.y += extraY;
                }
            }
        }

        if (playerPosition.x < 0 && playerPosition.y > 0)
        {
            int extraX = Random.Range(-3, 5) * obstacleSize;
            int extraY = Random.Range(-3, 5) * obstacleSize;

            playerPosition.x += extraX;
            playerPosition.y += extraY;

            if (activeObstaclesA.ContainsKey(obstacleAKey))
            {
                while (activeObstaclesA[obstacleAKey].transform.position == playerPosition)
                {
                    extraX = Random.Range(-3, 5) * obstacleSize;
                    extraY = Random.Range(-3, 5) * obstacleSize;

                    playerPosition.x += extraX;
                    playerPosition.y += extraY;
                }
            }
        }

        if (playerPosition.x == 0 && playerPosition.y == 0)
        {
            Debug.Log("0,0에서 생성 X");
        }

        obstaclePrefabsB[0].position = playerPosition;
        obstaclePrefabsB[0].gameObject.SetActive(true);
        obstacleBKey = useTimeTypeB + currentTime;
        activeObstaclesB.Add(obstacleBKey, obstaclePrefabsB[0].gameObject);
        isTriggerB = false;
    } //생성간격 조건 추가필요 :: 여유가 생길 경우 카운트 활성화

    //activeObstacleA 리스트에 추가되는 시점부터 유지시간 적용 후 리스트에서 제거 밑 Setactive:false
    //activeObstacleA, B의 공통 분모를 제외하고 개별 적용되는 사항을 if문으로 구분
    protected void Initialization() //Update
    {
        //여기서 활성화 된 오브젝트의 키 값이 current시간과 동일하거나 작을 경우 제거한다
        foreach (KeyValuePair<float, GameObject> activeObstacleA in activeObstaclesA)
        {
            Debug.Log($"A : {activeObstacleA.Key}, {activeObstacleA.Value}, " +
                $"{activeObstacleA.Value.transform.position}");
            saveObstacleA = activeObstacleA;
        }

        if (saveObstacleA.Key <= currentTime)
        {
            saveObstacleA.Value.SetActive(false);
            activeObstaclesA.Remove(saveObstacleA.Key);
        }

        foreach (KeyValuePair<float, GameObject> activeObstacleB in activeObstaclesB)
        {
            Debug.Log($"B : {activeObstacleB.Key}, {activeObstacleB.Value}," +
                $"{activeObstacleB.Value.transform.position}");
            saveObstacleB = activeObstacleB;
        }

        if (saveObstacleB.Key <= currentTime)
        {
            saveObstacleB.Value.SetActive(false);
            activeObstaclesB.Remove(saveObstacleB.Key);
        }
    }

    protected void HoldingA()
    {
        if (activeObstaclesA.Count >= 2) return;

        if (isTriggerA) return;

        if (intervalTime <= triggerTimeA)
        {
            isTriggerA = true;
            triggerTimeA = 0.0f;
        }
    }

    protected void HoldingB()
    {
        if (activeObstaclesB.Count > 0) return;

        if (isTriggerB) return;

        if (intervalTime <= triggerTimeB)
        {
            isTriggerB = true;
            triggerTimeB = 0.0f;
        }
    }

    //오브젝트 체력
    protected float ObjectHp()
    {
        float hp = 0.0f;

        return hp;
    }

    //상호작용
    //충돌
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        //몬스터가 겹칠 경우에 한함 몬스터 포지션 Impulse
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    #endregion
}
