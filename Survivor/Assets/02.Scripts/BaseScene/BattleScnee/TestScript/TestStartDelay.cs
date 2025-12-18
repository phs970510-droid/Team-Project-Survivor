using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartDelay : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 0f;
    }

}
