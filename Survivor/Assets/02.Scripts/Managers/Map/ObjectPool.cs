using UnityEngine;
using System.Collections.Generic;

/*
배경 타일 사용 x
장애물에 사용
*/
public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab; //자동할당 구문 작성 : 타입이 변경될 때 할당오브젝트 교체
    private Queue<T> pool = new Queue<T>();
    public Transform Root { get; private set; }

    public ObjectPool(T prefab, int initCount, Transform parent = null)
    {
        this.prefab = prefab;
        Root = new GameObject($"{prefab}_pool").transform;

        if (parent != null)
        {
            Root.SetParent(parent, false);
        }

        for (int i = 0; i < initCount; i++)
        {
            
        }
    }

    public T Dequeue()
    {
        if (pool.Count == 0) return null;

        var inst = pool.Dequeue();
        inst.gameObject.SetActive(true);
        return inst;
    }

    public void Enqueue (T chunkTile)
    {
        if (chunkTile == null) return;

        chunkTile.gameObject.SetActive(false);
        pool.Enqueue(chunkTile);
    }
}
