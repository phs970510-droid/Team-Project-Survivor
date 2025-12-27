using UnityEngine;

public class TestStageMovent : MonoBehaviour
{
    public StageSceneLode stageSceneLode;
    public ChunkManager chunkManager;
    private LockPanelUI lockPanelUI;

    private void Awake()
    {
        chunkManager = FindObjectOfType<ChunkManager>();
        lockPanelUI = FindObjectOfType<LockPanelUI>(true);
        lockPanelUI.Hide();
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
        UnlockStage unlockStage = other.GetComponent<UnlockStage>();
        if (unlockStage != null && !unlockStage.isUnlocked)
        {
            if (lockPanelUI != null)
            {
                lockPanelUI.Show();
            }
            return;
        }

        if (lockPanelUI != null)
        {
            lockPanelUI.Hide();
        }
        if (other.CompareTag("Stage1"))
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
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<UnlockStage>() != null)
        {
            if (lockPanelUI != null)
                lockPanelUI.Hide();
        }
    }
}
