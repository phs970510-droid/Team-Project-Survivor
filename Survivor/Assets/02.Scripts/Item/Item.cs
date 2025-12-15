using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private PlayerLevel playerLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Exp"))
        {
            playerLevel.GetEXP(10);
            Destroy(other.gameObject);
        }

        //자석 아이템
        //else if(other.CompareTag("Magnet"))
        //{
        //    GetAllEXP();
        //    Destroy(other.gameObject);
        //}

        //방어막 아이템 추가
    }

    //자석 먹으면 모든 경험치 플레이어에게
    private void GetAllEXP()
    {
        GameObject[] expItems = GameObject.FindGameObjectsWithTag("Exp");
        foreach(var exp in expItems)
        {
            playerLevel.GetEXP(10);
            Destroy(exp);
        }
    }
}
