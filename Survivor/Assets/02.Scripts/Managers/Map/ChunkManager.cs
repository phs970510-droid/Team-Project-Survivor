using UnityEngine;
using UnityEngine.SceneManagement;

/*
주의사항 : 싱글톤에 컴포넌트 연동시키지 말 것
해당 싱글톤에 연동시킨 컴포넌트 0개
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
                    SceneManager.LoadScene("InfinityMap");
                }
                break;
        }
    }

    //테스트 메서드 : 연동 시 수정필
    private void InputType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //인자 1: 튜툐, 스테이지, 인피니티
            //인자 2: 스프라이트 타입 1 - 3
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
