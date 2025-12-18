using UnityEngine;

public class FireBomb : MonoBehaviour, IPoolable
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
    private float timer;
    private Vector3 startPos;

    private BulletPool pool;
    private BulletPool fireZonePool;

    //화염병 스탯
    public void Init(Vector2 dir, float damage, float radius, BulletPool pool, BulletPool fireZonePool)
    {
        this.dir = dir.normalized;
        fireDamage = damage;
        fireRadius = radius;
        timer = 0f;
        this.pool = pool;
        this.fireZonePool = fireZonePool;

        startPos = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float t = timer / throwTime;

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

    public void ReturnPool()
    {
        if (pool != null)
        {
            pool.ReturnBullet(this.gameObject);
        }
    }
    public void Spawn() { }

    private void Explode()
    {
        //터졌을 때 파이어존 풀링으로 가져오기
        GameObject fireZoneObj = fireZonePool.SpawnBullet(transform.position, Quaternion.identity, fireZoneLeave);
        FireZone fireZone = fireZoneObj.GetComponent<FireZone>();
        //파이어존 스탯 전달
        fireZone.FireZoneStat(fireDamage, fireRadius, fireZoneLeave, pool);

        //파이어 봄도 풀
        ReturnPool();
    }
}
