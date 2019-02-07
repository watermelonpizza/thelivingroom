﻿using System.Linq;
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

    public Canvas pauseCanvas;

    private bool _pausedToggled = false;

    public enum GameState
    {
        PreStart,
        Menu,
        Running,
        Paused,
        GameOver,
        EndScreen,
    }

    private void Start()
    {
        gameState = GameState.Running;
        currentFeels = gameSettings.startingFeels;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("TitleScene");
            return;
        }

        if (gameState == GameState.EndScreen)
        {
            return;
        }

        if (Input.GetAxisRaw("Cancel") > 0)
        {
            if (!_pausedToggled)
            {
                if (gameState == GameState.Running)
                {
                    pauseCanvas.gameObject.SetActive(true);
                    gameState = GameState.Paused;
                    Time.timeScale = 0;
                }
                else
                {
                    pauseCanvas.gameObject.SetActive(false);
                    gameState = GameState.Running;
                    Time.timeScale = 1;
                }

                _pausedToggled = true;
            }
        }
        else
        {
            _pausedToggled = false;
        }

        if (currentFeels <= 0)
        {
            gameState = GameState.GameOver;
        }

        if (gameState == GameState.GameOver)
        {
            gameState = GameState.EndScreen;

            foreach (var otherManager in gameObject.GetComponents<MonoBehaviour>().Where(x => x != this))
            {
                Destroy(otherManager);
            }

            SceneManager.LoadScene(gameSettings.SceneName);
        }
    }
}
