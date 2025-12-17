using UnityEngine;

public class CircleWeapon : MonoBehaviour
{
    [SerializeField] private WeaponStat weaponStat;
    [SerializeField] private Transform player;

    private float CircleRadius = 2.0f;
    private float rotateSpeed = 180f;

    private GameObject[] circleObjects;

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
        transform.position = player.position;
        SpinBullets();

        //레벨업하면 새로 생성
        if(circleObjects.Length != weaponStat.bulletCount)
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
            float angle = (360f / bulletCount) * i;  //총알 늘어나면 각도 똑같이

            //원형 좌표 계산
            Vector3 offset = new Vector3(
                Mathf.Sin(angle * Mathf.Deg2Rad),
                Mathf.Cos(angle * Mathf.Deg2Rad),
                0) * CircleRadius;

            GameObject circleBulletObj = Instantiate(
            weaponStat.weaponData.weaponPrefab, transform.position + offset,
            Quaternion.identity, transform);

            //스탯 가져오기
            CircleBullet circleBullet = circleBulletObj.GetComponent<CircleBullet>();
            int playerLayer = player.gameObject.layer;
            circleBullet.BulletStat(weaponStat.damage, playerLayer);

            circleObjects[i] = circleBulletObj;
        }
    }

    private void SpinBullets()
    {
        //시계방향 돌기
        transform.Rotate(-Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void RecreateCircleBullets()
    {
        //기존 총알 삭제
        foreach (GameObject obj in circleObjects)
        {
            Destroy(obj);
        }
        //새로 생성
        StartSpin();
    }
}
