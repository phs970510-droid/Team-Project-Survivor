using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMissionBoard : MonoBehaviour
{
    public void TutorialSceneLoader()
    {
        SceneManager.LoadScene("TutorialMap");
    }
}
