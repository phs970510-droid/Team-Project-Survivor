using UnityEngine;

public class EXP : MonoBehaviour
{
    [Header("자석 세팅")]
    [SerializeField] private float magnetSpeed = 5.0f;
    [SerializeField] private float levelUpRange = 1.0f;

    public int magnetLevel = 0;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (player == null) return;

        float range = magnetLevel * levelUpRange;
        float distance = Vector2.Distance(transform.position, player.position);

        //범위 안이면 자석
        if(range >= distance)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, Time.deltaTime * magnetSpeed);
        }
    }
}
