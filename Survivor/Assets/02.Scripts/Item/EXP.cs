using UnityEngine;

public class EXP : MonoBehaviour
{
    [Header("경험치 세팅")]
    [SerializeField] public float expAmount = 10f;
    [SerializeField] private ItemPool expPool;
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] public float levelUpMlutiplier = 1.1f;

    [Header("자석 세팅")]
    [SerializeField] private float magnetSpeed = 5.0f;
    [SerializeField] public float levelUpRange = 1.0f;
    [SerializeField] public float magnetRange = 0f;

    private Transform player;
    private bool getMagnetItem = false;

    private void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;

            playerLevel = playerObj.GetComponent<PlayerLevel>();
        }
    }
    void Update()
    {
        if (player == null) return;

        MagnetRangeCheck();
    }

    private void MagnetRangeCheck()
    {
        float range;
        //자석 아이템 먹으면 자석 범위는 무한
        if (getMagnetItem)
        {
            range = Mathf.Infinity;
        }
        //평소에는 플레이어 자력
        else
        {
            range = magnetRange;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        //범위 안이면 자석
        if (range >= distance)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, Time.deltaTime * magnetSpeed);
        }
    }

    public void MagnetOn()
    {
        getMagnetItem = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어에 닿으면 플레이어가 경험치 얻고
        if (other.CompareTag("Player"))
        {
            float finalExp = expAmount * levelUpMlutiplier;
            playerLevel.GetEXP(Mathf.RoundToInt(finalExp));
            //경험치는 풀에 반환
            expPool.ReturnItem(this.gameObject);
        }
    }
}
