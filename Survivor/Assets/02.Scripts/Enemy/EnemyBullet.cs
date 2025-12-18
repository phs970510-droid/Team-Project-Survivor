using UnityEngine;

public class EnemyBullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float bulletSpeed = 10.0f;
    public float damage;
    public Vector2 direction;

    [SerializeField] BulletPool pool;
    private float lifeTime = 5f;
    private float timer;

    public void Init(float damage, Vector2 direction, float lifeTime, BulletPool pool)
    {
        this.damage = damage;
        this.direction = direction;
        this.pool = pool;
        this.lifeTime = lifeTime;

        timer = 0f;
    }

    void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);

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
    public void Spawn() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CommonHP hp = other.GetComponent<CommonHP>();
            if(hp != null)
            {
                hp.Damage(damage);
            }
            ReturnPool();
        }
    }
}
