using UnityEngine;

public class FireZone : MonoBehaviour
{
    private float fireDamage;
    private float fireRadius;

    [SerializeField] private LayerMask enemyLayer;

    public void FireZoneStat(float damage, float radius)
    {
        this.fireDamage = damage;
        this.fireRadius = radius;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CommonHP hp = other.GetComponentInParent<CommonHP>();
        if (hp == null) return;

        if(hp != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                hp.Damage(fireDamage);
                Debug.Log($"{name}이 {other.name}에게 데미지 줌({fireDamage})");
            }
        }
    }

    public void SpawnBullet() { }
}
