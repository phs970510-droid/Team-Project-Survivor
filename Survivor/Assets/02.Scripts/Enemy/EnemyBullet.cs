using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10.0f;
    public float bulletDamage;
    public Vector2 direction;

    void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);

        Destroy(gameObject, 5f);
    }

    public void BulletDamage(float damage)
    {
        bulletDamage = damage;
    }

    public void BulletDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CommonHP hp = other.GetComponent<CommonHP>();
            if(hp != null)
            {
                hp.Damage(bulletDamage);
            }
            Destroy(gameObject);
        }
    }
}
