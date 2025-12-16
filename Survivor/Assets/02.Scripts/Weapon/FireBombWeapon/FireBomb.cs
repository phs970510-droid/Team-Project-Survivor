using UnityEngine;

public class FireBomb : MonoBehaviour
{
    [Header("화염병 세팅")]
    [SerializeField] private GameObject fireZonePrefab;
    [SerializeField] private float throwSpeed = 3f;         //수평속도
    [SerializeField] private float throwHeight = 2f;        //던지는 높이
    [SerializeField] private float throwTime = 1f;          //던져지는 시간
    [SerializeField] private float fireZoneLeave = 3f;      //장판 지속시간

    private float fireDamage;
    public float fireRadius;

    private Vector2 dir;
    private float shootTimer;
    private Vector3 startPos;

    private int ignoreLayer;

    public void Init(Vector2 dir, float damage, float radius)
    {
        this.dir = dir.normalized;
        fireDamage = damage;
        fireRadius = radius;

        ignoreLayer = LayerMask.NameToLayer("Player");
        startPos = transform.position;
    }

    //보스 전용 무작위로 쏘기
    public void BossInit(Vector3 pos, float damage, float radius)
    {
        fireDamage = damage;
        fireRadius = radius;

        ignoreLayer = LayerMask.NameToLayer("Enemy");
        startPos = transform.position;

        dir = (pos  - startPos).normalized;

        float dist = Vector3.Distance(startPos, pos);
        throwSpeed = dist / throwTime;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;

        float t = shootTimer / throwTime;

        //수평으로 이동
        Vector3 h = startPos + (Vector3)(dir * throwSpeed * t);

        //수직 이동(2차 방정식 형태)
        float height = 4 * throwHeight * t * (1 - t);

        //수평 수직 적용
        transform.position = h + new Vector3(0, height, 0);

        //땅에 폭발
        if(t >= 1f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        GameObject fireZone = Instantiate(fireZonePrefab, transform.position, Quaternion.identity);
        fireZone.GetComponent<FireZone>().FireZoneStat(fireDamage, fireRadius, ignoreLayer);

        Destroy(fireZone, fireZoneLeave);
        Destroy(gameObject);
    }
}
