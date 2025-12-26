using UnityEngine;
using UnityEngine.SceneManagement;

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
        }
        else if (other.CompareTag("Stage2"))
        {
            chunkManager.SelectMap(2, 2);
        }
        else if (other.CompareTag("Stage3"))
        {
            chunkManager.SelectMap(2, 3);
        }
        else if (other.CompareTag("Infinity"))
        {
            chunkManager.SelectMap(3, 1);
        }
        else if (other.CompareTag("Infinity2"))
        {
            chunkManager.SelectMap(3, 2);
        }
        else if (other.CompareTag("Infinity3"))
        {
            chunkManager.SelectMap(3, 3);
        }


    }
}