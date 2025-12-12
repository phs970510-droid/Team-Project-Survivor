using UnityEngine;

public class BossCircleWeapon : MonoBehaviour
{
    public GameObject circlePrefab;
    public float radius;
    public float rotateSpeed;
    public int count;
    public float damage;

    public Transform boss;
    private GameObject[] circleObjects;

    private void Awake()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
    }
    void Start()
    {
        StartSpin();
    }

    void Update()
    {
        transform.position = boss.position;

        transform.Rotate(-Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void StartSpin()
    {
        circleObjects = new GameObject[count];

        //원형으로 총알 배치
        for (int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i;  //총알 늘어나면 각도 똑같이

            //원형 좌표 계산
            Vector3 offset = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                Mathf.Cos(angle * Mathf.Deg2Rad),
                0) * radius;

            GameObject circleBulletObj = Instantiate(
            circlePrefab, transform.position + offset, Quaternion.identity, transform);

            //스탯 가져오기
            CircleBullet circleBullet = circleBulletObj.GetComponent<CircleBullet>();
            circleBullet.BulletStat(damage);

            circleObjects[i] = circleBulletObj;
        }
    }
}
