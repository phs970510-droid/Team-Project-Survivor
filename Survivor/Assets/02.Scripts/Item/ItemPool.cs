using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private int poolSize;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        //게임시작하고 풀 만들기
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewItem();
        }
    }

    private GameObject CreateNewItem()
    {
        GameObject obj = Instantiate(itemPrefab, transform);//생성
        obj.SetActive(false);   //비활성화
        pool.Enqueue(obj);      //풀에 넣기
        return obj;
    }

    public GameObject SpawnItem(Vector3 position)
    {
        if (pool.Count == 0) CreateNewItem();//풀 다 쓰면 다시 만들기

        GameObject obj = pool.Dequeue();    //하나씩 빼기

        obj.transform.SetParent(null);

        obj.transform.position = position;  //위치 지정

        obj.gameObject.SetActive(true); //활성화

        return obj;
    }

    //반환
    public void ReturnItem(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
