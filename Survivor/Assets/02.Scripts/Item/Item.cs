using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private PlayerLevel playerLevel;
    [SerializeField] private CommonHP commonHP;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Exp"))
        {
            playerLevel.GetEXP(10);
            Destroy(other.gameObject);
        }

        //자석 아이템
        else if(other.CompareTag("Magnet"))
        {
            GetMagnetItem();
            Destroy(other.gameObject);
            Debug.Log("자석 먹음");
        }

        //방어막 아이템 추가
        else if (other.CompareTag("Shield"))
        {
            commonHP.GetShieldItem();
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
}
