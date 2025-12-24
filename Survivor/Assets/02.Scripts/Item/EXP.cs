癤using UnityEngine;

public class EXP : MonoBehaviour
{
    [Header("경험치 세팅")]
    [SerializeField] public float expAmount = 10f;
    [SerializeField] private ItemPool expPool;
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] public float levelUpMiutiplier = 1.1f;

    [Header(" 명")]
    [SerializeField] private float magnetSpeed = 5.0f;
    [SerializeField] public float levelUpRange = 1.0f;
    [SerializeField] public float magnetRange = 0f;

    public int magnetLevel = 0;
    public int expLevel = 0;
    private Transform player;
    private bool getMagnetItem = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player == null) return;

        // 댄 癒뱀쇰㈃  踰 臾댄
        if (getMagnetItem)
        {
            range = Mathf.Infinity;
        }
        // �댁 �
        else
        {
            float baseRange = DataManager.Instance.baseData.magnetRange;
            range = baseRange + (magnetLevel * levelUpRange);
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
        //�댁댁 우쇰㈃ �댁닿 寃쏀移 산�
        if (other.CompareTag("Player"))
        {
            playerLevel.GetEXP(expAmount);
            //寃쏀移  諛
            expPool.ReturnItem(this.gameObject);
        }
    }
}
