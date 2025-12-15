using UnityEngine;
using UnityEngine.UI;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
 protected static T MyInstace = null; 
    public static T Instace
    {
        get
        {
            if (MyInstace != null) return MyInstace;
            MyInstace = FindObjectOfType(typeof(T)) as T;
            return MyInstace == null ? null : MyInstace;
        }
    }

}

