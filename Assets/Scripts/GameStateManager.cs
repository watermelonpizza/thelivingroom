using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
