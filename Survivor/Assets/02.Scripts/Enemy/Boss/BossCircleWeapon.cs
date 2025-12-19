using UnityEngine;

//보스 회전축을 담당
public class BossCircleWeapon : MonoBehaviour
{
    public GameObject circlePrefab;
    public float radius;
    public float rotateSpeed;
    public int count;
    public float damage;
    public Transform boss;
    private BulletPool pool;

    private GameObject[] circleObjects;

    public void Init(float damage, float radius, float rotateSpeed, int count, BulletPool pool, Transform boss)
    {
        this.damage = damage;
        this.radius = radius;
        this.rotateSpeed = rotateSpeed;
        this.count = count;
        this.pool = pool;
        this.boss = boss;

        StartSpin();
    }

    void Update()
    {
        if(boss != null)
        {
            transform.position = boss.position;
        }
        //회전축 돌기
        transform.Rotate(-Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void StartSpin()
    {
        circleObjects = new GameObject[count];

        //원형으로 총알 배치
        for (int i = 0; i < count; i++)
        {
            //원형 탄 풀링하기
            GameObject circleBulletObj = pool.SpawnBullet(
            transform.position, Quaternion.identity, 0f);

            //회전축을 따라 돌기 위한 부모 설정
            circleBulletObj.transform.SetParent(this.transform);

            float angle = (360f / count) * i;  //총알 늘어나면 각도 똑같이

            //탄 위치설정
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            circleBulletObj.transform.localPosition = new Vector3(x, y, 0f);

            //원형 좌표 계산
            //Vector3 offset = new Vector3(
            //    Mathf.Sin(angle * Mathf.Deg2Rad),
            //    Mathf.Cos(angle * Mathf.Deg2Rad),
            //    0) * radius;

            //스탯 가져오기
            CircleBullet circleBullet = circleBulletObj.GetComponent<CircleBullet>();
            int bossLayer = boss.gameObject.layer;
            circleBullet.BulletStat(damage, bossLayer);

            circleObjects[i] = circleBulletObj; 
        }
    }

    private void OnDestroy()
    {
        //회전축이 파괴되면 자동으로 풀리턴
        ReturnPool();
    }
    private void ReturnPool()
    {
        foreach(GameObject obj in circleObjects)
        {
            if (obj != null && obj.activeSelf)
            {
                obj.transform.SetParent(pool.transform);
                pool.ReturnBullet(obj);
            }
        }
    }
}
