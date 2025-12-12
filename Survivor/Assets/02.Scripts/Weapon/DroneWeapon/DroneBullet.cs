using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    private float droneDamage;
    private float lifeTime = 0.5f;
    private float timer;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 dir, float speed, float damage)
    {
        rb.velocity = dir * speed;
        this.droneDamage = damage;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if( timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp != null && other.CompareTag("Enemy"))
        {
            hp.Damage(droneDamage);
            Destroy(gameObject);
            Debug.Log($"{name}이 {other.name}에게 데미지 줌({droneDamage})");
        }
    }
}
