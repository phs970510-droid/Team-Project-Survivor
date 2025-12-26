using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StageSelcetText : MonoBehaviour
{

    public UnlockStage stage1;
    public UnlockStage stage2;
    public UnlockStage stage3;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            stage1.Unlock();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            stage2.Unlock();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            stage3.Unlock();
        }
    }


}
