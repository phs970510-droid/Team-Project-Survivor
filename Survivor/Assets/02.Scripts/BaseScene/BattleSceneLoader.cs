using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //private void Awake()
    //{
    //    //PlayerPrefs를 이용해 디바이스에 저장되어 있는 현재 스테이지 인덱스 불러오기
    //    //저장하는 값은 0부터 시작하고, 스테이지는 1부터 시작하기 때문에 +1
    //    int index = PlayerPrefs.GetInt("StageIndex") + 1;
    //    //인덱스가 10보다 작으면 01, 02와 같이 0을 붙여주고, 10 이상이면 그대로 사용
    //    string currnetStage = index < 10 ? $"Stage0{index}" : $"Stage{index}";
    //    //저장되어 있는 인덱스에 따라 Stage01, Stage02, .. json파일에 데이터를 불러온다.
    //    //현재 json파일에 스테이지 x

    //    // json파일의 정보를 바탕으로 타일 형태의 맵 생성
    //    // 우리도인가?

    //    //json에서  플레이어 위치 설정
    //    //PlayerController.Setup(json.playerPosition, json.mapSize.y);

    //    //맵 크기 정보 를 바탕으로 카메라 시야 크기 설정
    //    //??뭘까
    //    //cameraController.Setup(json.mapSize.x, json.mapSize.y);

    //    //현재 스테이지의 정보를 UI에 출력
    //    //아직 stageUI를 만든게 없다.
    //    //stageUI.UpdateTextStage(currentStage);
        
    //}
    public void BattleSceneLoader()
    {
        SceneManager.LoadScene("TestBattleScene");
    }
}
