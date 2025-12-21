using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public GameObject loadPanel;

    public Button continueButton;
    public Button[] loadSlotButton = new Button[4];

    private void Start()
    {
        if(DataManager.Instance != null)
        {
            UpdateContinueButton();
        }
        else
        {
            Debug.LogWarning("[MainMenuUI] DataManager 인스턴스가 아직 없습니다. Continue 버튼 비활성화.");
        }
    }
    public void ShowLoad()
    {
        loadPanel.SetActive(true);
        UpdateLoadSlotButtons();
    }
    public void CloseMenuPanel()
    {
        loadPanel.SetActive(false);
    }
    public void OnStartNew()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("TutorialMap");
    }
    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnContinue()
    {
        int lastSlot = PlayerPrefs.GetInt("LastSaveSlot", 1);
        if (DataManager.Instance.HasSaveSlot(lastSlot))
        {
            DataManager.Instance.LoadAllData(lastSlot);
            DataManager.Instance.SetCurrentSlot(lastSlot);
            SceneManager.LoadScene("BaseScene");
        }
        else
        {
            Debug.Log("저장된 세이브가 없습니다!");
        }
    }

    public void OnLoadSlot(int slotIndex)
    {
        if (DataManager.Instance.HasSaveSlot(slotIndex))
        {
            DataManager.Instance.LoadAllData(slotIndex);
            DataManager.Instance.SetCurrentSlot(slotIndex);
            PlayerPrefs.SetInt("LastSaveSlot", slotIndex);
            SceneManager.LoadScene("Safehouse");
        }
        else
        {
            Debug.Log($"슬롯 {slotIndex}에 저장된 데이터가 없습니다!");
        }
    }

    private void UpdateContinueButton()
    {
        bool anySave = false;
        for (int i = 1; i <= 4; i++)
        {
            if (DataManager.Instance.HasSaveSlot(i))
            {
                anySave = true;
                break;
            }
        }
        continueButton.interactable = anySave;
    }

    private void UpdateLoadSlotButtons()
    {
        for (int i = 1; i <= 4; i++)
        {
            bool hasSave = DataManager.Instance.HasSaveSlot(i);
            loadSlotButton[i - 1].interactable = hasSave;
        }
    }
}
