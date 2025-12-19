using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    public Image[] stars;

    private int currentStar = 0;
    public void UpdateStar()
    {
        if (currentStar >= stars.Length) return;

        stars[currentStar].gameObject.SetActive(true);
        currentStar++;
    }
}
