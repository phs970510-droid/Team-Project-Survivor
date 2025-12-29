using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] private CommonHP commonHP;
    [SerializeField] private ItemPool coinPool;
    [SerializeField] private GameObject openedReward;
    [SerializeField] private GameObject shieldEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //코인
        if (other.CompareTag("Coin"))
        {
            GetCoin();
            coinPool.ReturnItem(other.gameObject);
            AudioManager.instance.PlayCoinSound();
        }
        //자석 아이템
        if(other.CompareTag("Magnet"))
        {
            GetMagnetItem();
            Destroy(other.gameObject);
            AudioManager.instance.PlayMagnetSound();
        }

        //방어막 아이템 추가
        if (other.CompareTag("Shield"))
        {
            commonHP.GetShieldItem();
            Destroy(other.gameObject);
            AudioManager.instance.PlayShieldSound();

            if (shieldEffect == null) return;
            if (shieldEffect != null)
            {
                GameObject shieldEffectObj = Instantiate(shieldEffect, transform.position, Quaternion.identity, transform);
                Destroy(shieldEffectObj, 5f);
            }
        }

        //보스 보상
        if (other.CompareTag("Reward"))
        {
            GetBossReward();
            Destroy(other.gameObject);
            if (openedReward == null) return;
            if (openedReward != null)
            {
                Instantiate(openedReward, transform.position, Quaternion.identity);
                Destroy(openedReward, 3f);
            }
        }
    }

    private void GetCoin()
    {
        DataManager.Instance.AddMoney(10);
    }

    private void GetMagnetItem()
    {
        GameObject[] exps = GameObject.FindGameObjectsWithTag("Exp");
        GameObject[] bigExps = GameObject.FindGameObjectsWithTag("BigExp");

        //모든 exp아이템에 MagnetOn실행
        foreach(GameObject go in exps)
        {
            EXP exp = go.GetComponent<EXP>();
            if(exp != null)
            {
                exp.MagnetOn();
            }
        }
        foreach (GameObject go in bigExps)
        {
            EXP exp = go.GetComponent<EXP>();
            if (exp != null)
            {
                exp.MagnetOn();
            }
        }
    }

    private void GetBossReward()
    {
        //인게임 재화 or 무기 해금 아이템 얻기
        DataManager.Instance.AddMoney(1000); //UI추가
    }
}
