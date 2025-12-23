using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneLoad : MonoBehaviour
{
    public void SceneLoad()
    {
        SceneManager.LoadScene("TestSceneSelect");
    }
}
