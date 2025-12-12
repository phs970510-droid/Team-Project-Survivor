using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEditor;
using UnityEngine;

//Data so 연결 전 임시로 작성
[System.Serializable]
public class WeaponItem
{
    public string name;
    public float price;
    public GameObject buttonObj;
    public TextMeshProUGUI priceText;
}
public class WeaponUnlock: MonoBehaviour
{
    public GameObject emptyGold;

    [SerializeField] private TextMeshProUGUI currentGoldTetxt;
    [SerializeField] private float currentGold = 2001;

    public List<WeaponItem> itmes = new List<WeaponItem>();


    private float timer = 0f; //추후에 삭제
    void Update()
    {
        //초당 골드 1씩 추가
        // 추후에 삭제
        timer += Time.deltaTime;

        if (timer >= 1.0f)
        {
            currentGold += 1;
            timer = 0.0f;
        }

        currentGoldTetxt.text = currentGold.ToString();

        // WeaponItem에서 price찾기
        foreach (var item in itmes)
        {
            item.priceText.text = item.price.ToString();
        }
    }


    //클릭시 Unlock 패널 비활성화 및 골드 감소
    public void UnlockWeapon(int index)
    {
        //WeaponItem애서 찾기
        WeaponItem item = itmes[index];
        //pirce가 updateGold 보다 작다면
        if(item.price <= currentGold)
        {
            // 골드 차감
            currentGold -= item.price;
            // panel비활성화
            item.buttonObj.SetActive(false);
        }
        else 
        {
            // undateGold가 적으면
            // 추후 구매 불가능하다는 UI추가
            emptyGold.SetActive(true);

        }
        currentGoldTetxt.text = "" + (float)currentGold;

    }

}

