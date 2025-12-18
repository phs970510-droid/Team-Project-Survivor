using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDelay : MonoBehaviour
{
    public GameObject joyStick;
    public GameObject oKPanel;
    private void Start()
    {
        StartCoroutine(Delay());
    }

    public void Restart()
    {   
        oKPanel.SetActive(false);
        joyStick.SetActive(true);
        Time.timeScale = 1.0f;
    }
    
    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 0f;
    }
}
