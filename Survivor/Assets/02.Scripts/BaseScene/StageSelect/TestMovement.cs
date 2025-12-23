using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public TestStageSceneLodeipt TestSceneLoad;
    public ChunkManager chunkManager;

    private void Awake()
    {
        chunkManager = FindObjectOfType<ChunkManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Main"))
        {
            //TestSceneLoad.MainSceneLoder();
        }
        //else if (other.CompareTag("Tutorial"))
        //{
        //    TestSceneLoad.TutorialSceneLoader();
        //}
        else if (other.CompareTag("Stage1")) //여기 부분 수정해서 테스트 전투 씬 가져오면 됩니다.
        {
            chunkManager.SelectMap(2, 1);
        }
        else if (other.CompareTag("Stage2"))
        {
            chunkManager.SelectMap(2, 2);
        }
        else if (other.CompareTag("Stage3"))
        {
            chunkManager.SelectMap(2, 3);
        }


    }
}
