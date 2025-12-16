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

        //플레이어, 보스가 쏜 화염은 자기가 안맞고 상대방한테만 데미지
        if (shooter == Shooter.Player)
        {
            if(other.CompareTag("Player")) return;
            else if (hp.CompareTag("Enemy") && hp != null)
            {
                hp.Damage(fireDamage);
                Debug.Log($"{name}이 {hp.name}에게 데미지 줌({fireDamage})");
            }
        }
        else if (shooter == Shooter.Boss)
        {
            if (other.CompareTag("Enemy")) return;
            else if (other.CompareTag("Player") && hp != null)
            {
                hp.Damage(fireDamage);
                Debug.Log($"{name}이 {other.name}에게 데미지 줌({fireDamage})");
            }
        }
    }
}
