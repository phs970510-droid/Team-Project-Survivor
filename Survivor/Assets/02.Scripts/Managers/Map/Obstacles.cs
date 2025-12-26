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
    private int obstacleSize = 5;
    private Vector3 obstaclePosition = Vector3.zero;

    [Header("컴포넌트")]
    public Transform player;

    [Header("유지시간")]
    [SerializeField] protected float useTimeTypeA = 15.0f;
    [SerializeField] protected float useTimeTypeB = 10.0f;

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
    [SerializeField] protected float intervalTime = 20.0f;

    private Vector3Int currentCenter;
    private Dictionary<float, GameObject> activeObstaclesA = new Dictionary<float, GameObject>();
    private Dictionary<float, GameObject> activeObstaclesB = new Dictionary<float, GameObject>();
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

        int indexA = Random.Range(0, obstaclePrefabsA.Length);

        Vector3 playerPosition = GetPlayerObstacle();

        if (currentTime >= intervalTime)
        {
            if (playerPosition.x < 0 && playerPosition.y < 0)
            {
                int extraX = Random.Range(2, 5);
                int extraY = Random.Range(2, 5);

                playerPosition.x += extraX;
                playerPosition.y += extraY;
                obstaclePosition = new Vector3(
                    playerPosition.x * obstacleSize,
                    playerPosition.y * obstacleSize,
                    0.0f
                    );

                for (int i = 0; i < activeObstaclesA.Count; i++)
                {
                    while (activeObstaclesA[i].transform.position == obstaclePosition)
                    {
                        extraX = Random.Range(2, 5);
                        extraY = Random.Range(2, 5);

                        playerPosition.x += extraX;
                        playerPosition.y += extraY;
                        obstaclePosition = new Vector3(
                            playerPosition.x * obstacleSize,
                            playerPosition.y * obstacleSize,
                            0.0f
                            );
                    }
                }

            }
            else if (playerPosition.x > 0 && playerPosition.y > 0)
            {
                int extraX = Random.Range(-2, -5);
                int extraY = Random.Range(-2, -5);

                playerPosition.x -= extraX;
                playerPosition.y -= extraY;
                obstaclePosition = new Vector3(
                    playerPosition.x * obstacleSize,
                    playerPosition.y * obstacleSize,
                    0.0f
                    );

                for (int i = 0; i < activeObstaclesA.Count; i++)
                {
                    while (activeObstaclesA[i].transform.position == obstaclePosition)
                    {
                        extraX = Random.Range(-2, -5);
                        extraY = Random.Range(-2, -5);

                        playerPosition.x -= extraX;
                        playerPosition.y -= extraY;
                        obstaclePosition = new Vector3(
                            playerPosition.x * obstacleSize,
                            playerPosition.y * obstacleSize,
                            0.0f
                            );
                    }
                }
            }
            else if (playerPosition.x > 0 && playerPosition.y < 0)
            {
                int extraX = Random.Range(2, 5);
                int extraY = Random.Range(-2, 5);

                playerPosition.x += extraX;
                playerPosition.y -= extraY;
                obstaclePosition = new Vector3(
                    playerPosition.x * obstacleSize,
                    playerPosition.y * obstacleSize,
                    0.0f
                    );

                for (int i = 0; i < activeObstaclesA.Count; i++)
                {
                    while (activeObstaclesA[i].transform.position == obstaclePosition)
                    {
                        extraX = Random.Range(2, 5);
                        extraY = Random.Range(-2, 5);

                        playerPosition.x += extraX;
                        playerPosition.y -= extraY;
                        obstaclePosition = new Vector3(
                            playerPosition.x * obstacleSize,
                            playerPosition.y * obstacleSize,
                            0.0f
                            );
                    }
                }
            }
            else if (playerPosition.x < 0 && playerPosition.y > 0)
            {
                int extraX = Random.Range(-2, -5);
                int extraY = Random.Range(2, 5);

                playerPosition.x -= extraX;
                playerPosition.y += extraY;
                obstaclePosition = new Vector3(
                    playerPosition.x * obstacleSize,
                    playerPosition.y * obstacleSize,
                    0.0f
                    );

                for (int i = 0; i < activeObstaclesA.Count; i++)
                {
                    while (activeObstaclesA[i].transform.position == obstaclePosition)
                    {
                        extraX = Random.Range(-2, -5);
                        extraY = Random.Range(2, 5);

                        playerPosition.x -= extraX;
                        playerPosition.y += extraY;
                        obstaclePosition = new Vector3(
                            playerPosition.x * obstacleSize,
                            playerPosition.y * obstacleSize,
                            0.0f
                            );
                    }
                }
            }
            else
            {
                Debug.Log("0,0에서 생성 X");
            }

            obstaclePrefabsA[indexA].position = obstaclePosition;
            obstaclePrefabsA[indexA].gameObject.SetActive(true);
            activeObstaclesA.Add(15.0f, obstaclePrefabsA[indexA].gameObject);
        }

    } //생성간격 조건 추가필요 :: 최대 수 활성화 시 카운트 종료

    //B obstacle은 사용자가 활용할 수 있는 오브젝트 최대 1개 활성화
    protected void ActiveObstacleB()
    {
        if (activeObstaclesB.Count > 0) return;

        Vector3 playerPosition = GetPlayerObstacle();

        if (currentTime >= intervalTime)
        {
            if (playerPosition.x < 0 && playerPosition.y < 0)
            {
                int extraX = Random.Range(3, 5);
                int extraY = Random.Range(3, 5);

                playerPosition.x += extraX;
                playerPosition.y += extraY;
                obstaclePosition = new Vector3(
                    playerPosition.x * obstacleSize,
                    playerPosition.y * obstacleSize,
                    0.0f
                    );

                for (int i = 0; i < activeObstaclesA.Count; i++)
                {
                    while (activeObstaclesA[i].transform.position == obstaclePosition)
                    {
                        extraX = Random.Range(3, 5);
                        extraY = Random.Range(3, 5);

                        playerPosition.x += extraX;
                        playerPosition.y += extraY;
                        obstaclePosition = new Vector3(
                            playerPosition.x * obstacleSize,
                            playerPosition.y * obstacleSize,
                            0.0f
                            );
                    }
                }

            }
            else if (playerPosition.x > 0 && playerPosition.y > 0)
            {
                int extraX = Random.Range(-3, -5);
                int extraY = Random.Range(-3, -5);

                playerPosition.x -= extraX;
                playerPosition.y -= extraY;
                obstaclePosition = new Vector3(
                    playerPosition.x * obstacleSize,
                    playerPosition.y * obstacleSize,
                    0.0f
                    );

                for (int i = 0; i < activeObstaclesA.Count; i++)
                {
                    while (activeObstaclesA[i].transform.position == obstaclePosition)
                    {
                        extraX = Random.Range(-3, -5);
                        extraY = Random.Range(-3, -5);

                        playerPosition.x -= extraX;
                        playerPosition.y -= extraY;
                        obstaclePosition = new Vector3(
                            playerPosition.x * obstacleSize,
                            playerPosition.y * obstacleSize,
                            0.0f
                            );
                    }
                }
            }
            else if (playerPosition.x > 0 && playerPosition.y < 0)
            {
                int extraX = Random.Range(3, 5);
                int extraY = Random.Range(-3, 5);

                playerPosition.x += extraX;
                playerPosition.y -= extraY;
                obstaclePosition = new Vector3(
                    playerPosition.x * obstacleSize,
                    playerPosition.y * obstacleSize,
                    0.0f
                    );

                for (int i = 0; i < activeObstaclesA.Count; i++)
                {
                    while (activeObstaclesA[i].transform.position == obstaclePosition)
                    {
                        extraX = Random.Range(3, 5);
                        extraY = Random.Range(-3, 5);

                        playerPosition.x += extraX;
                        playerPosition.y -= extraY;
                        obstaclePosition = new Vector3(
                            playerPosition.x * obstacleSize,
                            playerPosition.y * obstacleSize,
                            0.0f
                            );
                    }
                }
            }
            else if (playerPosition.x < 0 && playerPosition.y > 0)
            {
                int extraX = Random.Range(-3, -5);
                int extraY = Random.Range(3, 5);

                playerPosition.x -= extraX;
                playerPosition.y += extraY;
                obstaclePosition = new Vector3(
                    playerPosition.x * obstacleSize,
                    playerPosition.y * obstacleSize,
                    0.0f
                    );

                for (int i = 0; i < activeObstaclesA.Count; i++)
                {
                    while (activeObstaclesA[i].transform.position == obstaclePosition)
                    {
                        extraX = Random.Range(-3, -5);
                        extraY = Random.Range(3, 5);

                        playerPosition.x -= extraX;
                        playerPosition.y += extraY;
                        obstaclePosition = new Vector3(
                            playerPosition.x * obstacleSize,
                            playerPosition.y * obstacleSize,
                            0.0f
                            );
                    }
                }
            }
            else
            {
                Debug.Log("0,0에서 생성 X");
            }

            obstaclePrefabsB[0].position = obstaclePosition;
            obstaclePrefabsB[0].gameObject.SetActive(true);
            activeObstaclesB.Add(10.0f, obstaclePrefabsA[0].gameObject);
        }
    } //생성간격 조건 추가필요 :: 여유가 생길 경우 카운트 활성화

    //activeObstacleA 리스트에 추가되는 시점부터 유지시간 적용 후 리스트에서 제거 밑 Setactive:false
    //activeObstacleA, B의 공통 분모를 제외하고 개별 적용되는 사항을 if문으로 구분
    protected void Initialization() //Update
    {
        //여기서 활성화 된 오브젝트 비활성화
    }

    //유지시간
    //배열에 추가 될 때 마다 실행되게
    //인덱스 별로 개별실행
    protected float HordingTime()
    {
        float time = 0.0f;

        if (activeObstaclesA[0] != null && activeObstaclesA.Count > 0 && activeObstaclesA[1] == null)
        {

        }
        else if (activeObstaclesA[1] != null)
        {

        }

        if (activeObstaclesB[0] != null && activeObstaclesB.Count > 0)
        {

        }

        return time;
    }

    //오브젝트 체력
    //protected float ObjectHp()
    //{

    //}

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
