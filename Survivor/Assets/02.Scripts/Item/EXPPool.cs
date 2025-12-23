using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPPool : MonoBehaviour
{
    [SerializeField] private GameObject expPrefab;
    [SerializeField] private int poolSize;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        //게임시작하고 풀 만들기
        for(int i = 0; i < poolSize; i++)
        {
            CreateNewEXP();
        }
    }

    private GameObject CreateNewEXP()
    {
        GameObject obj = Instantiate(expPrefab, transform);//생성
        obj.SetActive(false);   //비활성화
        pool.Enqueue(obj);      //풀에 넣기
        return obj;
    }

    public GameObject SpawnEXP(Vector3 position)
    {
        if (pool.Count == 0) CreateNewEXP();//풀 다 쓰면 다시 만들기

        GameObject obj = pool.Dequeue();    //하나씩 빼기

        obj.gameObject.SetActive(true); //활성화

        return obj;
    }

    //반환
    public void ReturnEXP(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
