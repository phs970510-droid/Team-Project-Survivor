using UnityEngine;

public class CircleWeapon : MonoBehaviour
{
    [SerializeField] private WeaponStat weaponStat;
    [SerializeField] private Transform player;
    [SerializeField] private BulletPool pool;

    private float circleRadius = 2.0f;
    private float rotateSpeed = 180f;

    private GameObject[] circleObjects;

    private void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Start()
    {
        if (weaponStat != null)
        {
            weaponStat.StartStat();
        }
        StartSpin();
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.position;
        }
        //회전축 시계방향 돌기
        transform.Rotate(-Vector3.forward, rotateSpeed * Time.deltaTime);

        //레벨업하면 새로 생성
        if (circleObjects.Length != weaponStat.bulletCount)
        {
            RecreateCircleBullets();
        }
    }

    private void StartSpin()
    {
        int bulletCount = weaponStat.bulletCount;
        circleObjects = new GameObject[bulletCount];

        //원형으로 총알 배치
        for (int i = 0; i < bulletCount; i++)
        {
            //원형 탄 풀링하기
            GameObject circleBulletObj = pool.SpawnBullet(
            transform.position, Quaternion.identity, 0f);

            //회전축을 따라 돌기 위한 부모 설정
            circleBulletObj.transform.SetParent(this.transform);

            float angle = (360f / bulletCount) * i;  //총알 늘어나면 각도 똑같이

            //탄 위치설정
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * circleRadius;
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * circleRadius;
            circleBulletObj.transform.localPosition = new Vector3(x, y, 0f);

            //원형 좌표 계산
            //Vector3 offset = new Vector3(
            //    Mathf.Sin(angle * Mathf.Deg2Rad),
            //    Mathf.Cos(angle * Mathf.Deg2Rad),
            //    0) * circleRadius;

            //스탯 가져오기
            CircleBullet circleBullet = circleBulletObj.GetComponent<CircleBullet>();
            int playerLayer = player.gameObject.layer;
            circleBullet.BulletStat(weaponStat.damage, playerLayer);

            circleObjects[i] = circleBulletObj;
        }
    }

    //총알 늘어날 시 비활성화, 다시 만들기
    private void RecreateCircleBullets()
    {
        //기존 총알 삭제
        foreach (GameObject obj in circleObjects)
        {
            gameObject.SetActive(false);
        }
        //새로 생성
        StartSpin();
    }

    private void OnDestroy()
    {
        //회전축이 파괴되면 자동으로 풀리턴
        ReturnPool();
    }
    private void ReturnPool()
    {
        foreach (GameObject obj in circleObjects)
        {
            if (obj != null && obj.activeSelf)
            {
                obj.transform.SetParent(pool.transform);
                pool.ReturnBullet(obj);
            }
        }
    }
}