using UnityEngine;

public class DroneBullet : MonoBehaviour, IPoolable
{
    private float droneDamage;
    private float lifeTime = 0.5f;
    private float timer;

    private BulletPool pool;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 dir, float speed, float damage, BulletPool pool)
    {
        rb.velocity = dir * speed;
        this.droneDamage = damage;
        timer = 0f;
        this.pool = pool;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if( timer > lifeTime)
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
        CommonHP hp = other.GetComponent<CommonHP>();

        if (hp == null) return;
        if (hp != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                hp.Damage(droneDamage);
                ReturnPool();
                Debug.Log($"{name}이 {other.name}에게 데미지 줌({droneDamage})");
            }
        }
    }
}
