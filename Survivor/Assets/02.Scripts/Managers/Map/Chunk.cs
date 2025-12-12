using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Chunk : MonoBehaviour
{
    #region field
    protected enum Type
    {
        type01 = 1,
        type02,
        type03
    }

    [Header("기본 설정")]
    public Transform player;
    public int chunkSize = 1;
    public int loadRange = 15;
    public int unloadRange = 15;

    [Header("청크 프리팹 풀")]
    [SerializeField] protected Transform chunkParent;
    [SerializeField] protected ChunkSlot[] chunkPrefabs;

    private Vector2Int currentCenter; //현재 중심 좌표
    private readonly Dictionary<Vector2Int, ChunkSlot> activeChunks = new();
    #endregion

    protected void Awake()
    {
        currentCenter = GetPlayerChunk();
        UpdateChunks();
    }

    protected void Update()
    {
        Vector2Int newCenter = GetPlayerChunk();
        if (newCenter != currentCenter)
        {
            currentCenter = newCenter;
            UpdateChunks();
        }
    }

    #region method
    protected Vector2Int GetPlayerChunk()
    {
        int x = Mathf.FloorToInt(player.position.x / chunkSize);
        int y = Mathf.FloorToInt(player.position.y / chunkSize);
        return new Vector2Int(x, y);
    }

    protected void UpdateChunks()
    {
        HashSet<Vector2Int> needed = new HashSet<Vector2Int>();

        //범위내 청크 유지
        for (int x = -loadRange; x <= loadRange; x++)
        {
            for (int y = -loadRange; y <= loadRange; y++)
            {
                //현재 위치한 좌표 기준으로
                needed.Add(currentCenter + new Vector2Int(x, y));
            }
        }

        //범위내 청크 제거
        List<Vector2Int> toRemove = new List<Vector2Int>();
        foreach (var kv in activeChunks)
        {
            Vector2Int key = kv.Key;
            if (Mathf.Abs(key.x - currentCenter.x) > unloadRange ||
                Mathf.Abs(key.y - currentCenter.y) > unloadRange)
            {
                Destroy(kv.Value);
                toRemove.Add(key);
            }
        }

        //제거
        foreach (var key in toRemove)
        {
            activeChunks.Remove(key);
        }

        //프리팹 청크 로드
        foreach (var key in needed)
        {
            if (!activeChunks.ContainsKey(key))
            {
                Vector3 pos = new Vector3(key.x * chunkSize, key.y * chunkSize, 0.0f);
                ChunkSlot prefab = GetChunkPrefab(key);
                ChunkSlot chunk = Instantiate(prefab, pos, Quaternion.identity, transform);
                activeChunks[key] = chunk;
            }
        }
    }

    //청크 프리팹 선택
    protected ChunkSlot GetChunkPrefab(Vector2Int key)
    {
        if (chunkPrefabs == null || chunkPrefabs.Length == 0)
        {
            Debug.LogError("[ChunkManager] 청크 프리팹이 설정되지 않았습니다.");
            return null;
        }

        int rdN = Random.Range(0, 9);

        if (rdN < 8)
        {
            return chunkPrefabs[0];
        }

        return chunkPrefabs[Random.Range(1, chunkPrefabs.Length)];
    }
    #endregion
}
