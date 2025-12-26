using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Image iconImage;
    public Image[] starImages;
    public TextMeshProUGUI descriptionText;

    public GameObject selectPanel;
    private WeaponData weaponData;
    public WeaponStat currentWeaponStat;


    private void Awake()
    {
        currentWeaponStat = FindObjectOfType<WeaponStat>();
    }
    public void SetData(WeaponData data)
    {
        weaponData = data;

        nameText.text = data.weaponName;
        iconImage.sprite = data.Sprite;
        descriptionText.text = data.description;

        UpdateStarUI();
    }

    public void OnClick()
    {
        weaponData.damage *= 1.1f;
        weaponData.bulletCount += 1;
        weaponData.fireCoolTime *= 0.9f;
        weaponData.starLevel++;

        currentWeaponStat.weaponData = weaponData;
        currentWeaponStat.damage = weaponData.damage;
        currentWeaponStat.bulletCount = weaponData.bulletCount;
        currentWeaponStat.fireCoolTime = weaponData.fireCoolTime;

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
