using UnityEngine;

public class NormalBullet : MonoBehaviour, IPoolable
{
    private float bulletDamage;
    private float bulletSpeed;

    private float lifeTime = 3f;
    private float timer;

    private BulletPool pool;
    private Vector2 direction;

    public void BulletStat(float bulletDamage, float bulletSpeed, Vector2 direction, float lifeTime, BulletPool pool)
    {
        this.bulletDamage = bulletDamage;
        this.bulletSpeed = bulletSpeed;
        this.direction = direction;
        this.lifeTime = lifeTime;
        timer = 0f;
        this.pool = pool;
    }

    private void Update()
    {
        //총알 이동방향/속도
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ReturnPool();
        }
    }

    public void ReturnPool()
    {
        if(pool != null)
        {
            pool.ReturnBullet(this.gameObject);
        }
    }
    public void Spawn() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CommonHP hp = other.GetComponent<CommonHP>();

        if (hp == null) return;
        if (hp != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                hp.Damage(bulletDamage);
                ReturnPool();
                Debug.Log($"{name}이 {other.name}에게 데미지 줌({bulletDamage})");
            }
        }
    }
}
