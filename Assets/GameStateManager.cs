using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameSettings gameSettings;
    public GameState gameState;
    public int currentWaveNumber;
    public GameSettings.Wave currentWave;

    public int currentFeels;

    public enum GameState
    {
        PreStart,
        Menu,
        Running,
        GameOver,
    }

    private void Start()
    {
        gameState = GameState.Running;
        currentFeels = gameSettings.startingFeels;
    }

    private void Update()
    {

    }
}
