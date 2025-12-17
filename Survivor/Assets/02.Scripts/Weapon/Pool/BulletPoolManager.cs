using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
