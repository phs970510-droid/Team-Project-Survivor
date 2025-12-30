using TMPro;
using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public void SetLevel(float level)
    {
        text.text = $"Lv{level}";
    }
}
