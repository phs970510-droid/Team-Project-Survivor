using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private BaseData baseData;
    private float damage;

    private void Awake()
    {
        damage = baseData.damage;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CommonHP playerHP = other.GetComponent<CommonHP>();
            if(playerHP != null)
            {
                playerHP.Damage(damage);
            }
        }
    }
}
