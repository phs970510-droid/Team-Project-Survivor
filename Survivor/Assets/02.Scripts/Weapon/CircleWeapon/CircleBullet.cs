using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    private float circleDamage;
    private int ignoreLayer;

    public void BulletStat(float bulletDamage, int ignoreLayer)
    {
        this.circleDamage = bulletDamage;
        this.ignoreLayer = ignoreLayer;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //맞은 오브젝트가 무시레이어면 리턴
        if (other.gameObject.layer == ignoreLayer) return;

        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp == null) return;

        if (hp != null)
        {
            hp.Damage(circleDamage);
            Debug.Log($"{name}이 {other.name}에게 데미지 줌({circleDamage})");
        }
    }
}
