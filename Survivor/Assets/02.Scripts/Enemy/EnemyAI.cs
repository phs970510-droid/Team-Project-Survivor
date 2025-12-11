using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //슛 에너미는 슛에너미 스크립트에서 플립 적용
    [SerializeField] private bool useFlip;

    private Transform player;
    private NavMeshAgent agent;
    private SpriteRenderer sr;
    private CommonHP hp;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponentInChildren<SpriteRenderer>();
        hp = GetComponent<CommonHP>();

        //2D에 맞게 z축은 회전안하도록
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }
    void Update()
    {
        if (player == null || hp.isDead) return;

        //Enemy는 플레이어 따라가게
        agent.SetDestination(player.position);

        //좌우반전
        if(agent.velocity.x > 0)
        {
            sr.flipX = false;
        }
        else if(agent.velocity.x < 0)
        {
            sr.flipX = true;
        }
    }
}
