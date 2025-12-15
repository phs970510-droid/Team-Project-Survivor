using UnityEngine;

public class FireZone : MonoBehaviour
{
    private float fireDamage;
    private float fireRadius;
    private Shooter shooter;

    public void FireZoneStat(float damage, float radius, Shooter shooter)
    {
        this.fireDamage = damage;
        this.fireRadius = radius;
        this.shooter = shooter;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp == null) return;

        //플레이어, 보스가 쏜 화염은 자기가 안맞게
        if (shooter == Shooter.Player)
        {
            if(other.CompareTag("Player")) return;
            if (other.CompareTag("Enemy"))
            {
                if (hp != null)
                {
                    hp.Damage(fireDamage);
                }
                Debug.Log($"{name}이 {other.name}에게 데미지 줌({fireDamage})");
            }
        }
        if (shooter == Shooter.Boss && other.CompareTag("Enemy")) return;
    }
}
