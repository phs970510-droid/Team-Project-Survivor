using UnityEngine;

public class FireZone : MonoBehaviour
{
    private float fireDamage;
    private float fireRadius;

    private float lifeTime;
    private float timer;
    private BulletPool zonePool;

    [SerializeField] private LayerMask enemyLayer;

    public void FireZoneStat(float damage, float radius, float lifeTime, BulletPool zonePool)
    {
        this.fireDamage = damage;
        this.fireRadius = radius;
        this.lifeTime = lifeTime;
        this.zonePool = zonePool;
        this.timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > lifeTime)
        {
            ReturnPool();
        }
    }

    public void ReturnPool()
    {
        if (zonePool != null)
        {
            zonePool.ReturnBullet(this.gameObject);
        }
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
}
