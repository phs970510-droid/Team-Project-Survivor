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

    [Header("경험치 프리팹(에너미만 할당)")]
    [SerializeField] private GameObject EXPPrefab;
    //큰 경험치도 추가해야 함

    //플레이어 HP 체력바에 참조
    public float CurrentHP => currentHP;
    public float MaxHP => baseData.maxHp;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHP = baseData.maxHp;
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

        Destroy(gameObject, 1.0f);

        //플레이어 죽었다면 게임끝
        if (CompareTag("Player"))
        {
            //게임오버 / UI
        }
        //에너미라면
        else if (CompareTag("Enemy"))
        {
            DieEnemy();
        }
    }

    private void DieEnemy()
    {
        gameObject.tag = "DeadEnemy";   //플레이어가 공격 안하도록 태그변경

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
        //경험치 떨어뜨리는 것 추가
        DropEXP();
    }

    private void DropEXP()
    {
        if(EXPPrefab != null)
        {
            Instantiate(EXPPrefab, transform.position, Quaternion.identity);
        }
    }
}
