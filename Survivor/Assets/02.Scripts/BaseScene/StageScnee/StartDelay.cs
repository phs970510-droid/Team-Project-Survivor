using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDelay : MonoBehaviour
{
    public GameObject joyStick;
    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        joyStick.SetActive(false);
        Time.timeScale = 0f;
    }

}