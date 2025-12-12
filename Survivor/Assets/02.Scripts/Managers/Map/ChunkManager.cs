using System.Collections.Generic;
using UnityEngine;

/*
0. 스테이지맵, 무제한맵 인덱스로 조건문
0.1. 스테이지맵, 무제한맵 별도 스크립트제작
0.1.1. 중복되는 코드 abstract로 따로 빼서 작성

1.스테이지맵 : 규격
    1.1.맵타입 : 인덱스를 받아와서 해당 프리팹을 불러와서 배치
        ㄴ인덱스를 받아오면서 해당씬 불러오고 OnEnable에서 조건문 처리
        ㄴ해당 오브젝트 SetActive: ture||false
        ㄴchunkPrefabs에 해당타입의 children 자동으로 담기
    1.2.범위 규격 제한
        ㄴ+-250x, +-250y 규격으로 맵 한정
        ㄴ지나갈 수 없도록 장애물 배치 : 타입에 따라 장애물에 변경되어야함 : SetActive 활용

2.무제한맵 : 규격x
    2.1.맵타입 : 해당 시간을 조건식으로 작성
        ㄴchunkPrefabs를 비우고 변경되는 타입의 children 자동으로 담기
*/
public class ChunkManager : MonoBehaviour
{
    #region field
    [Header("기본 설정")]
    public Transform player;
    public int chunkSize = 1;
    public int loadRange = 15;
    public int unloadRange = 15;

    [Header("청크 프리팹 풀")]
    public GameObject[] chunkPrefabs;

    private Vector2Int currentCenter; //현재 중심 좌표
    private readonly Dictionary<Vector2Int, GameObject> activeChunks = new Dictionary<Vector2Int, GameObject>();
    #endregion

    void Awake()
    {
        currentCenter = GetPlayerChunk();
        UpdateChunks();
    }

    void Update()
    {
        Vector2Int newCenter = GetPlayerChunk();
        if (newCenter != currentCenter)
        {
            currentCenter = newCenter;
            UpdateChunks();
        }
    }

    #region method
    private Vector2Int GetPlayerChunk()
    {
        int x = Mathf.FloorToInt(player.position.x / chunkSize);
        int y = Mathf.FloorToInt(player.position.y / chunkSize);
        return new Vector2Int(x, y);
    }

    private void UpdateChunks()
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
    private GameObject GetChunkPrefab(Vector2Int key)
    {
        if (chunkPrefabs == null || chunkPrefabs.Length == 0)
        {
            Debug.LogError("[ChunkManager] 청크 프리팹이 설정되지 않았습니다.");
            return null;
        }

        //1 : 9 비율
        //1 - 장식물
        //9 - 기본배경
        int rdN = Random.Range(0, 9);

        if (rdN < 8)
        {
            return chunkPrefabs[0];
        }

        return chunkPrefabs[Random.Range(1, chunkPrefabs.Length)];
    }
    #endregion
}
