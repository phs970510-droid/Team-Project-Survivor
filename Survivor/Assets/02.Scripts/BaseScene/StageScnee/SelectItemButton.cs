using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image iconImage;
    public Image[] starImages;

    public GameObject selectPanel;
    private WeaponData weaponData;

    public void SetData(WeaponData data)
    {
        weaponData = data;

        nameText.text = data.weaponName;
        iconImage.sprite = data.Sprite;

        UpdateStarUI();
    }

    public void OnClick()
    {
        if (weaponData.starLevel >= weaponData.maxStar) return;

        weaponData.damage *= 1.1f;
        weaponData.bulletCount += 1;
        weaponData.starLevel++;
        
        foreach(var ws in FindObjectsOfType<WeaponStat>())
        {
            if(ws.weaponData == weaponData)
                ws.SyncFromData();
        }

        UpdateStarUI();

        Time.timeScale = 1f;
        selectPanel.SetActive(false);


    }

    void UpdateStarUI()
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].gameObject.SetActive(i < weaponData.starLevel);
        }
    }


}
