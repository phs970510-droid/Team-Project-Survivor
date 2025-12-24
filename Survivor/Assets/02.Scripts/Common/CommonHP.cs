using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CommonHP : MonoBehaviour
{
    [SerializeField] private BaseData baseData;
    private float currentHP;

    protected Animator anim;
    protected Rigidbody2D rb;

    public bool isDead = false;
    protected float invincibleTime = 0.5f;
    protected bool isInvincible = false;

    [Header("에너미 죽었을 때")]
    [SerializeField] private float expChance = 0.2f;
    [SerializeField] private ItemPool bigPool;
    [SerializeField] private ItemPool normalPool;
    [SerializeField] private float coinChance = 0.2f;
    [SerializeField] private ItemPool coinPool;

    [Header("보스 보상")]
    [SerializeField] private GameObject bossReward;

    [Header("실드")]
    [SerializeField] private float damageReduce = 0.3f;
    [SerializeField] private float sheildTime = 5f;
    private bool hasShield = false;

    //플레이어 HP 체력바에 참조
    public float CurrentHP => currentHP;
    public float MaxHP => baseData.maxHp;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHP = baseData.maxHp;
    }

    private void OnEnable()
    {
        isDead = false;
        isInvincible=false;
    }

    public void Damage(float damage)
    {
        TakeDamage(damage);
    }
    protected virtual void TakeDamage(float damage)
    {
        if (isDead) return; //죽었으면 리턴하기
        if (CompareTag("Player") && isInvincible)
            return; //플레이어가 무적이면 리턴하기

        //실드 가지고 있으면 데미지감소
        if (hasShield)
        {
            damage *= (1f - damageReduce);
        }
        currentHP -= damage;

        StartCoroutine(HitAnimation());

        //무적시간 코루틴
        StartCoroutine(InvincibleCoroutine());

        if(currentHP <= 0)
        {
            Die();
        }
    }

    private IEnumerator HitAnimation()
    {
        //맞는 애니메이션 0.1초만 지속
        anim.SetBool("Hitted", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Hitted", false);
    }

    //무적시간
    private IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void Die()
    {
        if(isDead) return;

        isDead = true;
        anim.SetTrigger("Die");

        //콜라이더 없애기(플레이어 데미지 안닪게)
        Collider2D collider = GetComponent<Collider2D>();
        if(collider != null) collider.enabled = false;

        StartCoroutine(ActiveFalse());

        //플레이어 죽었다면 게임끝
        if (CompareTag("Player"))
        {
            //게임오버 / UI
        }
        //에너미,보스라면
        else if (CompareTag("Enemy") || CompareTag("Boss"))
        {
            DieEnemy();
        }
    }

    private void DieEnemy()
    {
        //EnemyAI멈추기
        EnemyAI enemyAI = GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.enabled = false;
        }
        //이동 없애기
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.isStopped = true;
            agent.velocity = Vector2.zero;
        }
        if (CompareTag("Enemy"))
        {
            DropEXP();
            DropCoin();
        }
        if (CompareTag("Boss"))
        {
            DropReward();
            GameObject exitObj = GameObject.Find("ExitGate");
            if(exitObj == null) Debug.Log("게이트오브젝트 없음");
            if (exitObj != null)
            {
                ExitGate exitGate = exitObj.GetComponent<ExitGate>();
                if (exitGate == null) Debug.Log("게이트에 스크립트 없음");
                else
                {
                    exitGate.ShowExitGate();
                    Debug.Log("게이트 소환");
                }
            }
        }

        gameObject.tag = "DeadEnemy";   //플레이어가 공격 안하도록 태그변경
    }

    private void DropEXP()
    {
        float rand = Random.value;
        GameObject normalObj = GameObject.Find("NormalEXPPool");
        GameObject bigObj = GameObject.Find("BigEXPPool");
        normalPool = normalObj.GetComponent<ItemPool>();
        bigPool = bigObj.GetComponent<ItemPool>();

        if (bigPool != null && normalPool != null)
        {
            if (expChance >= rand)
            {
                bigPool.SpawnItem(transform.position);
            }
            else
            {
                normalPool.SpawnItem(transform.position);
            }
        }
    }

    private void DropCoin()
    {
        float rand = Random.value;
        GameObject coinObj = GameObject.Find("CoinPool");
        coinPool = coinObj.GetComponent<ItemPool>();

        if (coinPool != null && coinChance >= rand)
        {
            coinPool.SpawnItem(transform.position + Vector3.right * 0.5f);
        }
    }

    private void DropReward()
    {
        Instantiate(bossReward, transform.position, Quaternion.identity);
    }

    public void ResetHP()
    {
        if(baseData != null)
        {
            currentHP = baseData.maxHp; //hp �ǵ�����
        }
        
        isDead = false;
        isInvincible = false;

        anim.ResetTrigger("Die");   //애니메이션 리셋

        //콜라이더 되돌리기
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = true;

        gameObject.tag = "Enemy";   //태그 되돌리기

        //AI되돌리기
        EnemyAI enemyAI = GetComponent<EnemyAI>();
        if (enemyAI != null)
        {
            enemyAI.enabled = true;
        }
        //이동 되돌리기
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        if (!agent.isOnNavMesh)
        {
            Debug.Log("에너미isOnNavMesh아님");
        }
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = false;
        }

        gameObject.SetActive(true);
    }

    public void GetShieldItem()
    {
        StartCoroutine(ShieldCoroutine());
    }

    //실드시간 만큼 해스실드 on
    private IEnumerator ShieldCoroutine()
    {
        hasShield = true;

        yield return new WaitForSeconds(sheildTime);

        hasShield = false;
    }

    //�ִϸ��̼� �� ��Ȱ��ȭ
    private IEnumerator ActiveFalse()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
