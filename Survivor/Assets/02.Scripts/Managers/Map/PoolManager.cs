using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region field
    public static PoolManager Instance { get; private set; }

    private Dictionary<string, object> chunks = new Dictionary<string, object>();
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region method
    public void CreatePool<T>(T[] chunk, int initCount, Transform parent = null) where T : MonoBehaviour
    {
        //장애물에 맞춰서 수정
    }

    public T GetFromPool<T>(T chunk) where T : MonoBehaviour
    {
        if (chunk == null) return null;

        if (!chunks.TryGetValue(chunk.name, out var box))
        {
            return null;
        }

        var pool = box as ObjectPool<T>;

        if (pool != null)
        {
            return pool.Dequeue();
        }
        else
        {
            return null;
        }
    }

    public void ReturnPool<T>(T instance) where T : MonoBehaviour
    {
        if (instance == null) return;

        if (!chunks.TryGetValue(instance.gameObject.name, out var box))
        {
            Destroy(instance.gameObject);
            return;
        }

        var pool = box as ObjectPool<T>;

        if (pool != null)
        {
            pool.Enqueue(instance);
        }
    }
    #endregion
}
