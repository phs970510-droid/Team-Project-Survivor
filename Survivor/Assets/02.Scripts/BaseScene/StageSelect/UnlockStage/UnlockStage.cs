using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class UnlockStage : MonoBehaviour
{

    public bool isUnlocked = false;
    public GameObject lockPanel;
    public Sprite lockSprite;
    public Sprite unLockSprite;

    private SpriteRenderer sr;
    private Collider2D col;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Lock()
    {
        isUnlocked = false;
        sr.sprite = lockSprite;
        col.enabled = false;
    }
    public void Unlock()
    {
        isUnlocked = true;
        sr.sprite = unLockSprite;
        col.enabled = true;
    }

}
