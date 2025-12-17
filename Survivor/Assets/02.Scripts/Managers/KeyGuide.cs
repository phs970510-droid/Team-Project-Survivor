using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Search;

public class KeyGuide : MonoBehaviour
{
    [Header("안내 텍스트")]
    [SerializeField] private TMP_Text hintText;

    [Header("표시 설정")]
    [SerializeField] private float autoHideDelay = 5.0f;    //자동 숨김 시간
    [SerializeField] private KeyCode toggleKey = KeyCode.H;

    [TextArea]
    public string message=
        "WASD/콘솔: 이동\n" +
        "E: 상호작용\n" +
        "H: 도움말 다시 켜기\n" +
        "ESC: 설정창\n" +
        "마우스: 시선 방향\n" +
        "[Stage Exit] 구역으로 이동해 상점으로 넘어가세요!";

    private CanvasGroup canvasGroup;
    private bool isVisible = true;

    private void Awake()
    {
        canvasGroup= GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        { 
            canvasGroup= gameObject.AddComponent<CanvasGroup>();
        }
    }

    private IEnumerator Start()
    { 
        if(hintText !=null)
            hintText.text = message;

        ShowHint(true);     //처음엔 무조건 표시하기
        yield return new WaitForSeconds(autoHideDelay);
        ShowHint(false);    //일정 시간 지나면 자동 숨김
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ShowHint(!isVisible);
        }
    }

    private void ShowHint(bool show)
    { 
        isVisible = show;
        canvasGroup.alpha = show ? 1.0f : 0.0f;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }
}
