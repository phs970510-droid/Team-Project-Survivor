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

public class ChunkManager : MonoBehaviour
{
    #region field
    public static ChunkManager Instance { get; private set; }

    //선택된 값 저장할 변수
    private MapType selectNum;
    public int typeNumb;

    private enum MapType
    {
        tutorial = 1,
        stage,
        infinity
    }
    #endregion

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        InputType();
    }

    #region method
    //맵 선택 구문에 추가
    private void SelectMap(int index, int typeNumb)
    {
        selectNum = (MapType)index;
        this.typeNumb = typeNumb; //stage맵에서만 사용 : 청크타입 선택할 변수

        switch (selectNum)
        {
            case MapType.tutorial:
                {
                    SceneManager.LoadScene("TutorialMap");
                }
                break;
            case MapType.stage:
                {
                    SceneManager.LoadScene("StageMap");
                }
                break;
            case MapType.infinity:
                {
                    SceneManager.LoadScene("");
                }
                break;
        }
    }

    //테스트 메서드
    private void InputType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectMap(2, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectMap(2, 2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectMap(2, 3);
        }
    }
    #endregion
}
