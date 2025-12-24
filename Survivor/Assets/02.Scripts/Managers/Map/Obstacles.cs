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
    public Transform player;

    [Header("유지시간")]
    [SerializeField] protected float useTime;

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
    [SerializeField] protected float intervalTime = 20.0f;

    private Vector2Int currentCenter;
    private List<GameObject> activeObstacles = new List<GameObject>();
    #endregion

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        Vector2Int newCenter = GetPlayerObstacle();
        if (newCenter != currentCenter)
        {
            currentCenter = newCenter;
        }
    }

    #region method
    protected void UnActiveObstacle()
    {

    }

    protected Vector2Int GetPlayerObstacle()
    {
        int x = (int)player.position.x;
        int y = (int)player.position.y;
        return new Vector2Int(x, y);
    }

    protected void ActiveObstacle()
    {
        

    }

    protected void Initialization()
    {

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
