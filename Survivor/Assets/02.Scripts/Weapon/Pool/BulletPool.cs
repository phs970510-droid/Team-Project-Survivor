using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private WeaponData weaponData;
    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        weaponData = GetComponent<WeaponData>();
    }

    void Start()
    {
        //풀 사이즈만큼 새로운 총알 만들기
        for(int i = 0; i < weaponData.poolSize; i++)
        {
            CreateNewBullet();
        }
    }

    //총알 만들어서 풀에 넣기
    private GameObject CreateNewBullet()
    {
        GameObject obj = Instantiate(weaponData.weaponPrefab, transform);//생성
        obj.SetActive(false);   //비활성화
        pool.Enqueue(obj);      //풀에 넣기
        return obj;
    }

    public GameObject SpawnBullet(Vector3 position, Quaternion rotation, float lifeTime)
    {
        if (pool.Count == 0) CreateNewBullet();//풀 다 쓰면 다시 만들기

        GameObject obj = pool.Dequeue();    //하나씩 빼기

        //총알 위치, 회전 설정
        Transform t = obj.transform;
        t.position = position;
        t.rotation = rotation;

        obj.gameObject.SetActive(true); //총알 활성화

        return obj;
    }
}
