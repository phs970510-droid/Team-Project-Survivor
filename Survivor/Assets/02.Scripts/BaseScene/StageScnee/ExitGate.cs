
using System.Collections;
using UnityEngine;

public class ExitGate : MonoBehaviour
{
    private MissionBoard stageSceneLode;

    [Header("탈출설정")]
    [SerializeField] private float showDelay = 10f; //시작 후 나오는 시간
    [SerializeField] private float distance = 10f;  //플레이어와 거리

    private Transform player;
    private Collider2D col;
    private SpriteRenderer sr;
    private void Awake()
    {
        stageSceneLode = FindObjectOfType<MissionBoard>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        //gameObject.SetActive(false);
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        col.enabled = false;//게임시작하면 안 닿게
        sr.enabled = false; //게임시작하면 안 보이게
    }

    private void Start()
    {
        StartCoroutine(ShowExitGate());
    }

    private IEnumerator ShowExitGate()
    {
        yield return new WaitForSeconds(showDelay);
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        //플레이어 포지션 랜덤한 dist거리에 생성
        Vector3 pos = player.position + (Vector3)(randomDir * distance);

        transform.position = pos;
        //gameObject.SetActive(true);
        this.col.enabled = true;
        this.sr.enabled = true;
        //yield return new WaitForSeconds(showDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            stageSceneLode.BaseSceneLoder();
        }
    }
}
