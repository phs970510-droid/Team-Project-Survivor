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

        weaponData.damage++;
        weaponData.bulletCount++;
        weaponData.starLevel++;


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
