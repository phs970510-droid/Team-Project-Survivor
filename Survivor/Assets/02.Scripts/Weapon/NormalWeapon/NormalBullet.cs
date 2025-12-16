using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    private float bulletDamage;
    private float bulletSpeed;
    private float lifeTime = 3.0f;

    private Vector2 direction;

    public void BulletStat(float bulletDamage, float bulletSpeed, Vector2 direction)
    {
        this.bulletDamage = bulletDamage;
        this.bulletSpeed = bulletSpeed;
        this.direction = direction;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        //총알 이동방향/속도
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CommonHP hp = other.GetComponent<CommonHP>();

        if (hp == null) return;
        if (hp != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                hp.Damage(bulletDamage);
                Destroy(gameObject);
                Debug.Log($"{name}이 {other.name}에게 데미지 줌({bulletDamage})");
            }
        }
    }
}
