using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private float bossBulletDamage;
    private float bossBulletSpeed;
    private float bossBulletLifeTime = 3f;

    private Vector2 direction;

    public void BossBulletStat(float bulletDamage, float bulletSpeed, Vector2 direction)
    {
        this.bossBulletDamage = bulletDamage;
        this.bossBulletSpeed = bulletSpeed;
        this.direction = direction;

        Destroy(gameObject, bossBulletLifeTime);
    }

    void Update()
    {
        transform.Translate(direction * bossBulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp != null)
        {
            hp.Damage(bossBulletDamage);
        }
        Destroy(gameObject);
    }
}
