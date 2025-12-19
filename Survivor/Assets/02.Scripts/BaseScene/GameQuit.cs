using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuit : MonoBehaviour
{
    public void QuitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}