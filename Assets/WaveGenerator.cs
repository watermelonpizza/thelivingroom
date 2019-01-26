using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameState))]
public class WaveGenerator : MonoBehaviour
{
    public GameSettings gameSettings;
    public GameObject moverPrefab;

    private Coroutine _runningWave;

    private void Update()
    {
        if (gameSettings.gameState != GameSettings.GameState.Running || gameSettings.currentWave == gameSettings.waves.Length)
        {
            if (_runningWave != null)
            {
                StopCoroutine(_runningWave);
            }

            return;
        }
        
        if (_runningWave == null)
        {
            _runningWave = StartCoroutine(RunNextWave());
        }
    }

    private IEnumerator RunNextWave()
    {
        gameSettings.currentWave = Mathf.Clamp(gameSettings.currentWave + 1, 0, gameSettings.waves.Length);

        var wave = gameSettings.waves[gameSettings.currentWave - 1];

        for (int i = 0; i < wave.numberOfEnemies; i++)
        {
            SpawnEnemy();
        }

        return null;
    }

    private void SpawnEnemy()
    {

    }
}
