using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public enum GameState { GameClear, GameOver }
    public static TestGameManager instance { get; private set; }
    public GameState globalGameState { get; private set; }


    public GameObject lockStage;
    public GameObject unlockStage;


    private void Update()
    {
        switch (globalGameState)
        {
            case GameState.GameClear:
                {
                    unlockStage.SetActive(true);
                    lockStage.SetActive(false);
                }
                break;
            case GameState.GameOver:
                {
                    unlockStage.SetActive(false);
                    lockStage.SetActive(true);
                }
                break;
        }
    }
}
