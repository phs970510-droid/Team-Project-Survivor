using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneLode : MonoBehaviour
{

    public void MainSceneLoder()
    {
        SceneManager.LoadScene("BaseScene");
    }
    public void TutorialSceneLoader()
    {
        SceneManager.LoadScene("TutorialMap");
    }
    public void Stage1SceneLodar()
    {
        //SceneManager.LoadScene("Stage1");
    }
    public void Stage2SceneLodar()
    {
        //SceneManager.LoadScene("Stage2");
    }
    public void Stage3SceneLodar()
    {
        //SceneManager.LoadScene("Stage3");
    }
}
