using System.Collections;
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
[RequireComponent(typeof(Collider2D))]
public abstract class Obstacles : MonoBehaviour
{
    #region field
    [Header("컴포넌트")]
    protected Collider2D col;
    protected Rigidbody2D rb;

    protected Transform player;

    [Header("유지시간")]
    [SerializeField] protected float useTime;

    [Header("오브젝트 체력")]
    [SerializeField] protected int fabricHp;

    [Header("상호작용")]
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected float interactionRange = 3.0f;

    [Header("스폰")]
    [SerializeField] Transform prefabParents;
    [SerializeField] GameObject[] prefabs;

    [Header("스폰간격")]
    [SerializeField] protected float intervalTime = 20.0f;
    #endregion

#if UNITY_EDITOR //부모클래스 상속시킬경우 자식클래스 작동되는지 확인필요
    public void OnValidate()
    {
        prefabs = prefabParents.GetComponentsInChildren<GameObject>();
    }
#endif

    void Awake()
    {
        col = GetComponent<Collider2D>();//오류 생기면 col->collider로 모두 바꿔주기
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        
    }

    #region method
    //유지시간


    //오브젝트 체력


    //상호작용
    //충돌
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어 : 충돌 했을 때 움직임을 멈추고 충돌해온 좌표방향으로 튕겨내기
        //ㄴ플레이어에 구문을 추가해야 되는가 ?
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //플레이어 :
        //장애물 : 나중에 생성된 장애물이 겹칠 경우 좌표 값 다시 갱신
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //플레이어 :
        //장애물 : 
    }

    //스폰간격


    //스폰 조건&&etc


    #endregion
}
