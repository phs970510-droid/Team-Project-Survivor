using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FaidInOut : MonoBehaviour
{
    Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }
    private void Update()
    {
        float alpha = Mathf.PingPong(Time.time, 1f);
        img.color = new Color(1f,1f,1f,alpha);
    }
}
