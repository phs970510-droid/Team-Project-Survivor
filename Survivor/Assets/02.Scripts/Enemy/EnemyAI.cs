using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    //슛 에너미는 슛에너미 스크립트에서 플립 적용
    [SerializeField] private bool useFlip;

    private EnemyManager enemyManager;
    private TutorialEnemySpawner tutorialEnemySpawner;
    private NavMeshAgent agent;
    private SpriteRenderer sr;
    private CommonHP hp;

    private Coroutine followRoutine;
    private const float UPDATE_INTERVAL = 0.2f;

    private void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        tutorialEnemySpawner = GetComponentInParent<TutorialEnemySpawner>();
        agent = GetComponent<NavMeshAgent>();
        sr = GetComponentInChildren<SpriteRenderer>();
        hp = GetComponent<CommonHP>();

        //2D에 맞게 z축은 회전안하도록
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }

    private void OnEnable()
    {
        if (followRoutine != null)
        {
            StopCoroutine(followRoutine);
        }
        followRoutine = StartCoroutine(FollowPlayer());
    }

    private void OnDisable()
    {
        if(followRoutine != null)
            StopCoroutine(followRoutine);
    }
    private IEnumerator FollowPlayer()
    {
        while (true)
        {
            if (!hp.isDead && agent.isOnNavMesh)
            {
                if (enemyManager == null)
                {
                    //스포너에서 PlayerPos 받기
                    agent.SetDestination(tutorialEnemySpawner.PlayerPos);   //PlayerPos는 enemyManager, 혹은 튜토용 스포너에서 이미 Vector3(z=0)으로 관리중

                }
                else
                {
                    agent.SetDestination(enemyManager.PlayerPos);
                }

                //좌우반전
                if (agent.velocity.x > 0)
                {
                    sr.flipX = false;
                }
                else if (agent.velocity.x < 0)
                {
                    sr.flipX = true;
                }
            }
                yield return new WaitForSeconds(UPDATE_INTERVAL);
            //Debug.Log($"PlayerPos={spawner.PlayerPos}");
            //Debug.Log($"AgentDest={agent.destination}");
        }
    }
}
