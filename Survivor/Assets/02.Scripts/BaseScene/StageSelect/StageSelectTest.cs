using UnityEngine;

public class StageSelectTest : MonoBehaviour
{
    public UnlockStage stage1;
    public UnlockStage stage2;
    public UnlockStage stage3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            stage1.Unlock();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            stage2.Unlock();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            stage3.Unlock();
        }
    }
}
