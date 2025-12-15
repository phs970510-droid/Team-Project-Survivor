using UnityEngine;
using UnityEngine.SceneManagement;

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

[RequireComponent(typeof(StageMap))]
[RequireComponent(typeof(InfinityMap))]
public class ChunkManager : MonoBehaviour
{
    #region field
    public static ChunkManager Instance { get; private set; }

    private StageMap stageMap;
    private InfinityMap infinityMap;

    #endregion

    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        stageMap = GetComponent<StageMap>();
        infinityMap = GetComponent<InfinityMap>();
    }

    #region method
    private void OnEnable()
    {
        SceneManager.sceneLoaded += LoadScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= LoadScene;
    }

    private void LoadScene(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        stageMap.player = GameObject.Find("Player").transform;
        infinityMap.player = GameObject.Find("Player").transform;
    }

    #endregion
}
