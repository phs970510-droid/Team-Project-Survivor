using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneLode : MonoBehaviour
{

    public void MainSceneLoder()
    {
        SceneManager.LoadScene("WY Scene");
    }
    public void TutorialSceneLoader()
    {
        SceneManager.LoadScene("TestBattleScene");
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
