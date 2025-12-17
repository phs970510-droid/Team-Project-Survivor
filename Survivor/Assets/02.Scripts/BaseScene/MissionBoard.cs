using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class MissionBoard : MonoBehaviour
{
    public void BattleSceneLoader()
    {
        SceneManager.LoadScene("TestBattleScene");
    }
    public void MainSceneLoder()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BaseScene");
    }
    public void StageSelectLoder()
    {
        SceneManager.LoadScene("StageSelcetScene");
    }
}
