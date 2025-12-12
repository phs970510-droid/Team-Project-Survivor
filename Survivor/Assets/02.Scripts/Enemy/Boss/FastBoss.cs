using System.Collections;
using UnityEngine;

public class FastBoss : MonoBehaviour
{
    [Header("보스 대쉬 설정")]
    [SerializeField] private float dashCoolTime = 5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashCancelTime = 3f;

    private bool isDashed = false;
    private float timer;

    private Transform player;
    private SpriteRenderer sr;
    private CommonHP hp;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponentInChildren<SpriteRenderer>();
        hp = GetComponent<CommonHP>();;
    }

    void Update()
    {
        if (hp.isDead) return;

        timer += Time.deltaTime;

        //대쉬 쿨타임 찼으면 대쉬코루틴
        if(!isDashed && timer >= dashCoolTime)
        {
            StartCoroutine(BossDash());
            timer = 0.0f;
        }

        //좌우반전
        Vector2 dir = player.position - transform.position;
        if (dir.x > 0)
        {
            sr.flipX = false;
        }
        else if (dir.x < 0)
        {
            sr.flipX = true;
        }
    }

    private IEnumerator BossDash()
    {
        isDashed = true;

        //보스가 대쉬할 방향
        Vector2 dir = (player.position - transform.position).normalized;

        float dashingTime = 0.0f;
        //0초부터 대쉬 캔슬까지 플레이어 향해 돌진
        while(dashingTime < dashCancelTime)
        {
            dashingTime += Time.deltaTime;
            transform.position += (Vector3)(dir * dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashed = false;
    }
}
