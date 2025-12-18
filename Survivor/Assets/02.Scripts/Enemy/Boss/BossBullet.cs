using UnityEngine;

public class BossBullet : MonoBehaviour, IPoolable
{
    private float bossBulletDamage;
    private float bossBulletSpeed;
    private float bossBulletLifeTime = 3f;
    private float timer;

    private BulletPool pool;
    private Vector2 direction;

    public void BossBulletStat(float bulletDamage, float bulletSpeed, Vector2 direction, BulletPool pool)
    {
        this.bossBulletDamage = bulletDamage;
        this.bossBulletSpeed = bulletSpeed;
        this.direction = direction;
        this. pool = pool;
        timer = 0f;
    }

    void Update()
    {
        transform.Translate(direction * bossBulletSpeed * Time.deltaTime, Space.World);

        timer += Time.deltaTime;
        if (timer > bossBulletLifeTime)
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
    public void Spawn() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp != null)
        {
            hp.Damage(bossBulletDamage);
        }
        ReturnPool();
    }
}
