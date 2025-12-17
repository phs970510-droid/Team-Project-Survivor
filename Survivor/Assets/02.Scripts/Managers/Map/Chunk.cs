using System.Collections.Generic;
using UnityEngine;

/*
청크 오브젝트 풀 사용
1)싱글톤 하위로 스프라이트 생성 : 씬 이동 후 재입장 또는 리로드 시 파괴되어 null 오류 발생 방지
2)씬에 자체적으로 부모오브젝트 생성 : 불필요한 데이터가 유지되어 같이 이동하지 않음
*/

public abstract class Chunk : MonoBehaviour
{
    #region field
    [Header("기본 설정")]
    public Transform player;

    /* 수정사항
    수정 전 : 청크 스프라이트 활성화 수 961개
    수정 후 : 청크 스프라이트 활성화 수 49개
    */
    public int chunkSize = 5;
    public int loadRange = 3;
    public int unloadRange = 3;

    [Header("청크 프리팹 풀")]
    //gameObject에 붙어있는 하위 컴포넌트 이므로 타입으로 사용이 가능하다
    [SerializeField] protected GameObject chunkParent;
    [SerializeField] protected Transform[] chunkPrefabs;

    private Vector2Int currentCenter; //현재 중심 좌표
    private readonly Dictionary<Vector2Int, GameObject> activeChunks = new();
    #endregion

    protected virtual void Awake()
    {
        currentCenter = GetPlayerChunk();
        UpdateChunks();
    }

    protected virtual void Update()
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
                GameObject prefab = GetChunkPrefab(key);
                GameObject chunk = Instantiate(prefab, pos, Quaternion.identity, transform);
                activeChunks[key] = chunk;
            }
        }
    }

    //청크 프리팹 선택
    protected GameObject GetChunkPrefab(Vector2Int key)
    {
        if (chunkPrefabs == null || chunkPrefabs.Length == 0)
        {
            Debug.LogError("[ChunkManager] 청크 프리팹이 설정되지 않았습니다.");
            return null;
        }

        int rdN = Random.Range(0, 15);

        if (rdN < 13)
        {
            return chunkPrefabs[0].gameObject;
        }

        return chunkPrefabs[Random.Range(1, chunkPrefabs.Length)].gameObject;
    }
    #endregion
}
