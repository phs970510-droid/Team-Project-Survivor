using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public enum SelectItemType
{
    Weapon,
    Heal
}

public class SelectItemButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image iconImage;
    public Image[] starImages;

    public Sprite healSprite;

    private WeaponData weaponData;
    private SelectItemType itemType;

    public GameObject selectPanel;

    public void SetData(WeaponData data)
    {
        itemType = SelectItemType.Weapon;
        weaponData = data;

        nameText.text = data.weaponName;
        descriptionText.text = data.description;
        iconImage.sprite = data.Sprite;

        UpdateStarUI();
    }

    public void SetHeal()
    {
        itemType = SelectItemType.Heal;
        weaponData = null;

        nameText.text = "Heal";
        descriptionText.text = "full Hp";
        iconImage.sprite = healSprite;

        foreach (var star in starImages)
        { 
           star.gameObject.SetActive(false);
        }
    }


    public void OnClick()
    {
        if (itemType == SelectItemType.Weapon)
        {
            if (weaponData == null) return;
            if (weaponData.starLevel >= weaponData.maxStar) return;

            weaponData.damage *= 1.1f;
            weaponData.bulletCount += 1;
            weaponData.starLevel++;

            foreach (var ws in FindObjectsOfType<WeaponStat>())
            {
                if (ws.weaponData == weaponData)
                    ws.SyncFromData();
            }
            UpdateStarUI();

        }
        else if (itemType == SelectItemType.Heal) 
        {
            HealPlayer();
        }
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

    void HealPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        CommonHP hp = player.GetComponent<CommonHP>();
        if (hp == null) return;


        hp.HealFull();
    }
}
