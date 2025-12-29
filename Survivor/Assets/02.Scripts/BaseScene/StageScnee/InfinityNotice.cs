using TMPro;
using UnityEngine;

public class InfinityNotice : MonoBehaviour
{
    float time;
    float fadeTime = 1.5f;

    public GameObject StageText;
    private TextMeshProUGUI text;
    private ChunkManager chunkManager;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        chunkManager = ChunkManager.Instance;
    }
    private void OnEnable()
    {
        time = 0f;
        text.color = new Color(1, 1, 1, 1);

        text.text = GetStageText(ChunkManager.Instance.typeNumb);
    }
    private void Update()
    {
        OpenStageText();
    }

    public void OpenStageText()
    {
        time += Time.deltaTime;

        float alpha = 1f - (time / fadeTime);
        text.color = new Color(1, 1, 1, alpha);

        if (time >= fadeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private string GetStageText(int chunkNum)
    {
        switch (chunkNum)
        {
            case 1: return "Infinity 1";
            case 2: return "Infinity 2";
            case 3: return "Infinity 3";
            default: return $"Infinity {chunkNum}";
        }

    }
}
