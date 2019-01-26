using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameStateManager))]
[RequireComponent(typeof(MementoManager))]
[RequireComponent(typeof(MoverManager))]
public class WaveGenerator : MonoBehaviour
{
    public GameSettings gameSettings;
    public GameSettings.Wave currentWave;

    private GameStateManager _gameStateManager;
    private MementoManager _mementoManager;
    private MoverManager _moverManager;
    private Coroutine _runningWave;

    private void Start()
    {
        _gameStateManager = GetComponent<GameStateManager>();
        _mementoManager = GetComponent<MementoManager>();
        _moverManager = GetComponent<MoverManager>();
    }

    private void Update()
    {
        if (_gameStateManager.gameState != GameStateManager.GameState.Running)
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
        _gameStateManager.currentWave = Mathf.Clamp(_gameStateManager.currentWave + 1, 0, gameSettings.waves.Length);

        currentWave = gameSettings.waves[_gameStateManager.currentWave - 1].Clone();

        while (currentWave.numberOfEnemies > 0)
        {
            Memento memento = null;
            yield return new WaitUntil(() => _mementoManager.TryClaimMemento(out memento));

            var mover = _moverManager.SpawnMover();
            currentWave.numberOfEnemies--;
            mover.TargetMemento(memento);

            yield return new WaitForSeconds(currentWave.secondsBetweenEachSpawn + Random.Range(-currentWave.spawnJitter, currentWave.spawnJitter));
        }
    }
}
