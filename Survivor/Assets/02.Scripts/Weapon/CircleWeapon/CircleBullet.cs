using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    private float circleDamage;

    public void BulletStat(float bulletDamage)
    {
        this.circleDamage = bulletDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp == null) return;
        if (hp != null)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                hp.Damage(circleDamage);
                Debug.Log($"{name}이 {other.name}에게 데미지 줌({circleDamage})");
            }
        }
    }
}
