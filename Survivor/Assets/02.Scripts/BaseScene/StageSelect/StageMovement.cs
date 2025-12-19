using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour
{
    public StageSceneLode stageSceneLode;
    public ChunkManager chunkManager;

    private void Awake()
    {
        chunkManager = FindObjectOfType<ChunkManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Main"))
        {
            stageSceneLode.MainSceneLoder();
        }
        else if (other.CompareTag("Tutorial"))
        {
            stageSceneLode.TutorialSceneLoader();
        }
        else if (other.CompareTag("Stage1"))
        {
            chunkManager.SelectMap(2, 1);
            stageSceneLode.Stage1SceneLodar();
        }
        else if (other.CompareTag("Stage2"))
        {
            chunkManager.SelectMap(2, 2);
            stageSceneLode.Stage2SceneLodar();
        }
        else if (other.CompareTag("Stage3"))
        {
            chunkManager.SelectMap(2, 3);
            stageSceneLode.Stage3SceneLodar();
        }


    }
}
