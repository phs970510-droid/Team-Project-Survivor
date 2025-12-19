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
    [SerializeField] private KeyCode toggleKey = KeyCode.H;

    [TextArea]
    public string message =
        "WASD / Mouse Drag : Move\n" +
        "H : On/Off to Show Help Again\n" +
        "Move to the [Stage Exit]";

    public GameObject joyStick;

    private CanvasGroup canvasGroup;
    private bool isVisible = true;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }
    private void Start()
    {
        if (hintText != null)
            hintText.text = message;
        ShowHint(true);     //처음엔 무조건 표시하기

    }
    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ShowHint(!isVisible);
            joyStick.SetActive(true);
            Time.timeScale = 1f;
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