using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class UnlockStage : MonoBehaviour
{

    private int stageIndex;
    public bool isUnlocked = false;
    public Sprite lockSprite;
    public Sprite unLockSprite;

    private SpriteRenderer sr;
    private LockPanelUI lockPanelUI;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        lockPanelUI = FindObjectOfType<LockPanelUI>();

        ApplyState();
    }
    private void Start()
    {
        //bool unlocked = DataManager.Instance.stageUnlock(stageIndex);
    }

    public void Lock()
    {
        isUnlocked = false;
        ApplyState();
    }

    public void Unlock()
    {
        isUnlocked = true;
        ApplyState();
    }

    private void ApplyState()
    {
        if (isUnlocked)
            sr.sprite = unLockSprite;
        else
            sr.sprite = lockSprite;
    }
}
