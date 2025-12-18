using UnityEngine;

public class BossFireZone : MonoBehaviour
{
    private float fireDamage;
    private float fireRadius;

    private float lifeTime;
    private float timer;
    private BulletPool pool;

    public void BossFireZoneStat(float damage, float radius, float lifeTime, BulletPool pool)
    {
        this.fireDamage = damage;
        this.fireRadius = radius;
        this.lifeTime = lifeTime;
        this.pool = pool;
        this.timer = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            ReturnPool();
        }
    }

    public void ReturnPool()
    {
        if (pool != null)
        {
            pool.ReturnBullet(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp == null) return;

        if (hp != null && other.CompareTag("Player"))
        {
            hp.Damage(fireDamage);
            Debug.Log($"{name}이 {other.name}에게 데미지 줌({fireDamage})");
        }
    }
}
