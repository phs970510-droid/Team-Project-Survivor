using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] private CommonHP commonHP;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //경험치 먹었을 때
        if(other.CompareTag("Exp"))
        {
            playerLevel.GetEXP(10);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("BigExp"))
        {
            playerLevel.GetEXP(20);
            Destroy(other.gameObject);
        }
        
        //자석 아이템
        if(other.CompareTag("Magnet"))
        {
            GetMagnetItem();
            Destroy(other.gameObject);
        }

        //방어막 아이템 추가
        if (other.CompareTag("Shield"))
        {
            commonHP.GetShieldItem();
            Destroy(other.gameObject);
        }

        //보스 보상
        if (other.CompareTag("Reward"))
        {
            GetBossReward();
            Destroy(other.gameObject);
        }
    }

    private void GetMagnetItem()
    {
        GameObject[] exps = GameObject.FindGameObjectsWithTag("Exp");

        //모든 exp아이템에 MagnetOn실행
        foreach(GameObject go in exps)
        {
            EXP exp = go.GetComponent<EXP>();
            if(exp != null)
            {
                exp.MagnetOn();
            }
        }
    }

    private void GetBossReward()
    {
        //인게임 재화 or 무기 해금 아이템 얻기
    }
}
