using UnityEngine;

public class FireZone : MonoBehaviour
{
    private float fireDamage;
    private float fireRadius;

    public void FireZoneStat(float damage, float radius)
    {
        this.fireDamage = damage;
        this.fireRadius = radius;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp != null)
        {
            hp.Damage(fireDamage);
        }
        Debug.Log($"{name}이 {other.name}에게 데미지 줌({fireDamage})");
    }

}
