using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDirection : MonoBehaviour
{
    float moveRange = 20.0f;
    float speed = 20.0f;
    float currentPosition;
    float direction = 4.0f;
    private RectTransform rectTransform;

    float startX;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        startX = rectTransform.anchoredPosition.y;
        currentPosition = 0f;
    }
    private void Update()
    {
        currentPosition += Time.deltaTime * speed * direction;
        if (currentPosition <= -moveRange)
        {
            currentPosition = -moveRange;
            direction *= -1;

        }
        else if (currentPosition >= moveRange)
        {
            currentPosition = moveRange;
            direction *= -1;

        }
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startX + currentPosition);
    }
}
