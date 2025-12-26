using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    [SerializeField] private  Image[] stars;
    
    public void Refrsh(int starCount)
    {
        for (int i = 0; i < stars.Length; i++) 
        {
            stars[i].enabled = i < starCount;
        }
    }
}
