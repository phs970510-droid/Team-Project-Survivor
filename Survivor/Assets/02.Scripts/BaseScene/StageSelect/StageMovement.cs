using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour
{
    public StageSceneLode stageSceneLode;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Main"))
        {
            stageSceneLode.MainSceneLoder();
        }
        if (other.CompareTag("Tutorial"))
        {
            stageSceneLode.TutorialSceneLoader();
        }


    }
}
