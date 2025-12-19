using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { GameClear, GameOver}

    public static GameManager Instance { get; private set; }
    public GameState globalGameState { get; private set; }

    [SerializeField] private GameObject lockStage;
    [SerializeField] private GameObject unlockStage;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        switch (globalGameState)
        {
            case GameState.GameClear:
                {
                    lockStage.SetActive(false);
                    unlockStage.SetActive(true);
                }
                break;
            case GameState.GameOver: 
                {
                    lockStage.SetActive(true);
                    unlockStage.SetActive(false);
                }
                break;
        }
    }
}
