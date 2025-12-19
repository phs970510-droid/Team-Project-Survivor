using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

/*
생성 후 비활성화 : O

Awake 섞어서 활성화 : 필요 :: 씬 전환 후 바로 Update실행 x

섞어서 업데이트 : X
    ㄴ 섞어줄 부모 오브젝트 대상이 잘못 됨
*/

public abstract class Chunk : MonoBehaviour
{
    #region field
    [Header("기본 설정")]
    public Transform player;

    public int chunkSize = 5;
    public int loadRange = 3;
    public int unloadRange = 3;

    //청크 생성 인덱스 변수
    int i = 0;

    //인덱스 체크 변수
    int j = 0;
    private GameObject checkIndex;

    //활성화 리스트 체크
    [SerializeField] private List<GameObject> activeTure = new List<GameObject>();

    [Header("생성된 청크 활용")]
    [SerializeField] protected GameObject createdParent;
    [SerializeField] protected Transform[] createdPrefabs;

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
        CreateChunks();

        //변수 값 초기화 : 타입 변경 시 마다 재사용 되어야 함
        if (i > 0)
        {
            i = 0;
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
                activeTure.Add(chunk); //활성화 체크
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
                activeTure.Remove(kv.Value);
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
                activeTure.Add(chunk); //활성화 체크
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

        int numb = Random.Range(0, 10);

        if (numb < 9)
        {
            i++;
            return chunkPrefabs[0].gameObject;
        }

        i++;
        return chunkPrefabs[Random.Range(1, chunkPrefabs.Length)].gameObject;
    }

    //섞어줄 매서드
    protected GameObject SelectPrefab()
    {
        if (createdPrefabs == null || createdPrefabs.Length == 0)
        {
            Debug.LogError("[ChunkManager : SelectPrefab<T>()] 청크 프리팹이 설정되지 않았습니다.");
            return null;
        }

        //활성화 되어있는 오브젝트는 제외하고 넘어가는 것으로 수정



        if (j < createdPrefabs.Length)
        {
            checkIndex = createdPrefabs[Random.Range(0, createdPrefabs.Length)].gameObject;

            var isActive = activeTure.Find(x => x.gameObject == checkIndex);

            if (isActive != null)
            {
                while(isActive != null) //무한루프
                {
                    checkIndex = createdPrefabs[Random.Range(0, createdPrefabs.Length)].gameObject;
                    isActive = activeTure.Find(x => x.gameObject == checkIndex);
                    Debug.Log("중복");
                }
            }
            else if (isActive == null)
            {
                Debug.Log("해당x");
            }
        }

        return checkIndex;
    }
    #endregion
}