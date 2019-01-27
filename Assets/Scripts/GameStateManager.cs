using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameSettings gameSettings;
    public GameState gameState;
    public int currentWaveNumber;
    public GameSettings.Wave currentWave;

    public int currentFeels;
    public GameSettings SceneName;



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
        if (gameState == GameState.GameOver || currentFeels <= 0)
        {
            SceneManager.LoadScene(gameSettings.SceneName);
        }
    }
}
