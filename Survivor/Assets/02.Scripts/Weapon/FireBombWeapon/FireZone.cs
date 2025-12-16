using UnityEngine;

public class FireZone : MonoBehaviour
{
    private float fireDamage;
    private float fireRadius;
    private int ignoreLayer;

    public void FireZoneStat(float damage, float radius, int ignoreLayer)
    {
        this.fireDamage = damage;
        this.ignoreLayer = ignoreLayer;
        this.fireRadius = radius;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //맞은 오브젝트가 무시레이어면 리턴
        if (other.gameObject.layer == ignoreLayer) return;

        CommonHP hp = other.GetComponent<CommonHP>();
        if (hp == null) return;

        if(hp != null)
        {
            hp.Damage(fireDamage);
            Debug.Log($"{name}이 {other.name}에게 데미지 줌({fireDamage})");
        }
    }
}
