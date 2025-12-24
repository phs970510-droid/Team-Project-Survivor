using System.Collections.Generic;
using UnityEngine;

public abstract class Chunk : MonoBehaviour
{
    #region field
    [Header("기본 설정")]
    public Transform player;

    public int chunkSize = 5;
    public int loadRange = 3;
    public int unloadRange = 3;

    [Header("장식물 생성, 활성화 되어있는 청크 열외")]
    int i = 0; //청크 생성 인덱스 변수
    int j = 0; //장식물 순차적 생성을 위한 변수
    private GameObject checkIndex; //인덱스 체크 변수
    [SerializeField] private List<GameObject> activeTrue = new List<GameObject>(); //활성화 리스트 체크

    [Header("생성된 청크 활용")]
    [SerializeField] protected GameObject createdParent;
    [SerializeField] protected Transform[] createdPrefabs;

    [Header("청크 프리팹 풀")]
    //gameObject에 붙어있는 하위 컴포넌트 이므로 타입으로 사용이 가능하다
    [SerializeField] protected GameObject chunkParent;
    [SerializeField] protected Transform[] chunkPrefabs;

    private Vector2Int currentCenter; //현재 중심 좌표
    private Dictionary<Vector2Int, GameObject> activeChunks = new();
    #endregion

    protected virtual void Awake()
    {
        currentCenter = GetPlayerChunk();
        CreateChunks();

        //변수 값 초기화 : 타입 변경 시 마다 재사용 되어야 함
        if (i > 0 || j > 0)
        {
            i = 0;
            j = 0;
        }
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
    //플레이어 좌표 갱신 : Awake, Update
    protected Vector2Int GetPlayerChunk()
    {
        int x = Mathf.FloorToInt(player.position.x / chunkSize);
        int y = Mathf.FloorToInt(player.position.y / chunkSize);
        return new Vector2Int(x, y);
    }

    //만들고 비활성화 : Awake
    protected void CreateChunks()
    {
        HashSet<Vector2Int> needed = new HashSet<Vector2Int>();

        for (int x = -loadRange - 1; x <= loadRange + 1; x++)
        {
            for (int y = -loadRange - 1; y <= loadRange + 1; y++)
            {
                needed.Add(currentCenter + new Vector2Int(x, y));
            }
        }

        foreach (var key in needed)
        {
            if (i < 81)
            {
                Vector3 pos = new Vector3(key.x * chunkSize, key.y * chunkSize, 0.0f);
                GameObject prefab = GetChunkPrefab();
                GameObject chunk = Instantiate(
                    prefab,
                    pos,
                    Quaternion.identity,
                    createdParent.transform);
                activeChunks[key] = chunk;
                activeTrue.Add(chunk); //활성화 체크
            }
        }
    }


    //플레이어 좌표에 맞춰서 청크 갱신 : Update
    protected void UpdateChunks()
    {
        HashSet<Vector2Int> needed = new HashSet<Vector2Int>();

        for (int x = -loadRange; x <= loadRange; x++)
        {
            for (int y = -loadRange; y <= loadRange; y++)
            {
                needed.Add(currentCenter + new Vector2Int(x, y));
            }
        }

        List<Vector2Int> toFalse = new List<Vector2Int>();
        foreach (var kv in activeChunks)
        {
            Vector2Int key = kv.Key;
            if (Mathf.Abs(key.x - currentCenter.x) > unloadRange ||
                Mathf.Abs(key.y - currentCenter.y) > unloadRange)
            {
                kv.Value.SetActive(false); //OFF
                toFalse.Add(key);
                activeTrue.Remove(kv.Value);
            }
        }

        foreach (var key in toFalse)
        {
            activeChunks.Remove(key);
        }

        foreach (var key in needed)
        {
            if (!activeChunks.ContainsKey(key))
            {
                Vector3 pos = new Vector3(key.x * chunkSize, key.y * chunkSize, 0.0f);
                GameObject prefab = SelectPrefab();

                #region 좌표 값 지정 후 활성화
                prefab.transform.position = pos;
                prefab.SetActive(true);
                #endregion

                GameObject chunk = prefab;
                activeChunks[key] = chunk;
                activeTrue.Add(chunk); //활성화 체크
            }
        }
    }

    //청크 생성 비율 지정
    protected GameObject GetChunkPrefab()
    {
        if (chunkPrefabs == null || chunkPrefabs.Length == 0)
        {
            Debug.LogError("[ChunkManager : GetChunkPrefab()] 청크 프리팹이 설정되지 않았습니다.");
            return null;
        }

        //장식물 중복생성 방지 및 배열에 포함된 장식물 모두 생성
        int numb = Random.Range(0, 10);

        if (j < chunkPrefabs.Length - 1 && numb >= 8)
        {
            i++;
            j++;

            return chunkPrefabs[j].gameObject;
        }

        i++;
        return chunkPrefabs[0].gameObject;
    }

    //섞어줄 매서드
    protected GameObject SelectPrefab()
    {
        if (createdPrefabs == null || createdPrefabs.Length == 0) return null;

        //활성화 되어있는 오브젝트는 제외하고 넘어가는 것으로 수정
        checkIndex = createdPrefabs[Random.Range(0, createdPrefabs.Length)].gameObject;

        GameObject isActive = activeTrue.Find(x => x.gameObject == checkIndex);

        if (isActive != null)
        {
            while(isActive != null)
            {
                checkIndex = createdPrefabs[Random.Range(0, createdPrefabs.Length)].gameObject;
                isActive = activeTrue.Find(x => x.gameObject == checkIndex);
            }
        }

        return checkIndex;
    }
    #endregion
}