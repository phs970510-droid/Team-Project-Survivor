using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    private float bulletDamage;

    public void BulletStat(float bulletDamage)
    {
        this.bulletDamage = bulletDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp != null)
        {
            hp.Damage(bulletDamage);
            Debug.Log($"{name}이 {other.name}에게 데미지 줌({bulletDamage})");
        }
    }
}
