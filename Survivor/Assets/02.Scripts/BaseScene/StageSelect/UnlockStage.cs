using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class UnlockStage : MonoBehaviour
{

    [SerializeField] private int stageIndex;
    [SerializeField] private int infinityIndex;
    public bool isUnlocked = false;
    public Sprite lockSprite;
    public Sprite unLockSprite;

    private SpriteRenderer sr;
    private LockPanelUI lockPanelUI;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        lockPanelUI = FindObjectOfType<LockPanelUI>();
    }
    private void Start()
    {
        if(infinityIndex >= 0)
        {
            isUnlocked = DataManager.Instance.infinityUnlocked[infinityIndex];
            ApplyState();
            return;
        }

        if (stageIndex == 0)
        {
            isUnlocked = true;
        }
        else
        {
            isUnlocked = DataManager.Instance.stageUnlocked[stageIndex - 1];
        }
        ApplyState();

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
