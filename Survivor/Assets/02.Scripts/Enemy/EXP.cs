using UnityEngine;

public class EXP : MonoBehaviour
{
    [Header("자석 세팅")]
    [SerializeField] private float magnetSpeed = 5.0f;
    [SerializeField] private float levelUpRange = 1.0f;

    private float range;
    public int magnetLevel = 0;
    private Transform player;

    private bool getMagnetItem = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player == null) return;

        //자석 아이템 먹으면 자석 범위는 무한
        if (getMagnetItem)
        {
            range = Mathf.Infinity;
        }
        //평소에는 플레이어 자력
        else
        {
            range = magnetLevel * levelUpRange;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        //범위 안이면 자석
        if(range >= distance)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, Time.deltaTime * magnetSpeed);
        }
    }

    public void MagnetOn()
    {
        getMagnetItem = true;
    }
}
